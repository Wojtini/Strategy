using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    public UI_ObjectSlot[] slots;
    public GameObject slotsParent;
    public Selection selection;
    // Start is called before the first frame update
    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<UI_ObjectSlot>();
        selection = Selection.instance;
        selection.onItemSelectionChange += UpdateUI;
    }
    
    void UpdateUI()
    {
        if(selection.selectedObjects.Count != 0)
        {
            UI_MainSelect.instance.SetUnit(selection.selectedObjects[0]);
        }
        else
        {
            UI_MainSelect.instance.ClearUnit();
        }
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < selection.selectedObjects.Count)
            {
                slots[i].SetSlot(selection.selectedObjects[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
