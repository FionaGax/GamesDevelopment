using System;
using System.Collections;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        DetectPlayer,
        Chasing,
        AggroIdle,
    }

    public State enemyAIState;
    public float moveSpeed; //speed of the enemy when patrolling

    public float maxSpeed;

    public float chaseSpeed; //speed of the enemy when chasing the player

    private float _speed; //current speed of the enemy

    public float detectedPlayerTime; //time the enemy will stay in detect mode before beginning chasing player

    public float aggroTime; //used if player is out of detection radius - enemy will stay in aggro mode for this time, and can immediately resume chasing before going back to idle

    public bool playerDetected; //if the player is detected

    public bool aggro; //if the enemy is in an aggro state
    private Rigidbody2D _myRb;
    public Transform player;
    public Animator enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemyAIState = State.Idle;
        _myRb = GetComponent<Rigidbody2D>(); // look for a component called Rigidbody2D and assign it to myRb
        player = GameObject.Find("Player").transform;
        enemy = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float directionToPlayer = Mathf.Sign(player.position.x - transform.position.x);
        switch (enemyAIState)
        {
            case State.Idle:
                _speed = 0;
                enemy.SetTrigger("enemyIdle");
                //do nothing
                break;
            case State.Patrol:
                _speed = moveSpeed;
                // enemy.SetTrigger("enemyPatrol");
                //move the enemy
                break;
            case State.DetectPlayer:
                _speed = 0;
                //when player is detected, start a timer to chase the player
                break;
            case State.Chasing:
                //chases the player
                _speed = directionToPlayer * chaseSpeed;
                enemy.SetTrigger("enemyChase");
                break;
            case State.AggroIdle:
                //stays in aggro mode for a set time before going back to idle
                _speed = 0;
                enemy.SetTrigger("enemyAggro");
                break;
        }
        
        _myRb.velocity = new Vector2(_speed, _myRb.velocity.y);
// Flip the sprite based on the direction of movement
        if (_speed < 0.1f) // If the enemy is moving right
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_speed > -0.1f) // if the enemy is moving to the left
        {
            transform.localScale = new Vector3(-1, 1, 1); // set the scale of the enemy to -1,1,1
        }

       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            if (aggro == false)
            {
                StopCoroutine("DetectTimer"); //need to stop the Coroutine in case it was previously started e.g. if the player quickly enters and exits the detection radius
                StartCoroutine("DetectTimer");
            }
            if (aggro == true)
            {
                playerDetected = true;
                enemyAIState = State.Chasing;
            }
        }

    }

    IEnumerator DetectTimer()
    {
        enemyAIState = State.DetectPlayer;
        yield return new WaitForSeconds(detectedPlayerTime);
        if (playerDetected == true)
        {
            aggro = true;
            enemyAIState = State.Chasing;
        }
        if (playerDetected == false)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
            if (aggro == true)
            {
                StopCoroutine("AggroTimer");
                StartCoroutine("AggroTimer");
            }
        }
    }

    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTime);
        if (playerDetected == false & aggro == false)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }
        if (playerDetected == false & aggro == true)
        {
            enemyAIState = State.AggroIdle;
        }
        yield return new WaitForSeconds(aggroTime*2);
        if (playerDetected == false)
        {
            aggro = false;
            enemyAIState = State.Idle;
        }

    }
}