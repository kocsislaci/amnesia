using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCardController : MonoBehaviour
{
    public bool isHighlighted = false;
    private Transform _front;
    private Transform _frontHighlighted;
    private Material _original;
    void Start()
    {
        _front = transform.Find("Front");
        _frontHighlighted = transform.Find("Front_Highlighted");
        _original = gameObject.GetComponent<MeshRenderer>().material;
    }

    public void TurnOffHighlight()
    {
        if (isHighlighted)
            ToggleHighlight();
    }

    public void Clicked() {
        var splitted = gameObject.name.Split("Card");
        if (splitted.Length < 1) {
            Debug.Log("Could not find route for: " + gameObject.name);
            return;
        }
        var route = splitted[0];
        // SceneManager access should happen through the game manager?
        switch(route.ToLower()) {
            case "start":
            Debug.Log("Let's start mate");
            break;
            case "credits":
            Debug.Log("Let's show credits mate");
            SceneManager.LoadScene("CompanyIntroScene");
            break;
            case "exit":
            Debug.Log("I guess let's quit");
            Application.Quit();
            break;
            default:
            throw new Exception("Could not identify route: " + route);
        }
    }

    public void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
        var renderer = gameObject.GetComponent<MeshRenderer>();
        if (isHighlighted)
        {
            _front.gameObject.SetActive(false);
            renderer.material = Resources.Load<Material>("BA_Color_Purple");
            _frontHighlighted.gameObject.SetActive(true);
            return;
        }
        _front.gameObject.SetActive(true);
        _frontHighlighted.gameObject.SetActive(false);
        renderer.material = _original;
    }
}
