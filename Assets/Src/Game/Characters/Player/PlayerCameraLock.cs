using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLock : MonoBehaviour
{
    GameObject player;

    public void Lock(GameObject player)
    {
        this.player = player;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var position = new Vector3(player.transform.position.x, player.transform.position.y + 9.5f, player.transform.position.z + -8f);
        this.transform.SetPositionAndRotation(position, Quaternion.Euler(50f, 0, 0));
    }
}
