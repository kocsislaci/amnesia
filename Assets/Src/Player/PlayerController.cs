using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    // components
    public PlayerMovement playerMovement;
    
    // controls
    private PlayerControls playerControls;

    // basic internal info
    [CanBeNull]
    public GameObject Target {
        get => _target;
        set {
            _target = value;
        }
    }
    [CanBeNull] private GameObject _target = null;

    private Vector3? TargetPos {
        get => _targetPos;
        set {
            _targetPos = value;
            if (value != null) {
                playerMovement.MoveTo(value.Value);
            }
            Debug.Log($"targetSet {value}, {State}");
        }
    }
    private Vector3? _targetPos;
    
    public PlayerState State { get; private set; } = PlayerState.Idle;

    // RPG stats
    public float Health {
        get => _health;
        private set => _health = value;
    }
    private float _health;
    
    public float Speed {
        get => _speed;
        private set {
            _speed = value;
            playerMovement.SetSpeed(_speed);
        }
    }
    private float _speed;

    // attack range
    // attack speed
    // attack power
    
    private void Awake() {
        playerMovement = GetComponent<PlayerMovement>();
        playerControls = new();
    }

    private void OnEnable() {
        playerControls.Gameplay.Enable();
    }

    private void Start() {
        playerControls.Gameplay.LeftClick.performed += OnLeftClick;
        playerMovement.OnArrived.AddListener(HasArrived);

        Speed = 10.0f;
        Health = 100.0f;
    }

    private void OnDisable() {
        playerControls.Gameplay.Disable();
    }
    
    private void OnDestroy() {
        playerControls.Gameplay.LeftClick.performed -= OnLeftClick;
    }

    private void OnLeftClick(InputAction.CallbackContext callbackContext) {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(playerControls.Gameplay.MousePosition.ReadValue<Vector2>()), out hit, 100)) {
            
            // check what we hit ground or enemy
            switch (hit.collider.tag) {
                case "Ground": {
                    State = PlayerState.Moving;
                    TargetPos = hit.point;
                    break;
                }
                default: {
                    Debug.Log("Nothing interesting hit.");
                    break;
                }
            }
            
        }
    }

    private void HasArrived() {
        Debug.Log("arrived");
        State = PlayerState.Idle;
    }
}






public enum PlayerState {
    Idle,
    Moving,
    Attacking,
}
