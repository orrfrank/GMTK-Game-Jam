using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutAnimation : MonoBehaviour
{
    float delay;
    float velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Random.Range(80f, 120f);
        delay = Random.Range(0.5f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isDead && GameManager.timer > delay)
        {
            Debug.Log("Boosted");
            transform.position -= new Vector3(0, 0, velocity * Time.deltaTime);
        }    
    }
}
