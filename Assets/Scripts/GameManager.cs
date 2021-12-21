using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameIsOver;
    public GameObject gameOverUI;
    // Update is called once per frame
    public Animation ani;
    private void Start()
    {
        ani = GetComponent<Animation>();
        gameIsOver = false;
    }
    void Update()
    {
        if (gameIsOver)
            return;
        if(PlayerStats.Lives<= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Debug.Log("Game Over!");
    }

}
