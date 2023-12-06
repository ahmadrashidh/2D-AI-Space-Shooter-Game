using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class State : MonoBehaviour
{
    public Agent agent;
    public List<Transition> transitions = new List<Transition>();

    protected SpaceshipController controller;

    private void Awake()
    {
        agent = GetComponent<Agent>();
        controller = GetComponent<SpaceshipController>();

    }

public virtual void onEnable()
    {

    }

    public virtual void onDisable()
    {

    }

    public virtual void Update()
    {

    }

    private void FixedUpdate()
    {
        foreach(Transition transition in transitions)
        {
            if (transition.condition.test(agent))
            {
                
                transition.targetState.enabled = true;
                
                this.enabled = false;
                Debug.Log("ConditionPassed:" + this + "->" + transition.targetState);
            }
        }
    }

    [Serializable]
    public struct Transition
    {
        public Condition condition;
        public State targetState;
    }
}
