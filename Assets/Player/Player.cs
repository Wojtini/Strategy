using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public string playerName = "Change Me";
    public string faction = "BLUE";

    public GameObject PlayerControl;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Instantiate(PlayerControl,this.transform);
    }
}
