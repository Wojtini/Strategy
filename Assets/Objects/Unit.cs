﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Mobile
{
    [Header("Unit Stats")]
    public int attackDamage = 1;
    public float attackCooldown = 0f;
    public float attackSpeed = 1f;
    public int armor = 0;
    public int attackRange = 2;
    public int spotRange = 20;

    //temporary
    

    override public void Start()
    {
        base.Start();
        PlayerControl.allUnits.Add(this);
    }

    override public void Update()
    {
        base.Update();
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown <= 0)
            {
                agent.speed = this.unitSpeed;
            }
        }
    }

    public Object CheckRange(float range)
    {
        Object closestEnemy = null;
        float closestDistance = range;
        foreach (Object obj in Map.instance.allObjects)
        {
            if (obj.Faction != this.Faction)
            {
                float dist = Vector3.Distance(this.transform.position, obj.transform.position);
                bool test = dist <= closestDistance;
                if (dist <= closestDistance)
                {
                    closestDistance = dist;
                    closestEnemy = obj;
                }
            }
        }
        return closestEnemy;
    }

    public void TargetObject(Object obj,bool isRepairing = false)
    {
        if (attackCooldown <= 0)
        {
            if (!isRepairing)
            {
                Debug.Log("Atakuje: " + obj);
                obj.dealDamage(attackDamage);
            }
            else
            {
                Debug.Log("Naprawiam: " + obj);
                obj.healDamage(attackDamage);
            }
            attackCooldown = attackSpeed;
            //Widoczki + blokowanie mozliwosci ruchu podczas i w trakcie trwania cd ataku
            agent.speed = 0;
            this.gameObject.transform.LookAt(obj.transform);
        }
    }

    override public void Destroy()
    {
        base.Destroy();
        PlayerControl.allUnits.Remove(this);
    }

    public override string getStringStats()
    {
        string str = base.getStringStats();
        str = str + "Atk: " + attackDamage.ToString() + "\n";
        str = str + "Armor: " + armor.ToString() + "\n";
        str = str + "Range: " + attackRange.ToString() + "\n";
        str = str + "Atk Speed: " + attackSpeed.ToString() + "\n";
        return str;
    }
}
