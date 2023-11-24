using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to identify walls between the camera and the player
/// Expects to be mesh rendered
/// </summary>
public class WallObstacle : MonoBehaviour
{
    public bool isTransparent = false;
    private Color _originalColor;
    private Color _transparentColor;

    private void Start() {
        var renderer = gameObject.GetComponent<MeshRenderer>();
        _originalColor = renderer.material.color;
        _transparentColor = new Color(
            _originalColor.r,
            _originalColor.g,
            _originalColor.b,
            0.5f
        );
    }
    public void SetOriginalColor() {
        if (!isTransparent) return;
        isTransparent = false;
        var renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material.color = _originalColor;
    }
    public void SetTransparent() {
        if (isTransparent) return;
        isTransparent = true;
        var renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material.color = _transparentColor;
    }
}
