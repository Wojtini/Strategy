using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Static
{
    public int stock;

    virtual public void Gather(int amount)
    {
        stock -= amount;
    }
}
