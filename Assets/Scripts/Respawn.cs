using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField] float checkRadius = 6;

    public bool IsSafeFor(Player p) {
        var allPlayers = Object.FindObjectsOfType<Player>();
        foreach (var player in allPlayers) {
            if (Vector3.Distance(transform.position, player.transform.position) < checkRadius) {
                continue;
            }

            return true;
        }

        return false;
    }

    public static Vector3 FindSafestFor(Player player) {
        var allRespawns = Object.FindObjectsOfType<Respawn>();
        var allSafeSpawns = new List<Respawn>();

        foreach (var respawn in allRespawns)
            if (!allSafeSpawns.Contains(respawn) && respawn.IsSafeFor(player))
                allSafeSpawns.Add(respawn);

        if (allSafeSpawns.Count > 0) {
            var randomSpawn = allRespawns[Random.Range(0, allRespawns.Length)];
            return randomSpawn.transform.position;

        } else {
            var randomSpawn = allSafeSpawns[Random.Range(0, allSafeSpawns.Count)];
            return randomSpawn.transform.position;
        }
    }
}