using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;

    public float score;

    public float lives = 3;
    
    public bool gameLost = false;
    public bool gameWon = false;
    public Transform playerTransform;

    public Transform spawnPoint;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Start").transform;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore(float pointsToAdd)
    {
        score += pointsToAdd;
    }

    public void DecrementHealth(float reduceHealthBy)
    {
        health -= reduceHealthBy;
    
        // Check if health has reached 0 or less
        if (health <= 0)
        {
            // Prevent health from going into negative
            health = 0;

            // Only decrease lives if game is not already lost
            if (!gameLost)
            {
                lives -= 1;

                // Check if lives have reached 0 or less
                if (lives <= 0)
                {
                    // Prevent lives from going into negative
                    lives = 0;
                    gameLost = true;
                }
                else
                {
                    health = 100;
                    RespawnPlayer();
                }
            }
        }
    }


    private void RespawnPlayer()
    {
        playerTransform.position = spawnPoint.position;
    }
}
