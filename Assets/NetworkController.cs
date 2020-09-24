using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : NetworkBehaviour
{
    public static NetworkController instance;

    public NetworkManager manager;
    public GameObject obj;

    public void Start()
    {
        instance = this;
        manager = GetComponent<NetworkManager>();
    }
}
