using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour {
  private NavMeshAgent agent;

  private bool isMoving = false;
  [HideInInspector]
  public UnityEvent onArrived = new UnityEvent();

  private void Awake() {
    agent = GetComponent<NavMeshAgent>();
  }
  
  // Sole purpose is to invoke an event ONCE to whoever listens
  private void Update() {
    if (isMoving && (transform.position - agent.destination).magnitude <= 0.1f) {
      isMoving = false;
      onArrived.Invoke();
    }
  }

  public void MoveTo(Vector3 destination) {
    agent.SetDestination(destination);
    isMoving = true;
  }
}
