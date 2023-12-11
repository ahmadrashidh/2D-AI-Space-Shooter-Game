using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCasting : MonoBehaviour
{
    public float detectionRadius = 5f;
    public LayerMask playerLayer; // Assign the layer where the player is placed
    public float attackCooldown = 2f;
    private float nextAttackTime = 0f;
    public GameObject attackPrefab; // The projectile or attack object
    public Transform attackPoint; // The point from which the attack is spawned

    private SpriteRenderer enemyRenderer; // Reference to the SpriteRenderer component
    private Color defaultColor; // Store the default color of the enemy

    private void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>();
        defaultColor = enemyRenderer.color;
    }

    private void Update()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D obj in detectedObjects)
        {
            if (obj.CompareTag("Player"))
            {
                // Change the enemy color to green when the player is detected
                enemyRenderer.color = Color.green;

                // Check if enough time has passed to perform the attack again
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackCooldown; // Set the next attack time
                }
            }
        }
    }

    void Attack()
    {
        // collide and destroy
        // //on death()
        //
        // 
    }

    private void OnDrawGizmos()
    {
        // Visualize the detection radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Reset the enemy color to its default color when the player exits the detection radius
            enemyRenderer.color = defaultColor;
        }
    }
}

