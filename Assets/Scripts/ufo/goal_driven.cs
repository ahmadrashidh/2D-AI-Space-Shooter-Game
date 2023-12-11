using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goal_driven : MonoBehaviour
{
    public float speed = 5f;
    int direction;
    float startingX;
    public float minX, maxX;
    public LayerMask playerLayer;


    public float detectionRadius = 4f;
    public float movementSpeed = 2f; // Speed at which the enemy moves towards the player
    public Transform playerTransform; // Reference to the player's Transform
    private SpriteRenderer enemyRenderer; // Reference to the SpriteRenderer component
    private Color defaultColor; // Store the default color of the enemy
    private bool isChasing = false; // Flag to indicate whether the enemy is chasing the player
    bool powerBoosterDetected = false;
    public Transform powerBoosterTransform;
    // Start is called before the first frame update
    void Start()
    {
        startingX = transform.position.x;
        direction = 1;
        enemyRenderer = GetComponent<SpriteRenderer>();
        defaultColor = enemyRenderer.color;


    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (!isChasing)
        {
            patrol();
            CheckForPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }



    void patrol()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime * direction);
        if (transform.position.x > maxX || transform.position.x < minX)
        {
            direction *= -1;
        }
    }

    void CheckForPlayer()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);


        foreach (Collider2D obj in detectedObjects)
        {
            if (obj.CompareTag("Player"))
            {
                enemyRenderer.color = Color.green; // Change the enemy color to green when chasing
                playerTransform = obj.transform;
                //playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Get player's transform
                CheckHealth();
                //StartChasing();
                break;
            }
            if (obj.CompareTag("PowerBooster"))
            {
                Debug.Log("Powerbooster detected");
                powerBoosterDetected = true;
                powerBoosterTransform = obj.transform;
            }


        }
    }
    void CheckHealth()
    {
        var damagable = playerTransform.GetComponent<Damagable>();
        if (damagable != null)
        {
            Debug.Log(damagable.Health);
            if (damagable.Health < 60)
            {
                if (powerBoosterDetected)
                {
                    //powerBoosterTransform = GameObject.FindGameObjectWithTag("PowerBooster").transform; // Get player's transform
                    AttackPowerBooster();
                }// Player's health is less than 60, so attack power booster

            }
            else
            {
                // Player's health is 60 or higher, so attack enemy
                isChasing = true; // Set the chasing flag to true
                StartChasing();
            }
        }
    }
    void AttackPowerBooster()
    {
        if (powerBoosterTransform != null)
        {
            enemyRenderer.color = Color.blue;
            transform.position = Vector2.MoveTowards(transform.position, powerBoosterTransform.position, movementSpeed * Time.deltaTime);
            // Change the 'movementSpeed' value as needed for the speed of movement
        }

    }
    void AttackPlayer()
    {
        if (playerTransform != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
            // Change the 'movementSpeed' value as needed for the speed of movement
        }

    }
    void StartChasing()
    {
        enemyRenderer.color = Color.green; // Change the enemy color to green when chasing
        isChasing = true; // Set the chasing flag to true
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Get player's transform
    }


    private void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }



}



