using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    public Transform dungeonSpawn;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) other.transform.position = new Vector3(dungeonSpawn.transform.position.x, dungeonSpawn.transform.position.y + 2, dungeonSpawn.transform.position.z);
    }

}
