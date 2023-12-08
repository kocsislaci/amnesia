using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SkeletonState
{
    Idle,
    HasTarget,
    CloseEnough,
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
    
    private SkeletonState State {
        get => _state;
        set {
            _state = value;
            SetAnimationSettings(_state);
        } 
    }
    private SkeletonState _state;

    [CanBeNull]
    private PlayerController Target {
        get => _target;
        set {
            _target = value;
            if (value != null) {
                State = SkeletonState.HasTarget;
            }
        }
    }
    [CanBeNull] private PlayerController _target;
    
    void Awake() { // Init the configuration specific stuff
        skeletonMovement = GetComponent<SkeletonMovement>();
        animator = GetComponent<Animator>();
        fovEventBehaviour = GetComponentInChildren<FOVEventBehaviour>();
        closeRangeEventBehaviour = GetComponentInChildren<CloseRangeEventBehaviour>();

        fovEventBehaviour.SearchedTag = "Player";
        closeRangeEventBehaviour.SearchedTag = "Player";
    }
    
    void Start() { // Init the game specific stuff
        State = SkeletonState.Idle;
        Target = null;
        
        fovEventBehaviour.OnMyTriggerEnter.AddListener(OnFOVEnter);
        closeRangeEventBehaviour.OnMyTriggerEnter.AddListener(OnCloseRangeEnter);
        closeRangeEventBehaviour.OnMyTriggerExit.AddListener(OnCloseRangeExit);
    }
    
    void Update()
    {
        // Our AI comes here :)
        // We repeat certain tasks in certain states

        switch (State) {
            case SkeletonState.Idle: // nothing :/
                break;
            case SkeletonState.HasTarget: // we have target, follow it damn
                skeletonMovement.MoveTo(Target.transform.position);
                break;
            case SkeletonState.CloseEnough: // we close enough, ATTACK
                print("Close enough, ATTACK!");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnFOVEnter(Collider other) {
        Debug.Log("Player on sight! Let's follow it!");
            
        Target = other.gameObject.GetComponent<PlayerController>();
    }

    private void OnCloseRangeEnter(Collider other) {
        State = SkeletonState.CloseEnough;
    }

    private void OnCloseRangeExit(Collider other) {
        State = SkeletonState.HasTarget;
    }
    
    private void SetAnimationSettings(SkeletonState state) {
        switch (state) {
            case SkeletonState.Idle:
                animator.SetBool(Run, false);
                animator.SetBool(Attack, false);
                break;
            case SkeletonState.HasTarget:
                animator.SetBool(Run, true);
                animator.SetBool(Attack, false);
                break;
            case SkeletonState.CloseEnough:
                animator.SetBool(Run, false);
                animator.SetBool(Attack, true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}
