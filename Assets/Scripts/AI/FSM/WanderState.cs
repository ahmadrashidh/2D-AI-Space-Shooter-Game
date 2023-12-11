using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : State
{
    public float angleModifier = 1;
    public bool isWandering = false;
    public float wanderingSpeed = 0.5f;

    Vector2? direction = null;

    public override void Update()
    {
        base.Update();
        if (isWandering)
        {
            if (direction.HasValue)
            {
                controller.currentSpeed = 20;
                controller.currentForewardDirection = 1;
                Debug.Log("WanderingUp");
                controller.HandleMoveBody(direction.Value.normalized * Vector2.up);
            }
            return;
        }
        isWandering = true;
        StartCoroutine(WanderAround());
    }

    IEnumerator WanderAround()
    {
        direction = RotateAgent();
        yield return new WaitForSeconds(2);
        StartCoroutine(LookAround());
    }

    private Vector2 RotateAgent()
    {
        float wanderOrientation = Random.Range(-70f, 70f) * angleModifier;
        var newRotation = Quaternion.AngleAxis(wanderOrientation, Vector2.up);
        var rotationDirection = newRotation * Vector2.left;
        controller.HandleMoveBody(rotationDirection);
        return rotationDirection;
    }

    IEnumerator LookAround()
    {
        direction = null;
        controller.HandleMoveBody(Vector2.down);
        var rotationDirection = RotateAgent();
        direction = rotationDirection;
        yield return new WaitForSeconds(3);
        isWandering = false;

    }
}
