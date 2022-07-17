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
    [Header("Animations")]
    public GameObject DiceModel;
    [Header("Input Gathering")]
    public float inputBuffer;
    //INTERNAL VARIABLES
    Rigidbody2D rb;
    int horizontalInput;
    int verticalInput;
    int lastDir;
    public int currentDiceNumber = 1;
    public int direction;
    public int dashCounter;
    EnemyStats selectedEnemy;
    GameObject[] enemies;
    EnemyStats[] enemyStats;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameManager.ticks = 0;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyStats = new EnemyStats[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyStats[i] = enemies[i].GetComponent<EnemyStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        GetInput();
        Movement();
        UpdateDiceNumber();
        Dash();
        Attack();
        //MoveDiceToRightPosition();
        lastDir = direction;
    }
    void GetInput()
    {
        if (timer < inputBuffer)
        {
            verticalInput = 0;
            horizontalInput = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            verticalInput = 1;
            horizontalInput = 0;
            direction = 4;
            timer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            verticalInput = -1;
            horizontalInput = 0;
            direction = 2;
            timer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            horizontalInput = 1;
            verticalInput = 0;
            direction = 1;
            timer = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            horizontalInput = -1;
            verticalInput = 0;
            direction = 3;
            timer = 0;
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
        }
    }
    void DealDamage(int xDirection, int yDirection)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position + new Vector3(0.5f, -0.5f, 0), Vector2.right * xDirection + Vector2.up * yDirection, 0.6f, enemyLayerMask);
        if (hit.collider == null)
            return;
        GameObject target = hit.collider.gameObject;
        if (target.CompareTag("enemy"))
        {
            selectedEnemy = target.GetComponent<EnemyStats>();
            if (selectedEnemy.level <= currentDiceNumber)
            {
                selectedEnemy.Kill();
            }
        }
        else if (target.CompareTag("breakable"))
        {
            BreakableWall slectedWall = target.GetComponent<BreakableWall>();
            if (slectedWall.level <= currentDiceNumber)
            {
                slectedWall.Break();
            }
        }

    }
    void Movement()
    {
        if (!GoingTowardsWall())
        {
            MovePlayer(horizontalInput, verticalInput);
            if (horizontalInput == 1)
            {
                GameManager.ticks++;
                diceNumberManager.RollRight();
                StartCoroutine(AnimateDiceRoll(0, -90, 0, inputBuffer));
            }
            else if (horizontalInput == -1)
            {
                GameManager.ticks++;
                diceNumberManager.RollLeft();
                StartCoroutine(AnimateDiceRoll(0, 90, 0, inputBuffer));
            }
            if (verticalInput == 1)
            {
                GameManager.ticks++;
                diceNumberManager.RollUp();
                StartCoroutine(AnimateDiceRoll(90, 0, 0, inputBuffer));
            }
            if (verticalInput == -1)
            {
                GameManager.ticks++;
                diceNumberManager.RollDown();
                StartCoroutine(AnimateDiceRoll(-90, 0, 0, inputBuffer));
            }
            currentDiceNumber = diceNumberManager.dieSide[1, 1];
            
        }
        else
        {
            direction = lastDir;
        }
        CheckForPlayerDeath();
    }
    void CheckForPlayerDeath()
    {
        for (int i = 0; i < enemyStats.Length; i++)
        {
            if (enemyStats[i].playerHit)
            {
                GameManager.RestartLevel();
            }
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
    IEnumerator AnimateDiceRoll(float xRot, float yRot, float zRot, float rotTime)
    {
        const float ROTATION_INTERVALS = 0.01f;
        xRot /= 0.15f / ROTATION_INTERVALS;
        yRot /= 0.15f / ROTATION_INTERVALS;
        zRot /= 0.15f / ROTATION_INTERVALS;
        for (int i = 0; i < 15; i++)
        {
            yield return new WaitForSeconds(0.004f);
            DiceModel.transform.Rotate(xRot, yRot, zRot, Space.World);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "jumpPad")
        {
            UseBouncePad(collision);
        }
        if (collision.tag == "button")
        {
            Button button = collision.GetComponent<Button>();
            if (currentDiceNumber >= button.level)
                button.OpenDoor();
        }
        if (collision.tag == "void" || collision.tag == "breakable")
        {
            GameManager.RestartLevel();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
