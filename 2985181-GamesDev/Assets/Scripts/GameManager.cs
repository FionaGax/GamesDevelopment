using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float health;

    public float score;

    public Transform spawnPoint;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Start").transform;
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
    }
}
