using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class Map : MonoBehaviour
{
    public static Map instance;
    public List<Object> allObjects = new List<Object>();

    public void Awake()
    {
        instance = this;
        //allObjects = new List<Object>(FindObjectsOfType<Object>());
    }


    //Still not handling
    public void onObjectSpawn(Object obj)
    {
        allObjects.Add(obj);
    }
    public void onObjectDestroy(Object obj)
    {
        allObjects.Remove(obj);
    }
}
