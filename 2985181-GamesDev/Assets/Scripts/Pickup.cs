using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float scoreValue;

    public GameManager gameManager;

    public GameObject collectedEffect;
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
        if (col.CompareTag("Player")) // Check what tag the collider has, if it's the player then increment score
        {
            gameManager.IncrementScore(scoreValue);
            Instantiate(collectedEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
