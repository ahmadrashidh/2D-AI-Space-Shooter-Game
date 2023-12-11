using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detection_collision : MonoBehaviour
{
    public float speed = 1.0f; 
    public float amplitude = 3.0f; 
    public float frequency = 1.0f; 
    public float patrolRange = 10.0f; 
    public float minY = -4.0f; 
    public float maxY = 4.0f; 
    public float leftBoundary = -8.0f; 
    public float rightBoundary = 9.0f; 
    private Animator anim;


    public float detectionRadius = 4f;
    public LayerMask playerLayer; 
    public float movementSpeed = 2f; 

    private float startXPos;
    private bool movingLeft = true; 
    private float timer = 0.0f;

    public Transform playerTransform; 
    private SpriteRenderer enemyRenderer; 
    private Color defaultColor; 
    private bool isChasing = false; 

    private void Start()
    {
        startXPos = transform.position.x;
        enemyRenderer = GetComponent<SpriteRenderer>();
        defaultColor = enemyRenderer.color;
        anim = GetComponent<Animator>();
    }

    

    private void Update()
    {
        if (!isChasing)
        {
            MoveEnemy();
            CheckForPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    void MoveEnemy()
    {
        float patrolX = CalculatePatrolX();
        float newYPos = Mathf.Lerp(minY, maxY, Mathf.InverseLerp(-1, 1, Mathf.Sin(timer * frequency)));
        float newXPos = Mathf.Clamp(patrolX, leftBoundary, rightBoundary);

        transform.position = new Vector2(newXPos, newYPos);
        timer += Time.deltaTime * speed;
        enemyRenderer.color = defaultColor;

        if (HasReachedPatrolBoundary(patrolX))
        {
            movingLeft = !movingLeft;
        }
    }
    float CalculatePatrolX()
    {
        float direction = movingLeft ? -1.0f : 1.0f;
        return startXPos + direction * Mathf.PingPong(Time.time * speed, patrolRange);
    }

    bool HasReachedPatrolBoundary(float patrolX)
    {
        return (patrolX <= startXPos - patrolRange && movingLeft) || (patrolX >= startXPos && !movingLeft);
    }
    
    void CheckForPlayer()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (Collider2D obj in detectedObjects)
        {
            if (obj.CompareTag("Player"))
            {
                StartChasing();
                break;
            }


        }
    }

    void StartChasing()
    {
        enemyRenderer.color = Color.green; 
        isChasing = true; 
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void ChasePlayer()
    {
        if (playerTransform != null)
        {
            if (transform.position.x < leftBoundary || transform.position.x > rightBoundary || transform.position.y < minY || transform.position.y > maxY)
            {
                isChasing = false; // Stop chasing if beyond boundaries
                return;
            }
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, movementSpeed * Time.deltaTime);
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy Collision:" + collision.gameObject.name);
        Debug.Log("Enemy collided with Player");

        if (collision.gameObject.CompareTag("Player"))
        {   
            Death();
            var damagable = collision.GetComponent<Damagable>();
            if (damagable != null)
            {
                damagable.Hit(40);
                Debug.Log(damagable.Health);
            }
           
        }
    }

    public void Death()
    {
        anim.Play("destroy");
        Destroy(this.gameObject, 0.15f);
    }
}
