using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork; // ïîäêëþ÷àåì áèáëèîòåêó

public class Player : MonoBehaviour
{

    public float health = 100f;
    public Text healthText;
    private Text tipText;
    //Movement
    Rigidbody rb;
    public NetworkID networkID; // ññûëêà íà ìóëüòèïëååð êîìïîíåíòó
    float speed;
    public float defaultSpeed;
    public float sprintSpeed;
    bool sprintOn;
    public AudioListener audioListener;
    public Transform spawnpoint1;
    public Transform spawnpoint2;

    public GameObject playerCameraGO;


    private void Start()
    {
        GameObject spawnpoint1GO = GameObject.FindGameObjectWithTag("spawnpoint1");
        GameObject spawnpoint2GO = GameObject.FindGameObjectWithTag("spawnpoint2");

        spawnpoint1 = spawnpoint1GO.transform;
        spawnpoint2 = spawnpoint2GO.transform;


        rb = GetComponent<Rigidbody>();
        networkID = GetComponent<NetworkID>(); // ïîëó÷àåì ññûëêó
        speed = defaultSpeed;
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        tipText = GameObject.FindGameObjectWithTag("TipText").GetComponent<Text>();

        if (gameObject.GetComponent<NetworkID>().IsMine == true)
        {
            audioListener.enabled = true;
        }
        if (gameObject.GetComponent<NetworkID>().IsMine == false)
        {
            audioListener.enabled = false;
        }
    }

    void Update()
    {


        if (gameObject.GetComponent<NetworkID>().IsMine == true)
        {
            if (networkID.IsMine == true)
            { // åñëè ýòèì ïåðñîíàæåì óïðàâëÿåì ÌÛ
                healthText.text = "Health: " + health; // ïîêàçûâàåì çäîðîâüå èãðîêà òîëüêî ñàìîìó ñåáå

                Sprint();
                Move();
                speed = sprintOn ? sprintSpeed : defaultSpeed;

                playerCameraGO.GetComponent<FirstPersonLook>().enabled = true;
            }
            playerCameraGO.SetActive(true);
        }
        else if (gameObject.GetComponent<NetworkID>().IsMine == false)
        {
            playerCameraGO.GetComponent<FirstPersonLook>().enabled = false;
            playerCameraGO.GetComponent<Camera>().enabled = false;
            playerCameraGO.SetActive(true);
        }
    }

    public void Hurt(float damage)
    {
        health -= damage;
        healthText.text = "Health: " + health;
        if (health <= 0f)
        {
            health = 100f;
            if(Random.value > 0.5f)
            {
                gameObject.transform.position = spawnpoint1.position;
            }
            else
            {
                gameObject.transform.position = spawnpoint2.position;
            }
            
        }
    }

    public void Move()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        Vector3 Move = transform.right * H * speed + transform.forward * V * speed;
        rb.velocity = new Vector3(Move.x, rb.velocity.y, Move.z);
    }

    public void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintOn = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintOn = false;
        }
    }
}
