using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAbilities : MonoBehaviour
{
    public UnitAbility[] slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<UnitAbility>();
    }
    public void SetUnit(Object obj)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < obj.abilities.Count)
            {
                slots[i].SetSlot(obj.abilities[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public void clearUnit()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
    }
}
