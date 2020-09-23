using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConstructAbility : Ability
{
    public GameObject building;
    // Start is called before the first frame update
    override public void Start()
    {
        //Building cost
    }

    public bool Construct() //true if construct place is good, false if not
    {
        GameObject newBuilding = Instantiate(building);
        newBuilding.transform.position = target;
        NetworkServer.Spawn(newBuilding);
        Building newBuildingComp = newBuilding.GetComponent<Building>();
        newBuildingComp.SetOwner(playerControl, factionCasted);
        newBuildingComp.healthPoints = 1;
        return true;
    }
}
