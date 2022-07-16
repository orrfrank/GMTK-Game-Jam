using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    public Color doorColor;
    public int level = 1;
    public GameObject door;
    public TextMeshProUGUI levelDisplay;
    // Start is called before the first frame update
    void Start()
    {
         levelDisplay.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenDoor()
    {
        Destroy(door);
        Destroy(gameObject);
    }
}
