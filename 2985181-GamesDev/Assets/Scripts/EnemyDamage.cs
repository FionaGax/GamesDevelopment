using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damageAmount; // Amount of damage the enemy will cost the player
    private GameManager gameManager; // Reference to the GameManager

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.DecrementHealth(damageAmount);
        }
    }
}