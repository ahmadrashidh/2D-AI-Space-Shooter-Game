using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    public List<Transform> patrolPoints = new List<Transform>();

    public int Length { get => patrolPoints.Count;  }

    [Header("Gizmos parameters")]
    public Color pointsColor = Color.white;
    public float pointSize = 1;
    public Color lineColor = Color.magenta;

    public struct PathPoint
    {
        public int Index;
        public Vector2 Position;
    }

    public PathPoint GetClosestPathPoint(Vector2 shipPosition)
    {
        var minDist = float.MaxValue;
        var index = -1;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            var tempDist = Vector2.Distance(shipPosition, patrolPoints[i].position);
            if(tempDist < minDist)
            {
                minDist = tempDist;
                index = i;
            }
        }

        return new PathPoint { Index = index, Position = patrolPoints[index].position };
    }

    public PathPoint GetNextPathPoint(int index)
    {
        Debug.Log("PatrolPointsCount:" + patrolPoints.Count);
        var newIndex = index + 1 >= patrolPoints.Count ? 0 : index + 1;
        return new PathPoint { Index = newIndex, Position = patrolPoints[newIndex].position };
    }


    private void OnDrawGizmos()
    {
        if(patrolPoints.Count == 0)
        {
            return;
        }

        for(int i = patrolPoints.Count - 1; i > 0; i--)
        {
            if (patrolPoints[i] == null)
                return;

            Gizmos.color = pointsColor;
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize);

            if(patrolPoints.Count == 1 || i == 0)
            {
                return;
            }

            Gizmos.color = lineColor;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i - 1].position);

            if(patrolPoints.Count > 2 && i == patrolPoints.Count - 1)
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position);
            }
        }
    }
}
