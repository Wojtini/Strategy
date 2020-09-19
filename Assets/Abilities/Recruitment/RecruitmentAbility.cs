using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitmentAbility : Ability
{
    public GameObject unitToRecruit;

    override public bool Perform(Object obj)
    {
        Building building = (Building)obj;
        GameObject newUnit = Instantiate(unitToRecruit);
        newUnit.transform.position = building.spawnPoint.transform.position;
        newUnit.transform.localScale = new Vector3(1f, 1f, 1f);
        return true;
    }
}
