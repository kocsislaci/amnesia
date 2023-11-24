using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for detecting if there are any walls
/// between the camera and the player gameObject that
/// should be made transparent
/// </summary>
public class CameraWallDetector : MonoBehaviour
{
    private Dictionary<int, WallObstacle> wallsMadeTransparent = new Dictionary<int, WallObstacle>();
    private int throttle = 0;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("RedCube");
    }
    void Update()
    {
        // Enough to check this every 5 frames
        if (throttle < 5) {
            throttle++;
            return;
        } else {
            throttle = 0;
        }
        float dist = Vector3.Distance(transform.position, player.transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position,
                                                      transform.forward, dist);
        var usedInstanceIds = new HashSet<int>();
        foreach (RaycastHit h in hits)
        {
            if (!h.rigidbody) {
                continue;
            }
            var renderer = h.rigidbody.gameObject.GetComponent<MeshRenderer>();
            if (!renderer) {
                continue;
            }
            var wallObstacle = h.rigidbody.gameObject.GetComponent<WallObstacle>();
            if (!wallObstacle) {
                continue;
            }
            var instanceId = h.rigidbody.gameObject.GetInstanceID();
            usedInstanceIds.Add(instanceId);
            if (wallsMadeTransparent.ContainsKey(instanceId) && wallsMadeTransparent[instanceId].isTransparent) {
                continue;
            }
            wallsMadeTransparent[instanceId] = wallObstacle;
            wallObstacle.SetTransparent();
            //Change the opacity of the of each object to semitransparent.
        }
        foreach (var id in wallsMadeTransparent.Keys)
        {
            if (usedInstanceIds.Contains(id)) continue;
            wallsMadeTransparent[id].SetOriginalColor();
        }
    }
}
