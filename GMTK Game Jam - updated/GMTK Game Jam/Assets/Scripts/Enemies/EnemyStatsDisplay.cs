using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyStatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelDisplay;
    // Start is called before the first frame update
    void Start()
    {
        levelDisplay.text = GetComponent<EnemyStats>().level.ToString();
    }
}
