using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SkeletonState
{
    Idle,
    Chase,
    Investigate,
    Patrol,
    // CloseEnough,
}

[RequireComponent(typeof(SkeletonMovement))]
[RequireComponent(typeof(Animator))]
public class SkeletonController : MonoBehaviour
{

    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private Animator animator;
    private SkeletonMovement skeletonMovement;
    private FOVEventBehaviour fovEventBehaviour;
    private CloseRangeEventBehaviour closeRangeEventBehaviour;

    private SkeletonState State
    {
        get => _state;
        set
        {
            _state = value;
            SetAnimationSettings(_state);
        }
    }
    private SkeletonState _state;

    private GameObject Target;

    [CanBeNull] private Vector3 LastSeenAt;

    private List<GameObject> waypoints;

    void Awake()
    { // Init the configuration specific stuff
        skeletonMovement = GetComponent<SkeletonMovement>();
        animator = GetComponent<Animator>();
        fovEventBehaviour = GetComponentInChildren<FOVEventBehaviour>();
        // closeRangeEventBehaviour = GetComponentInChildren<CloseRangeEventBehaviour>();

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint").ToList();
        fovEventBehaviour.SearchedTag = "Player";
        // closeRangeEventBehaviour.SearchedTag = "Player";

        patrol();
    }

    void Start()
    { // Init the game specific stuff
        fovEventBehaviour.OnMyTriggerEnter.AddListener(OnFOVEnter);
        fovEventBehaviour.OnMyTriggerExit.AddListener(OnFOVExit);
        // closeRangeEventBehaviour.OnMyTriggerEnter.AddListener(OnCloseRangeEnter);
        // closeRangeEventBehaviour.OnMyTriggerExit.AddListener(OnCloseRangeExit);
    }

    void Update()
    {
        // Our AI comes here :)
        // We repeat certain tasks in certain states
        if (!Target && LastSeenAt.magnitude == 0)
        {
            patrol();
        }

        Debug.Log("currently" + State);

        switch (State)
        {

            case SkeletonState.Patrol:
            case SkeletonState.Chase:
                float targetDistance = (this.transform.position - Target.transform.position).magnitude;
                bool isPatroling = State == SkeletonState.Patrol;

                if (targetDistance < 1 && isPatroling)
                {
                    int index = waypoints.FindIndex(w => w == Target);
                    if (index == waypoints.Count - 1)
                    {
                        Target = waypoints.First();
                    }
                    else
                    {
                        Target = waypoints[index + 1];
                    }
                }

                skeletonMovement.MoveTo(Target.transform.position);
                break;


            case SkeletonState.Investigate:
                skeletonMovement.MoveTo(LastSeenAt);

                float lastSeenDistance = (this.transform.position - LastSeenAt).magnitude;
                if (lastSeenDistance < 1)
                {
                    LastSeenAt = new Vector3(0, 0, 0);
                    State = SkeletonState.Patrol;
                }
                break;
        }
    }

    private void OnFOVEnter(Collider other)
    {
        Debug.Log("Player on sight! Let's follow it!");

        State = SkeletonState.Chase;
        Target = other.gameObject;
    }

    private void OnFOVExit(Collider other)
    {
        Debug.Log("Lost player!");
        State = SkeletonState.Investigate;
        LastSeenAt = other.gameObject.transform.position;
        Target = null;
    }

    // private void OnCloseRangeEnter(Collider other) {
    //     State = SkeletonState.CloseEnough;
    // }

    // private void OnCloseRangeExit(Collider other) {
    //     State = SkeletonState.Chase;
    // }

    private void SetAnimationSettings(SkeletonState state)
    {
        switch (state)
        {
            case SkeletonState.Idle:
                animator.SetBool(Run, false);
                animator.SetBool(Attack, false);
                break;
            case SkeletonState.Investigate:
            case SkeletonState.Chase:
            case SkeletonState.Patrol:
                animator.SetBool(Run, true);
                animator.SetBool(Attack, false);
                break;
                // case SkeletonState.CloseEnough:
                //     animator.SetBool(Run, false);
                //     animator.SetBool(Attack, true);
                //     break;
                // default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private void patrol()
    {
        Target = waypoints.First();
        State = SkeletonState.Patrol;
    }
}
