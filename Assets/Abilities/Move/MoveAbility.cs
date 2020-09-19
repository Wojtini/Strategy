using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbility : Ability
{
    public float stopDistance = 2f;

    public bool isAttackMove = false;
    public AttackAbility attackAbility;

    public void setTarget(Vector3 pos, Object obj, bool isAttackMove = false)
    {
        base.setTarget(pos, obj);
        this.isAttackMove = isAttackMove;
    }

    override public bool Perform(Object obj)
    {
        Unit mobObj = (Unit)obj;
        mobObj.agent.SetDestination(target);

        if (isAttackMove)
        {
            Object possibleEnemy = mobObj.CheckRange(mobObj.spotRange);
            if (possibleEnemy != null)
            {
                AttackAbility attackcast = Instantiate(attackAbility);
                attackcast.setTarget(target, possibleEnemy);
                obj.addTask(attackcast, 0);
                return false;
            }
        }

        if (Vector3.Distance(obj.transform.position, target) < stopDistance)
        {
            mobObj.agent.SetDestination(mobObj.transform.position);
            return true;
        }
        return false;
    }
}
