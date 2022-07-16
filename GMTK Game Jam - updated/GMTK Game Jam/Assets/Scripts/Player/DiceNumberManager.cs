using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceNumberManager : MonoBehaviour
{
    public string startingSequence = "";
    public int bottomNum = 6;
    public int[,] dieSide = {{ 0, 5, 0 },
                             { 4, 1, 3 },
                             { 0, 2, 0 }};
    private void Start()
    {
        for (int i = 0; i < startingSequence.Length; i++)
        {
            if (startingSequence[i] == 'd')
            {
                RollDown();
            }
            else if (startingSequence[i] == 'r')
            {
                RollRight();
            }
            else if (startingSequence[i] == 'l')
            {
                RollLeft();
            }
            else if (startingSequence[i] == 'u')
            {
                RollUp();
            }
        }
    }
    public void RollRight()
    {
        int[] holder = { bottomNum, dieSide[1, 0], dieSide[1, 1] };
        bottomNum = dieSide[1, 2];
        dieSide[1, 0] = holder[0];
        dieSide[1, 1] = holder[1];
        dieSide[1, 2] = holder[2];
    }
    public void RollLeft()
    {
        int[] holder = { dieSide[1, 1], dieSide[1, 2], bottomNum };
        bottomNum = dieSide[1, 0];
        dieSide[1, 0] = holder[0];
        dieSide[1, 1] = holder[1];
        dieSide[1, 2] = holder[2];
    }
    public void RollUp()
    {
        int[] holder = { dieSide[1, 1], dieSide[2, 1], bottomNum };
        bottomNum = dieSide[0, 1];
        dieSide[0, 1] = holder[0];
        dieSide[1, 1] = holder[1];
        dieSide[2, 1] = holder[2];
    }
    public void RollDown()
    {
        int[] holder = { bottomNum, dieSide[0, 1], dieSide[1, 1], };
        bottomNum = dieSide[2, 1];
        dieSide[0, 1] = holder[0];
        dieSide[1, 1] = holder[1];
        dieSide[2, 1] = holder[2];
    }
}

