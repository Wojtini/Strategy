using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitmentAbility : Ability
{
    public GameObject unitToRecruit;
    public float currentTime = 0f;
    public float timeToRecruit = 5f;

    override public bool Perform(Object obj)
    {
        if(currentTime >= timeToRecruit)
        {
            Building building = (Building)obj;
            GameObject newUnit = Instantiate(unitToRecruit);
            Unit newUnitComp = newUnit.GetComponent<Unit>();
            newUnitComp.SetOwner(playerControl, factionCasted);
            newUnit.transform.position = building.spawnPoint.transform.position;
            newUnit.transform.localScale = new Vector3(1f, 1f, 1f);
            return true;
        }
        else
        {
            currentTime += Time.deltaTime;
            return false;
        }
    }
}
