using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestAbility : Ability
{
    public MoveAbility moveability;
    // Start is called before the first frame update
    override public bool Perform(Object obj)
    {
        if (targetObject == null || !(targetObject is Resource)) //if targeted object disappeared ie. Killed return completed task (true)
        {
            return true;
        }
        UnitHarvest((Unit)obj);

        return false;
    }

    private void UnitHarvest(Unit unit)
    {
        //Debug.Log(Vector3.Distance(unit.transform.position, targetObject.transform.position) + " i " + unit.attackRange + targetObject.size);
        if (Vector3.Distance(unit.transform.position, targetObject.transform.position) <= unit.attackRange + targetObject.size)
        {
            unit.HarvestObject((Resource) targetObject);
        }
        else
        {
            unit.agent.SetDestination(targetObject.transform.position);
        }
    }
}
