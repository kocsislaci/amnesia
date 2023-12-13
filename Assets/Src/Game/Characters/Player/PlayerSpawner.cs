using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject playerPrefab;

    public void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, transform.position, transform.rotation);
        var mainCamera = Camera.main.GetComponent<PlayerCameraLock>();
        mainCamera.Lock(player);
    }
}
