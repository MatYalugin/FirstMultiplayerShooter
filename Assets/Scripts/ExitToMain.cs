using UnityEngine;
using UnityEngine.SceneManagement;
using SWNetwork; // ���������� ����������

public class ExitToMain : MonoBehaviour
{

    NetworkID networkID; // ������ �� ����������� ����������

    private void Start() {
        networkID = GetComponent<NetworkID>(); // �������� ������
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P) && networkID.IsMine) // ����������� ��������� ��������, �������� �� ������ ����� ���������
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
