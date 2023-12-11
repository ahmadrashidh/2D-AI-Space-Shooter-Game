using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{

    public string targetTag = "Player";
    public Agent agentData;
    private List<string> obstacleTag = new List<string>() { "Obstacle", "Boundary" };

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject;
        string tag = target.tag;

        Debug.Log("EnterColliderTag:" + tag);
        // If not Player
        if (obstacleTag.Contains(tag))
        {
            SpaceshipController controller = this.GetComponentInParent<SpaceshipController>();
            Debug.Log("NotPlayer_MoveRotation" + controller);
            controller.HandleMoveBody(Vector2.left);
            return;
        }

    }

    private void MoveAway(GameObject collider)
    {
        SpaceshipController controller = this.GetComponentInParent<SpaceshipController>();
        Vector2 dir = transform.position - collider.transform.position;
        dir = -dir.normalized;
        controller.HandleMoveBody(dir);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        GameObject target = other.gameObject;
        string tag = target.tag;

        Debug.Log("ColliderTag:" + tag);
        
        
        if(obstacleTag.Contains(tag))
        {
            MoveAway(other.gameObject);
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
