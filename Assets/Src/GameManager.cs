using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private GameObject _placeholderPrefab;

    private void Start() {
        Debug.Log("I started!");
        StartCoroutine(StartDrawing());
    }

    private IEnumerator StartDrawing() {
        yield return new WaitForSeconds(2);
        Debug.Log("I waited for 2 seconds!");
        _placeholderPrefab = Resources.Load<GameObject>("Level/Prefabs/Cube");
        var placeHolderCube = Instantiate(_placeholderPrefab, new UnityEngine.Vector3(0, 2, 0), UnityEngine.Quaternion.identity);
        yield return null;
    }
}

