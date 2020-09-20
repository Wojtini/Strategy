using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : Resource
{
    public override void Gather(int amount)
    {
        base.Gather(amount);
        PlayerResources.instance.addWood(amount);
    }
}
