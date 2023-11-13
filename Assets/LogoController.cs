using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoController : MonoBehaviour
{
    private Image _logo;
    private TextMeshProUGUI _text;
    private float scale = 0.1f;
    void Start()
    {
        _logo = gameObject.GetComponentInChildren<Image>();
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(Animate());
    }
    private void OnDestroy() {
        StopCoroutine(Animate());
    }
    private IEnumerator Animate() {
        if (scale > 0.8) yield return null;
        while (scale < 0.4) {
            scale += 0.001f;
            yield return new WaitForSeconds(0.001f);
            _logo.rectTransform.localScale = new Vector3(scale, scale, scale);
        }
        scale = 0.1f;
        while (scale < 1) {
            scale += 0.001f;
            yield return new WaitForSeconds(0.01f);
            _text.alpha += scale;
        }
        yield return null;
    }
}
