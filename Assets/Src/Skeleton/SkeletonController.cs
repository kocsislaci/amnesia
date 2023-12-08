using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkeletonController : MonoBehaviour
{
    Animator animator;

    public SkeletonMovement skeletonMovement;

    public PlayerController target;

    public SkeletonState State { get; private set; } = SkeletonState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Run", false);
        animator.SetBool("Attack", false);
    }
    
    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.transform.position;
        if (target != null & !this.transform.position.Equals(targetPosition))
        {
            animator.SetBool("Run", true);
            //State = SkeletonState.Moving;
            skeletonMovement.MoveTo(targetPosition);
        } else {
            animator.SetBool("Run", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            target = other.gameObject.GetComponent<PlayerController>();
            Debug.Log("collision with player");
            animator.SetBool("Attack", true);
        }
        animator.SetBool("Attack", false);
    }
}

public enum SkeletonState
{
    Idle,
    Moving,
    Attacking,
}