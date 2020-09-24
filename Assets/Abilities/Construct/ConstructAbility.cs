using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ConstructAbility : Ability
{
    public GameObject building;

    public bool Construct() //true if construct place is good, false if not
    {
        //GameObject newBuilding = Instantiate(building);
        //newBuilding.transform.position = target;
        
        //Building newBuildingComp = newBuilding.GetComponent<Building>();
        //newBuildingComp.SetOwner(playerControl, factionCasted);
        //newBuildingComp.healthPoints = 1;

        //NetworkController.instance.CmdSpawnObject(building);


        Debug.Log("This prints");
        Player.localPlayer.SpawnObject(building,target,factionCasted);
        return true;
    }
}
