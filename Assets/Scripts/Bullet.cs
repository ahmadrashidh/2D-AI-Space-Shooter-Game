using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public int damage = 5;
    public float maxDist = 10;

    private Vector2 startPos;
    private float conqueredDist;
    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        startPos = transform.position;
        rb2d.velocity = transform.up * speed;
    }

    private void Update()
    {
        conqueredDist = Vector2.Distance(transform.position, startPos);
        if(conqueredDist >= maxDist)
        {
            Disable();
        }
    }

    private void Disable()
    {
        rb2d.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided:" + collision.name);

        var damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            damagable.Hit(damage);
        }
        Disable();
    }

}
