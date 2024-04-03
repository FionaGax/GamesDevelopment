using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeFireTrap : MonoBehaviour
{
    public float initalPointsDeduction;
    public float continuosPointDeduction;
    public bool playerIsInTrap;
    public bool initialPointsDeducted;
    public bool pointsBeingDeductedContinuously;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!initialPointsDeducted)
            {
                playerIsInTrap = true;
                gameManager.DecrementHealth(initalPointsDeduction);
                initialPointsDeducted = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsInTrap = false;
            initialPointsDeducted = false;
            pointsBeingDeductedContinuously = false;
            CancelInvoke();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!pointsBeingDeductedContinuously)
        {
            DeductPointsContinuously();
        }
    }

    private void DeductPointsContinuously()
    {
        pointsBeingDeductedContinuously = true;
        InvokeRepeating("DeductPoints", 0f, 1.5f);
    }

    private void DeductPoints()
    {
        gameManager.DecrementHealth(continuosPointDeduction);
    }
}
