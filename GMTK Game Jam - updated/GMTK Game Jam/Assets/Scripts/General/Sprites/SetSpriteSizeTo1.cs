using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpriteSizeTo1 : MonoBehaviour
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.size = new Vector2(1f, 1f);
    }
}
