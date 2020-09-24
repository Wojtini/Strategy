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
            //GameObject newUnit = Instantiate(unitToRecruit);
            //Unit newUnitComp = newUnit.GetComponent<Unit>();
            //newUnitComp.SetOwner(factionCasted);
            //newUnit.transform.position = building.spawnPoint.transform.position;

            Player.localPlayer.SpawnObject(unitToRecruit, building.spawnPoint.transform.position, factionCasted);
            return true;
        }
        else
        {
            currentTime += Time.deltaTime;
            return false;
        }
    }
}
