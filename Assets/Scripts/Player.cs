using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork; // подключаем библиотеку

public class Player : MonoBehaviour
{

    public float health = 100f;
    public Text healthText;
    //Movement
    Rigidbody rb;
    NetworkID networkID; // ссылка на мультиплеер компоненту
    float speed;
    public float defaultSpeed;
    public float sprintSpeed;
    bool sprintOn;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        networkID = GetComponent<NetworkID>(); // получаем ссылку
        speed = defaultSpeed;
        healthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
    }

    void Update()
    {
        if (networkID.IsMine) { // если этим персонажем управляем МЫ
            healthText.text = "Health: " + health; // показываем здоровье игрока только самому себе

            Sprint();
            Move();

            speed = sprintOn ? sprintSpeed : defaultSpeed;
        }
    }

    public void Hurt(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            health = 100f;
            transform.position = Vector3.zero;
        }
    }

    public void Move()
    {
        if (CheckGround())
        {
            float H = Input.GetAxis("Horizontal");
            float V = Input.GetAxis("Vertical");
            Vector3 Move = transform.right * H * speed + transform.forward * V * speed;
            rb.velocity = new Vector3(Move.x, rb.velocity.y, Move.z);
        }
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

    public bool CheckGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y + 0.1f);
    }
}
