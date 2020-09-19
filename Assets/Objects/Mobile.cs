using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class Mobile : Object
{
    public int unitSpeed = 1;
    public NavMeshAgent agent;
    override public void Start()
    {
        base.Start();
        setupAgent();
    }

    public void setupAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = unitSpeed;
    }

    virtual public void Move(Vector3 target){
        agent.destination = target;
    }

    public override string getStringStats()
    {
        string str = base.getStringStats();
        str = str + "Speed:" + unitSpeed.ToString() + "\n";

        return str;
    }
}
