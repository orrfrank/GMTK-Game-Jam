using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakableWall : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelDisplay;
    // Start is called before the first frame update
    void Start()
    {
        levelDisplay.text = level.ToString();
    }
    public void Break()
    {
        Destroy(gameObject);
    }
}
