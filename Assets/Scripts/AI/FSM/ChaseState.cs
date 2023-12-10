using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public override void onEnable()
    {
        base.onEnable();
    }

    public override void onDisable()
    {
        base.onDisable();
    }

    public override void Update()
    {
        if(agent.target == null)
        {
            return;
        }

        Rigidbody2D rb2d = controller.gameObject.GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = false;
        Vector2 direction = agent.target.transform.position - gameObject.transform.position;
        direction.Normalize();
        controller.HandleMoveBody(gameObject.transform.InverseTransformDirection(direction));
        controller.HandleShoot();

    }
}
