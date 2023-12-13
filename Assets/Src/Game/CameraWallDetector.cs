using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Script responsible for detecting if there are any walls
/// between the camera and the player gameObject that
/// should be made transparent
/// </summary>
public class CameraWallDetector : MonoBehaviour
{
    private Dictionary<int, WallTransparencyController> wallsMadeTransparent = new Dictionary<int, WallTransparencyController>();
    private int throttle = 0;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        LayerMask mask = (1 << LayerMask.NameToLayer("Obstacle"));
        RaycastHit[] hits = Physics.RaycastAll(transform.position,
            player.transform.position - this.transform.position, dist, mask);
        var usedInstanceIds = new HashSet<int>();
        foreach (RaycastHit h in hits)
        {
            var renderer = h.collider.gameObject.GetComponent<MeshRenderer>();
            if (!renderer) {
                continue;
            }
            var wallObstacle = h.collider.gameObject.GetComponent<WallTransparencyController>();
            if (!wallObstacle) {
                continue;
            }
            var instanceId = h.collider.gameObject.GetInstanceID();
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
