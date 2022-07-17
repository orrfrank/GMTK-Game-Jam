using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int ticks = 0;
    static GameObject player;
    static GameManager gameManager;
    public static float timer;
    public static bool isDead = false;
    private void Start()
    {
        isDead = false;
        timer = 0;
        ticks = 0;
        gameManager = GetComponent<GameManager>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public static void RestartLevel()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
        timer = 0;
        isDead = true;
        gameManager.StartCoroutine(DeathAnimation());
        
    }
    static IEnumerator DeathAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
