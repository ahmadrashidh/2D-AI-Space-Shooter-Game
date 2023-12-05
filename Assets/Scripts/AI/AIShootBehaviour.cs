using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    public float fieldOfVisionForShooting = 60;

    public override void PerformAction(SpaceshipController spaceship, AIDetector detector)
    {
        if(TargetInFOV(spaceship, detector))
        {
            spaceship.HandleMoveBody(Vector2.zero);
            spaceship.HandleShoot();
        }

        spaceship.HandleMoveBody(detector.Target.position);
    }

    private bool TargetInFOV(SpaceshipController spaceship, AIDetector detector)
    {
        var direction = detector.Target.position - spaceship.transform.position;
        if(Vector2.Angle(spaceship.transform.up, direction) <  fieldOfVisionForShooting / 2)
        {
            return true;
        }

        return false;
    }
}
