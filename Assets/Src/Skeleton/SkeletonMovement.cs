using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(SkeletonController))]
public class SkeletonMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private bool shouldSignal = false;
    public UnityEvent OnArrived = new UnityEvent();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Sole purpose is to invoke an event ONCE to whoever listens
    private void Update()
    {
        if (shouldSignal && (transform.position - agent.destination).magnitude <= 0.5f)
        {
            shouldSignal = false;
            OnArrived.Invoke();
        }
    }

    public void MoveTo(Vector3 destination)
    {
        agent.SetDestination(destination);
        shouldSignal = true;
    }

    public void SetSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }
}
