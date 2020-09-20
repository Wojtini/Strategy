using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildingSlot : UnitAbility
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        if (this.ability != null)
        {
            this.SetSlot(this.ability);
        }
    }
    
}
