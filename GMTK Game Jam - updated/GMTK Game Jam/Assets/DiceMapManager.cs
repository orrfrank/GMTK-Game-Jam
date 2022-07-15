using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceMapManager : MonoBehaviour
{
    public TextMeshProUGUI[] displays;
    public DiceNumberManager numberManager;
    // Start is called before the first frame update
    void Start()
    {
        numberManager = GameObject.FindGameObjectWithTag("Player").GetComponent<DiceNumberManager>();
    }

    // Update is called once per frame
    void Update()
    {
        displays[0].text = numberManager.dieSide[1, 1].ToString();
        displays[2].text = numberManager.dieSide[0, 1].ToString();
        displays[3].text = numberManager.dieSide[1, 2].ToString();
        displays[4].text = numberManager.dieSide[2, 1].ToString();
        displays[1].text = numberManager.dieSide[1, 0].ToString();
    }
}
