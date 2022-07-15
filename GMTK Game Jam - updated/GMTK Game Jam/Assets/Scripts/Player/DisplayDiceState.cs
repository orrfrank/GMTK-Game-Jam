using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayDiceState : MonoBehaviour
{
    PlayerController playerController;
    public TextMeshProUGUI diceNumberDisplay;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        diceNumberDisplay.text = playerController.currentDiceNumber.ToString();
    }
}
