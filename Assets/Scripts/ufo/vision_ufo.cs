using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vision_ufo : MonoBehaviour
{

    public float rotationSpeed;
    public float visionDistance;
    public LineRenderer lineofsight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineofsight.SetPosition(0, transform.position);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, visionDistance);
        if (hit.collider != null)
        {   if (hit.collider.tag == "Player")
            {
            }
            Debug.DrawLine(transform.position, hit.point, Color.red);
            lineofsight.SetPosition(1, hit.point);
            lineofsight.startColor = Color.red;
            lineofsight.startColor = Color.red;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * visionDistance, Color.green);
            lineofsight.SetPosition(1, transform.position + transform.right * visionDistance);
            lineofsight.startColor = Color.green;
            lineofsight.startColor = Color.green;
        }
    }
}
