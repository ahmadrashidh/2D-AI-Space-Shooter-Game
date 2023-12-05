using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemyAI : MonoBehaviour
{
    [SerializeField]
    private AIBehaviour shootBehaviour, patrolBehaviour;

    [SerializeField]
    private SpaceshipController spaceship;

    [SerializeField]
    private AIDetector detector;

    private void Awake()
    {
        detector = GetComponentInChildren<AIDetector>();
        spaceship = GetComponentInChildren<SpaceshipController>();
    }

    private void Update()
    {
        if (detector.TargetVisible)
        {
            shootBehaviour.PerformAction(spaceship, detector);
        } else
        {
            patrolBehaviour.PerformAction(spaceship, detector);
        }
    }
}
