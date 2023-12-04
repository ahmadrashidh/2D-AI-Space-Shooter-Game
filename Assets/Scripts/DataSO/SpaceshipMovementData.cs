using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpaceshipMovementData", menuName = "Data/SpaceshipMovementData")]
public class SpaceshipMovementData : ScriptableObject
{
    public float maxSpeed = 10;
    public float rotationSpeed = 100;
    public float acceleration = 70;
    public float decceleration = 50;
}
