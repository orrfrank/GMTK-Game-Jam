using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerEventCamera : MonoBehaviour
{
    Canvas canvas;
    //this scripts is used to set the event camera of the number display on the dice
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }
}
