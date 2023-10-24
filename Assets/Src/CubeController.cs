using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Placeholder controller for camera testing
/// </summary>
public class CubeController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private PlayerInput _playerInput;
    public bool isMoving = false;
    private UnityEngine.Vector3 forwardVector = new UnityEngine.Vector3(0f, 0f, 0.1f);
    private UnityEngine.Vector3 backwardVector = new UnityEngine.Vector3(0f, 0f, -0.1f);
    private UnityEngine.Vector3 rightwardVector = new UnityEngine.Vector3(0.1f, 0f, 0);
    private UnityEngine.Vector3 leftwardVector = new UnityEngine.Vector3(-0.1f, 0f, 0f);

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Move"].performed += OnJump;
    }

    private IEnumerator Move(UnityEngine.Vector3 by)
    {
        while (isMoving)
        {
            yield return new WaitForSeconds(0.01f);
            _rigidBody.MovePosition(by + _rigidBody.position);
        }
        yield return null;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        var movementVector = forwardVector;
        switch (context.control.name)
        {
            case "s":
                movementVector = backwardVector;
                break;
            case "a":
                movementVector = leftwardVector;
                break;
            case "d":
                movementVector = rightwardVector;
                break;
            default:
                break;
        }
        isMoving = true;
        StartCoroutine(Move(movementVector));
        context.action.canceled += (context) =>
        {
            isMoving = false;
            StopCoroutine(Move(movementVector));
        };
    }
}
