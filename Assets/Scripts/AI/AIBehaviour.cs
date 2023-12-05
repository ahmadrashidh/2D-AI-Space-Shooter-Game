using System;
using UnityEngine;

public abstract class AIBehaviour : MonoBehaviour
{
    public abstract void PerformAction(SpaceshipController spaceship, AIDetector detector);
}