using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{

    public string targetTag = "Player";
    public Agent agentData;

    private void Awake()
    {
        agentData = GetComponentInParent<Agent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        GameObject target = other.gameObject;
        string tag = target.tag;

        Debug.Log("ColliderTag:" + tag);
        // If not Player
        if(tag.Equals(targetTag) == false)
        {
            return;
        }

        // If player, ray cast in the direction to confirm
        Vector2 agentPosition = gameObject.transform.position;
        Vector2 targetPosition = target.transform.position;
        Vector2 direction = targetPosition - agentPosition;

        RaycastHit2D hit = Physics2D.Raycast(agentPosition, direction);

        Debug.DrawRay(agentPosition, direction);

        if(hit.collider != null && hit.collider.gameObject.tag.Equals(targetTag))
        {
            agentData.target = target;
            return;
        }


        agentData.target = null;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if(agentData.target != null && collision.gameObject == agentData.target)
        {
            agentData.target = null;
        }
    }
}
