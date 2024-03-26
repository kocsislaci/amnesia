using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
  Idle,
  Moving,
}

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
  private static readonly int Run = Animator.StringToHash("Run");
  
  // components
  private Animator animator;
  private PlayerMovement playerMovement;

  // controls
  private PlayerControls playerControls;

  // basic internal info
  [CanBeNull]
  private GameObject Target {
    set {
      _target = value;
    }
  }
  [CanBeNull] private GameObject _target = null;

  private Vector3? TargetPos {
    set {
      _targetPos = value;
      if (value != null) {
        State = PlayerState.Moving;
        playerMovement.MoveTo(value.Value);
      }
      print($"TargetSet: {value}");
    }
  }
  private Vector3? _targetPos = null;

  private PlayerState State {
    set {
      _state = value;
      SetAnimationSettings(_state);
    } 
  }
  private PlayerState _state;
  
  // RPG stats
  public float Health {
    get => _health;
    private set => _health = value;
  }
  private float _health;


  // speed
  // attack range
  // attack speed
  // attack power

  void Awake() {
    playerMovement = GetComponent<PlayerMovement>();
    animator = GetComponent<Animator>();

    playerControls = new();
  }

  void OnEnable() {
    playerControls.Gameplay.Enable();
  }
  
  void Start() {
    playerControls.Gameplay.LeftClick.performed += OnLeftClick;
    playerMovement.onArrived.AddListener(HasArrived);

    Health = 100.0f;
    State = PlayerState.Idle;
  }

  void OnDisable() {
    playerControls.Gameplay.Disable();
  }

  void OnDestroy() {
    playerControls.Gameplay.LeftClick.performed -= OnLeftClick;
  }

  private void OnLeftClick(InputAction.CallbackContext callbackContext) {
    LayerMask mask = (LayerMask)((1 << LayerMask.NameToLayer("Ground")) | (1 << LayerMask.NameToLayer("Enemy")) | (1 << LayerMask.NameToLayer("Item")));
    
    if (Physics.Raycast(Camera.main.ScreenPointToRay(playerControls.Gameplay.MousePosition.ReadValue<Vector2>()), out RaycastHit hit, 100, mask))
    {
      switch (hit.collider.tag)
      {
        case "Ground": {
          TargetPos = hit.point;
          break;
        }
        default: { 
          break;
        }
      }
    }
  }

  private void HasArrived()
  {
    State = PlayerState.Idle;
    print("Arrived");
  }

  private void SetAnimationSettings(PlayerState state) {
    switch (state) {
      case PlayerState.Idle:
        animator.SetBool(Run, false);
        break;
      case PlayerState.Moving:
        animator.SetBool(Run, true);
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(state), state, null);
    }
  }
}
