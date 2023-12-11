using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUFO : MonoBehaviour
{
    public float rotationSpeed;
    public float visionDistance;
    public LineRenderer lineofsight;
    private bool canShoot = false;
    private float currentDelay = 0;
    private float reloadDelay = 1;
    public ObjectPool bulletPool;
    private bool canRotate = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }

        if (canRotate)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }



        lineofsight.SetPosition(0, transform.position);
        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, visionDistance);
        if (hit.collider != null)
        {   if (hit.collider.tag == "Player")
            {
                canRotate = false;
                Shoot();
                Debug.DrawLine(transform.position, hit.point, Color.red);
                lineofsight.SetPosition(0, hit.point);
                lineofsight.startColor = Color.red;
            }
            
        }
        else
        {
            canRotate = true;
            Debug.DrawLine(transform.position, transform.position + transform.up * visionDistance, Color.green);
            lineofsight.SetPosition(0, transform.position + transform.up * visionDistance);
            lineofsight.startColor = Color.green;
        }
    }


    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = reloadDelay;

            GameObject bullet = bulletPool.CreateObject();
            bullet.transform.position = transform.position;
            bullet.transform.localRotation = transform.rotation;
            bullet.GetComponent<Bullet>().Initialize();

        }
    }
}
