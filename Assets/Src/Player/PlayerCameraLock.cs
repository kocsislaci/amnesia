using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLock : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Uram");
    }

    // Update is called once per frame
    private void LateUpdate() {
        var position = new Vector3(_player.transform.position.x, _player.transform.position.y + 9.5f, _player.transform.position.z + -8f);
        this.transform.SetPositionAndRotation(position, Quaternion.Euler(50f, 0 ,0));
    }  
}
