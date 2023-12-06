using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void onEnable()
    {
        agent.target = null;
        base.onEnable();
        
    }

    public override void Update()
    {
        if (agent.target != null)
        {
            return;
        } else
        {
            controller.HandleMoveBody(Vector2.down);
            Rigidbody2D rb2d = controller.gameObject.GetComponent<Rigidbody2D>();
            rb2d.rotation = 0;
        }

        




    }

}
