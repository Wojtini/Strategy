using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : Ability
{
    public MoveAbility moveability;

    // Start is called before the first frame update
    override public bool Perform(Object obj)
    {
        UnitAttack((Unit)obj);
        if (targetObject == null)
        {
            return true;
        }

        return false;
    }

    private void UnitAttack(Unit unit)
    {
        if (targetObject != null)
        {
            Debug.Log(Vector3.Distance(unit.transform.position, targetObject.transform.position) + " i " + unit.attackRange + targetObject.size);
            if (Vector3.Distance(unit.transform.position, targetObject.transform.position) <= unit.attackRange + targetObject.size)
            {
                unit.AttackTarget(targetObject);
            }
            else
            {
                unit.agent.SetDestination(targetObject.transform.position);
            }
        }
        else
        {
            AttackMove(unit);
        }

    }

    private void AttackMove(Unit obj) //each frame ale na start dodaje wiec git
    {
        MoveAbility move = Instantiate(moveability);
        move.setTarget(target, obj, true);
        obj.addTask(move, 1);
    }
}
