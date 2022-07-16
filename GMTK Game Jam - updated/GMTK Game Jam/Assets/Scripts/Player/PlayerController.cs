using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dice Number Generation")]
    public DiceNumberManager diceNumberManager;
    [Header("Physics Settings")]
    public LayerMask whatIsWall;
    public LayerMask enemyLayerMask;
    //INTERNAL VARIABLES
    Rigidbody2D rb;
    int horizontalInput;
    int verticalInput;
    public int currentDiceNumber = 1;
    public int direction;
    public int dashCounter;
    EnemyStats selectedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        UpdateDiceNumber();
        Dash();
        Attack();
    }
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            verticalInput = 1;
            horizontalInput = 0;
            direction = 4;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            verticalInput = -1;
            horizontalInput = 0;
            direction = 2;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            horizontalInput = 1;
            verticalInput = 0;
            direction = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            horizontalInput = -1;
            verticalInput = 0;
            direction = 3;
        }
        else
        {
            verticalInput = 0;
            horizontalInput= 0;
        }
    }
    void UpdateDiceNumber()
    {
        KeyCode[] keyCodes = new KeyCode[] { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };
        for (int i = 0; i < keyCodes.Length; ++i)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                currentDiceNumber = i;
            }
        }
    }
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCounter > 0)
        {
            if (direction == 1)
            {
                MovePlayer(currentDiceNumber, 0);
            }
            else if (direction == 2)
            {
                MovePlayer(0, -currentDiceNumber);
            }
            else if (direction == 3)
            {
                MovePlayer(-currentDiceNumber, 0);
            }
            else if (direction == 4)
            {
                MovePlayer(0, currentDiceNumber);
            }
            dashCounter--;
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (direction == 1)
            {
                DealDamage(1, 0);
            }
            else if (direction == 2)
            {
                DealDamage(0, -1);
            }
            else if (direction == 3)
            {
                DealDamage(-1, 0);
            }
            else if (direction == 4)
            {
                DealDamage(0, 1);
            }
            dashCounter--;
        }
    }
    void DealDamage(int xDirection, int yDirection)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.right * xDirection + Vector2.up * yDirection, 0.6f, enemyLayerMask);
        if (hit == null)
            return;
        selectedEnemy = hit.collider.gameObject.GetComponent<EnemyStats>();
        Debug.DrawRay(transform.position, Vector2.right * xDirection + Vector2.up * yDirection);
        if (selectedEnemy.level <= currentDiceNumber)
        {
            selectedEnemy.Kill();
            Debug.Log("Enemy Found And Killed");
        }
        else
        {
            Debug.Log("Enemy Not Found");
        }
    }
    void Movement()
    {
        if (!GoingTowardsWall())
        {
            MovePlayer(horizontalInput, verticalInput);
            if (horizontalInput == 1)
            {
                diceNumberManager.RollRight();
            }
            else if (horizontalInput == -1)
            {
                diceNumberManager.RollLeft();
            }
            if (verticalInput == 1)
            {
                diceNumberManager.RollUp();
            }
            if (verticalInput == -1)
            {
                diceNumberManager.RollDown();
            }
            currentDiceNumber = diceNumberManager.dieSide[1, 1];
        }
    }
    void MovePlayer(int xMove, int yMove)
    {
        rb.position += new Vector2(xMove, yMove);
    }
    bool GoingTowardsWall()
    {
        bool isBonking = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.right * horizontalInput + Vector2.up * verticalInput, 0.51f, whatIsWall);
        return isBonking;
    }
    void UseBouncePad(Collider2D collision)
    {
        BouncePad bouncePad = collision.GetComponent<BouncePad>();
        int jumpDirection = bouncePad.direction;
        if (jumpDirection == 1)
        {
            MovePlayer(1 * currentDiceNumber, 0);
        }
        else if (jumpDirection == 2)
        {
            MovePlayer(0, -1 * currentDiceNumber);
        }
        else if (jumpDirection == 3)
        {
            MovePlayer(-1 * currentDiceNumber,0);
        }
        else if (jumpDirection == 4)
        {
            MovePlayer(0, 1 * currentDiceNumber);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "jumpPad")
        {
            UseBouncePad(collision);
        }
        
    }
}
