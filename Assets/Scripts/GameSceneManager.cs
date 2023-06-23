using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork; // ����������� ����������

public class GameSceneManager : MonoBehaviour {

    // ����� ��� ������
    // ������ ����� ����� ��������� ����� ��� �������,
    // ���� random.range ��� ������ ������� ������ ����������
    public void OnSpawnerReady(bool finishedSetup, SceneSpawner spawner) {

        if (!finishedSetup) {
            if (NetworkClient.Instance.IsHost) { // ���� �� ����
                spawner.SpawnForPlayer(0, 0); // ������� ������ ��� �������� 0 �� ������ � �������� 0
            } else {
                spawner.SpawnForPlayer(0, 1); // ������� ������ ��� �������� 0 �� ������ � �������� 1
            }
            spawner.PlayerFinishedSceneSetup(); // ��������� �������������
            //Camera.main.gameObject.SetActive(false);
        }
    }
}