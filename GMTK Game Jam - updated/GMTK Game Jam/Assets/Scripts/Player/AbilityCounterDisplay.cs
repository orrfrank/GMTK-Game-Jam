using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityCounterDisplay : MonoBehaviour
{
    public TextMeshProUGUI dashText;
    public Image dashImage;
    public TextMeshProUGUI slashText;
    public Image slashImage;
    PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        dashText.text = playerController.dashCounter.ToString();
    }
}
