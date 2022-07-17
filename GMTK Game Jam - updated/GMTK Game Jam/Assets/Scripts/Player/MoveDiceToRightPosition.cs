using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDiceToRightPosition : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject DiceModel;
    GameObject player;
    void Start()
    {
        DiceModel = gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        MoveDiceToRightPositionVoid();
    }

    void MoveDiceToRightPositionVoid()
    {
        DiceModel.transform.position = Vector3.Lerp(DiceModel.transform.position, player.transform.position + new Vector3(0.5f, -0.5f), 20 * Time.deltaTime);
    }
}
