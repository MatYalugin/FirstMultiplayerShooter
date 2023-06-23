using UnityEngine;
using UnityEngine.UI;
using SWNetwork;

[RequireComponent(typeof(NetworkID))]
public class Player : MonoBehaviour
{
    public Health health;

    private Text tipText;

    Rigidbody rb;
    NetworkID networkID;
    float speed;
    public float defaultSpeed;
    public float sprintSpeed;
    bool sprintOn;

    public AudioListener audioListener;
    public GameObject playerCameraGO;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        networkID = GetComponent<NetworkID>();
        speed = defaultSpeed;

        playerCameraGO.SetActive(true);

        if (networkID.IsMine)
        {
            tipText = GameObject.FindGameObjectWithTag("TipText").GetComponent<Text>();

            audioListener.enabled = true;

            playerCameraGO.GetComponent<FirstPersonLook>().enabled = true;
        }
        else
        {
            audioListener.enabled = false;

            playerCameraGO.GetComponent<FirstPersonLook>().enabled = false;
            playerCameraGO.GetComponent<Camera>().enabled = false;
        }

        if (health != null)
        {
            health.OnDeath += OnPlayerDeath;
        }
    }

    void Update()
    {
        if (networkID.IsMine)
        {
            Sprint();
            Move();
            speed = sprintOn ? sprintSpeed : defaultSpeed;
        }
    }

    public void OnPlayerDeath()
    {
        health.Restore();
        gameObject.transform.position = Respawn.FindSafestFor(this);
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
