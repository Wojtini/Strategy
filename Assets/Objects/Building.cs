using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Static
{
    public RecruitmentAbility recruitAbility;
    public GameObject[] recruitableUnits;

    public GameObject voidBuilding;

    public GameObject spawnPoint;

    override public void Start()
    {
        foreach(GameObject gameobject in recruitableUnits)
        {
            Ability newAbility = Instantiate(recruitAbility);
            recruitAbility.unitToRecruit = gameobject;
            newAbility.transform.parent = this.transform;
            this.abilities.Add(newAbility);
        }
        base.Start();
    }
}
