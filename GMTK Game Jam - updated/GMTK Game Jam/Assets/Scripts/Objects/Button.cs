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
    bool wasPressed;
    bool doorFly;
    float buttonRaiseTime;
    float doorRaiseTime;

    // Start is called before the first frame update
    void Start()
    {
        levelDisplay.text = level.ToString();
        buttonRaiseTime = Random.Range(82f, 158f);
        doorRaiseTime = Random.Range(82f, 158f);
    }

    // Update is called once per frame
    void Update()
    {
        if (wasPressed)
        {
            transform.position -= new Vector3(0, 0, buttonRaiseTime * Time.deltaTime);
        }
        if (doorFly)
        {
            door.transform.position -= new Vector3(0, 0, doorRaiseTime * Time.deltaTime);
        }
    }
    public void OpenDoor()
    {
        if (!wasPressed)
            StartCoroutine(DoorDestructionAnimation());
    }
    IEnumerator DoorDestructionAnimation()
    {
        wasPressed = true;
        yield return new WaitForSeconds(0.1f);
        doorFly = true;
        yield return new WaitForSeconds(0.3f);
        Destroy(door);
        Destroy(gameObject);
    }
}
