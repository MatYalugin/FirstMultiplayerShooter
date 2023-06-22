using UnityEngine;
using UnityEngine.SceneManagement;
using SWNetwork; // подключаем библиотеку

public class ExitToMain : MonoBehaviour
{

    NetworkID networkID; // ссылка на мультиплеер компоненту

    private void Start() {
        networkID = GetComponent<NetworkID>(); // получаем ссылку
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P) && networkID.IsMine) // обязательно добавляем проверку, является ли данный игрок локальным
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
