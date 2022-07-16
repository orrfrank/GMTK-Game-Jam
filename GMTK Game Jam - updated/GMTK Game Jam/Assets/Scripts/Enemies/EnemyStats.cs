using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int direction;
    public int level;
    public int[] pattern = {1};
    public LayerMask playerLayermask;
    public GameObject directionIndicator;
    public bool playerHit;
    Vector2 targetRotation;
    public void Kill()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        direction = pattern[GameManager.ticks % pattern.Length];
        targetRotation = new Vector3(0, 0, -direction * 90);
        directionIndicator.transform.localEulerAngles = Vector3.Lerp(directionIndicator.transform.eulerAngles, targetRotation, 20*Time.deltaTime);
        CheckForPlayerHit();
    }
    void CheckForPlayerHit()
    {
        if (direction == 1)
        {
            playerHit = IsHittingPlayer(0.6f, 0);
        }
        else if (direction == 2)
        {
            playerHit = IsHittingPlayer(0, -0.6f);
        }
        else if (direction == 3)
        {
            playerHit = IsHittingPlayer(-0.6f, 0);
        }
        else if (direction == 4)
        {
            playerHit = IsHittingPlayer(0, 0.6f);
        }
    }
    public bool IsHittingPlayer(float xDir, float yDir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * xDir + Vector2.up * yDir, 0.6f, playerLayermask);
        if (hit.collider == null)
            return false;
        return true;
    }
}
