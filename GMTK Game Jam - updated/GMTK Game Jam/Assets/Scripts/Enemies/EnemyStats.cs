using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int direction;
    public int level;
    public void Kill()
    {
        Destroy(gameObject);
    }
}
