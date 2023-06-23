using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork; // подключаем библиотеку

public class Melee : MonoBehaviour
{
    public float hitDistance;
    public Animator animator;
    public AudioSource kickSound;
    public float damage = 30f;
    public Camera mainCamera;
    public GameObject playerGO;
    public float fireRate = 1f;
    public float invokeAnimDuration = 3.5f;
    private bool isReadyToKick = true;
    public Text ammoText;

    public NetworkID networkID;
    // Start is called before the first frame update
    void Start()
    {
        ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (networkID.IsMine == true)
        {
            ammoText.text = "Ammo: &";

            Kick();
            if (isReadyToKick != true)
            {
                playerGO.GetComponent<ChangeWeapon>().enabled = false;
            }
            if (isReadyToKick == true)
            {
                playerGO.GetComponent<ChangeWeapon>().enabled = true;
            }
            if (Input.GetKey(KeyCode.F))
            {
                Inspection();
            }
        }
    }
    public void Kick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isReadyToKick == true)
        {
            isReadyToKick = false;
            Invoke("makeReadyToKick", fireRate);
            animator.Play("Kick");
            kickSound.Play();
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, hitDistance))
            {
                if (hit.transform.tag.Equals("Player"))
                {
                    hit.transform.GetComponent<Player>().health.Damage(damage);
                }
            }
        }
    }
    public void Inspection()
    {
        animator.Play("Inspection");
        isReadyToKick = false;
        Invoke("makeReadyToKick", invokeAnimDuration);
    }
    public void makeReadyToKick()
    {
        isReadyToKick = true;
    }

}
