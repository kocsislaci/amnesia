using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
  private NavMeshAgent agent;
  private PlayerControls playerControls;

  void Awake() {
    agent = GetComponent<NavMeshAgent>();
    playerControls = new PlayerControls();
    playerControls.Gameplay.LeftClick.performed += Move;
  }
  private void OnEnable() {
    playerControls.Gameplay.Enable();
  }

  private void OnDisable() {
    playerControls.Gameplay.Disable();
  }
  
  private void Move(InputAction.CallbackContext context) {
    RaycastHit hit;

    if (Physics.Raycast(Camera.main.ScreenPointToRay(playerControls.Gameplay.MousePosition.ReadValue<Vector2>()), out hit, 100))
    {
      agent.destination = hit.point;
    }
  }
}
