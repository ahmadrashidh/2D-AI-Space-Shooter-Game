using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();

    // Update is called once per frame
    void Update()
    {
        getMovement();
        getShootingInput();
        
    }

    private void getShootingInput()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnShoot?.Invoke();
        }
    }

    private void getMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
    }

}
