using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Resource
{
    public override void Gather(PlayerResources playerresources,int amount)
    {
        base.Gather(playerresources,amount);
        playerresources.addGold(amount);
    }
}
