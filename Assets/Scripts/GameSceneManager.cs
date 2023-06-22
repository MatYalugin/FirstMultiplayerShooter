using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork; // испортируем бибилотеку

public class GameSceneManager : MonoBehaviour {

    // метод для спавна
    // вообще можно взять случайный спавн для каждого,
    // типа random.range или вообще каждому давать уникальный
    public void OnSpawnerReady(bool finishedSetup, SceneSpawner spawner) {
        if (!finishedSetup) {
            if (NetworkClient.Instance.IsHost) { // если мы хост
                spawner.SpawnForPlayer(0, 0); // спавним префаб под индексом 0 на спавне с индексом 0
            } else {
                spawner.SpawnForPlayer(0, 1); // спавним префаб под индексом 0 на спавне с индексом 1
            }
            spawner.PlayerFinishedSceneSetup(); // завершаем инициализацию
            //Camera.main.gameObject.SetActive(false);
        }
    }
}