using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{

    public SkeletonMovement skeletonMovement;

    public PlayerController target;

    public SkeletonState State { get; private set; } = SkeletonState.Idle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.transform.position;
        if (target != null & !this.transform.position.Equals(targetPosition))
        {
            skeletonMovement.MoveTo(targetPosition);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            target = other.gameObject.GetComponent<PlayerController>();
            Debug.Log("collision with player");
        }
    }
}

public enum SkeletonState
{
    Idle,
    Moving,
    Attacking,
}