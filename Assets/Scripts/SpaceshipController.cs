using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class SpaceshipController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private Vector2 movementVector;
    public SpaceshipMovementData movementData;

    // Movement
    public float currentSpeed = 0;
    public float currentForewardDirection = 1;

    // Shooting
    public List<Transform> shooters;
    public GameObject bulletPrefab;
    public float reloadDelay = 1;

    private bool canShoot = true;
    private Collider2D[] pColliders;
    private float currentDelay = 0;

    //Object Pooling
    private ObjectPool bulletPool;

    [SerializeField]
    private int bulletPoolCount = 10;

    private void Awake()
    {
        pColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bulletPool.Initialize(bulletPrefab, bulletPoolCount);
    }


    public void HandleMoveBody(Vector2 moveVector)
    {
        this.movementVector = moveVector;
        CalculateSpeed(movementVector);

        if(movementVector.y > 0)
        {
            currentForewardDirection = 1;
        } else if(movementVector.y < 0)
        {
            currentForewardDirection = 0;
        }
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if(Math.Abs(movementVector.y) > 0)
        {
            currentSpeed += movementData.acceleration * Time.deltaTime;
        } else
        {
            currentSpeed -= movementData.decceleration * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, movementData.maxSpeed);
    }

    public void HandleShoot()
    {
        Debug.Log("Shooting");
        Shoot();
    }

    private void Update()
    {
        if(canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if(currentDelay <= 0)
            {
                canShoot = true;
            }
        }
        
    }

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            foreach(var shooter in shooters)
            {
                //GameObject bullet = Instantiate(bulletPrefab);
                GameObject bullet = bulletPool.CreateObject();
                bullet.transform.position = shooter.position;
                bullet.transform.localRotation = shooter.rotation;
                bullet.GetComponent<Bullet>().Initialize();
                foreach(var collider in pColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * currentForewardDirection * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
    }

    public void rotate(Vector2 moveVector)
    {

        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -moveVector.x * movementData.rotationSpeed * Time.fixedDeltaTime));
    }
}


