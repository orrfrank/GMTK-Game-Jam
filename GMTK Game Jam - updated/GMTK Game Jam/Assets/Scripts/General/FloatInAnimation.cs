using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatInAnimation : MonoBehaviour
{
    float startingHeight;
    float velocity;
    float delay;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        startingHeight = transform.position.z;
        transform.position -= new Vector3(0, 0, 30);
        velocity = Random.Range(80f, 120f);
        delay = Random.Range(0.5f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            transform.position += new Vector3(0, 0, velocity * Time.deltaTime);
            if (transform.position.z > startingHeight)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, startingHeight);
                this.enabled = false;
            }
        }
        
    }
}
