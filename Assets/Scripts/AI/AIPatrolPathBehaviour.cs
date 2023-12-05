using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolPathBehaviour : AIBehaviour
{
    public PatrolPath patrolPath;

    [Range(0.1f, 1)]
    public float arriveDistance = 1;

    public float waitTime = 0.5f;

    [SerializeField]
    private bool isWaiting = false;

    [SerializeField]
    Vector2 currentPatrolTarget = Vector2.zero;
    bool isInit = false;

    private int currIndex = -1;


    private void Awake()
    {
        if (patrolPath == null)
            patrolPath = GetComponentInChildren<PatrolPath>();
    }

    public override void PerformAction(SpaceshipController spaceship, AIDetector detector)
    {
        if (!isWaiting)
        {
            if (patrolPath.Length < 2)
                return;

            if (!isInit)
            {
                var currentPathPoint = patrolPath.GetClosestPathPoint(spaceship.transform.position);
                this.currIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInit = true;
            }

            Debug.Log("ArriveDist:" + Vector2.Distance(spaceship.transform.position, currentPatrolTarget));
            if(Vector2.Distance(spaceship.transform.position, currentPatrolTarget) < arriveDistance)
            {
                isWaiting = true;
                StartCoroutine(WaitCoroutine());
                return;
            }

            Vector2 directionToGo = currentPatrolTarget - (Vector2)spaceship.transform.position;
            var dotProduct = Vector2.Dot(spaceship.transform.up, directionToGo.normalized);
            Debug.Log("DotProduct:" + dotProduct);
            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(spaceship.transform.up, directionToGo.normalized);
                int rotationResult = crossProduct.z >= 0 ? -1 : 1;
                spaceship.HandleMoveBody(new Vector2(rotationResult, 1));
            } else
            {
                spaceship.HandleMoveBody(Vector2.up);
            }
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        var nextPathPoint = patrolPath.GetNextPathPoint(currIndex);
        Debug.Log("NextPathPoint:" + nextPathPoint.Index);
        currentPatrolTarget = nextPathPoint.Position;
        currIndex = nextPathPoint.Index;
        isWaiting = false;
    }
}
