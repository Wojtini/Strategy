using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public string playerName = "Change Me";
    public string faction = "BLUE";

    public static Player localPlayer;

    public NetworkController control;
    public NetworkManager manager;

    public GameObject PlayerControl;

    public PlayerResources playerResources;

    public void Start()
    {
        control = NetworkController.instance;
        manager = control.GetComponent<NetworkManager>();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Instantiate(PlayerControl,this.transform);
        playerResources = GetComponentInChildren<PlayerResources>();

        localPlayer = this;
    }

    private int GetSpawnIndex(GameObject obj)
    {
        int index = manager.spawnPrefabs.IndexOf(obj);
        if (index == -1)
            Debug.Log(obj + "Obiekt nie jest zarejestrowany w spawnable prefabs");
        return index;
    }

    public void SpawnObject(GameObject obj,Vector3 pos,string factionCasted)
    {
        Debug.Log("Trying to spawn " + obj);
        int index = GetSpawnIndex(obj);
        Debug.Log(index);
        if (index == -1)
            return;
        CmdSpawnObject(index, pos, factionCasted);
    }

    [Command]
    public void CmdSpawnObject(int objIndex,Vector3 pos, string factionCasted)
    {
        GameObject newObj = Instantiate(manager.spawnPrefabs[objIndex]);
        newObj.transform.position = pos;
        newObj.GetComponent<Object>().SetOwner(factionCasted);
        Debug.Log("Command work");
        NetworkServer.Spawn(newObj);
        //RpcSpawnObject(objIndex);
    }

    public void AddTaskToObject(GameObject ObjectGameObject, GameObject AbilityGameObject, Vector3 clickPos, GameObject targetPos)
    {
        int index = GetSpawnIndex(AbilityGameObject);
        index = 2;
        if (index == -1)
            return;
        CmdAddTaskToObject(ObjectGameObject, index, clickPos, targetPos);
    }

    [Command]
    public void CmdAddTaskToObject(GameObject ObjectGameObject, int AbilityGameObjectIndex,Vector3 clickPos,GameObject targetPos)
    {
        Object obj = ObjectGameObject.GetComponent<Object>();
        //first spawn ability object
        GameObject newObj = Instantiate(manager.spawnPrefabs[AbilityGameObjectIndex]);
        //Setup ability
        Ability ability = newObj.GetComponent<Ability>();
        if (targetPos == null)
        {
            ability.setTarget(clickPos, null);
        }
        else
        {
            ability.setTarget(clickPos, targetPos.GetComponent<Object>());
        }
        obj.addTask(ability);
        NetworkServer.Spawn(newObj);
        Debug.Log("Dodano taska " + ability + " ze wspolrzednymi " + clickPos + targetPos);
    }


    public void ClearObjectsTask(GameObject ObjectGameObject)
    {
        CmdClearObjectsTask(ObjectGameObject);
    }

    [Command]
    public void CmdClearObjectsTask(GameObject ObjectGameObject)
    {
        Object obj = ObjectGameObject.GetComponent<Object>();
        obj.clearTaskList();
    }
}
