using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuCameraController : MonoBehaviour
{
    private List<MainMenuCardController> highlights = new List<MainMenuCardController>();
    private RaycastHit raycastHit;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(AnimateCameraMovement());
    }

    private void OnDestroy() {
        StopCoroutine(AnimateCameraMovement());
    }

    private void Update() {
        if (!Cursor.visible) { return; }
        if (Mouse.current == null) {
            return;
        }
        var hit = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(hit, out raycastHit);
        if (raycastHit.transform == null) {
            return;
        }
        var highlighter = raycastHit.transform.GetComponent<MainMenuCardController>();
        highlights.ForEach((item) => { item.TurnOffHighlight(); });
        if (highlighter == null) {
            highlighter = raycastHit.transform.GetComponentInParent<MainMenuCardController>();
            if (highlighter == null) return;
        }
        if (highlighter.isHighlighted) {
            return;
        }
        highlights.Add(highlighter);
        highlights.ForEach((item) => { item.TurnOffHighlight(); });
        highlighter.ToggleHighlight();
        if (Mouse.current.leftButton.isPressed) {
            highlighter.Clicked();
        }
    }

    private IEnumerator AnimateCameraMovement() {
        var rotate = 0f;
        var z = -6f;
        while (z < -2.9f) {
            z += 0.02f;
            rotate += 0.03f;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.Translate(new UnityEngine.Vector3(0, 0, 0.02f));
            gameObject.transform.Rotate(new UnityEngine.Vector3(0.03f, 0, 0));
        }
        while (rotate > 0) {
            rotate -= 0.03f;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.Rotate(new UnityEngine.Vector3(-0.03f, 0.2f, 0));
        }
        while (rotate < 20) {
            rotate += 0.1f;
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.Rotate(new UnityEngine.Vector3(0.1f, 0.05f, 0));
        }
        while (gameObject.transform.position.z < -1f) {
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.Translate(new UnityEngine.Vector3(0, 0, 0.02f));
            gameObject.transform.Rotate(new UnityEngine.Vector3(0.1f, 0.05f, 0));
        }
        while (gameObject.transform.position.x < 0f) {
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.Translate(new UnityEngine.Vector3(0.02f, 0, 0));
        }
        while (gameObject.transform.rotation.y > 0f) {
            yield return new WaitForSeconds(0.002f);
            gameObject.transform.Rotate(new UnityEngine.Vector3(0, -0.05f, 0));
        }
        while (gameObject.transform.position.z < -0.93f) {
            yield return new WaitForSeconds(0.005f);
            gameObject.transform.Translate(new UnityEngine.Vector3(0, 0, 0.02f));
            gameObject.transform.Rotate(new UnityEngine.Vector3(0.05f, 0, 0));
        }
        while (gameObject.transform.rotation.z < 0f) {
            yield return new WaitForSeconds(0.0005f);
            gameObject.transform.Rotate(new UnityEngine.Vector3(0, 0, 0.01f));
        }
        Cursor.visible = true;
        yield return null;
    }
}
