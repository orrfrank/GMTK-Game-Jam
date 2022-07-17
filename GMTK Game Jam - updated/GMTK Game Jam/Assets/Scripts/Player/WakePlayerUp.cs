using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakePlayerUp : MonoBehaviour
{
    public float delay;
    public PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WakeUp());
    }
    IEnumerator WakeUp()
    {
        controller.enabled = false;
        yield return new WaitForSeconds(delay);
        controller.enabled = true;
    }
}
