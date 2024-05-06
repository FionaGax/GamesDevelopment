using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameUI : MonoBehaviour
{
    public TMP_Text scoreText;

    public TMP_Text healthText;

    public TMP_Text livesText;
    public TMP_Text lostGame;
    public TMP_Text wonGame;

    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
        // Don't show the game over text
        lostGame.enabled = false;
        
        // Don't show the won game text
        wonGame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameManager.score;
        healthText.text = "Health: " + gameManager.health;
        livesText.text = "Lives: " + gameManager.lives;

        if (gameManager.gameLost)
        {
            lostGame.enabled = true; // Show the game over text
        }

        if (gameManager.gameWon)
        {
            wonGame.enabled = true; // Show game won text
        }
        
        
    }
}
