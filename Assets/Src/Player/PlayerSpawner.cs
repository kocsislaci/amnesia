using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject playerPrefab;

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, transform.position, transform.rotation);
    }
}
