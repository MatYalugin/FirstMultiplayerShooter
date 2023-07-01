using UnityEngine;
using UnityEngine.SceneManagement;
using SWNetwork; // подключаем библиотеку

public class ExitToMain : MonoBehaviour {

    [SerializeField] NetworkID networkId;
    [SerializeField] SyncPropertyAgent syncPropertyAgent;
    [SerializeField] string leaveBoolKey = "PlayerLeaveGame";
    [Space]
    [SerializeField] GameObject playerObject;
    private void Update() {
        if (Input.GetKey(KeyCode.Escape) && Input.GetKeyDown(KeyCode.P) && networkId.IsMine) {
            LeavePlayer();
        }
    }

    private void LeavePlayer() {
        NetworkClient.Lobby.LeaveRoom((successful, error) => {
            if (successful) {
                syncPropertyAgent.Modify(leaveBoolKey, true);
                Debug.Log("Left room");
                SceneManager.LoadScene(0);
                Cursor.lockState = CursorLockMode.None;
            } else {
                Debug.Log("Failed to leave room " + error);
            }
        });
    }

    public void OnPlayerLeftSynced()
    {
        bool isLeft = syncPropertyAgent.GetPropertyWithName(leaveBoolKey).GetBoolValue();

        if (isLeft)
        {
            Destroy(playerObject);   
        }
    }
}