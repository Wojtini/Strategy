using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionUI : MonoBehaviour
{
    public UI_ObjectSlot[] slots;
    public GameObject slotsParent;
    public Selection selection;

    public UI_MainSelect mainSelect;
    // Start is called before the first frame update
    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<UI_ObjectSlot>();
        //mainSelect.GetComponentInChildren<UI_MainSelect>();
        selection = GetComponent<Selection>();
        selection.onItemSelectionChange += UpdateUI;
    }
    
    void UpdateUI()
    {
        if(selection.selectedObjects.Count != 0)
        {
            mainSelect.SetUnit(selection.selectedObjects[0]);
        }
        else
        {
            mainSelect.ClearUnit();
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
