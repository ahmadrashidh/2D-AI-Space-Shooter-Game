using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour
{
    public float moveSpeed = 3;
    public float deathzone = -41;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * moveSpeed) * Time.deltaTime;
        DeactivateOnDeathZone();
    }

    void DeactivateOnDeathZone()
    {
        if (transform.position.x < deathzone)
        {
            gameObject.SetActive(false);
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BoosterCollided:" + collision.name);

        var damagable = collision.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.Health += 30;
        }
        Disable();
    }
}
