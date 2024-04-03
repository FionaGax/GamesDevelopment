using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>()
            .spawnPoint.position;
    }
}
