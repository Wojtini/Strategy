using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainSelect : MonoBehaviour
{
    public static UI_MainSelect instance;

    public Object currobj;
    public Image Icon;
    public Text HpText;
    public Text NameText;
    public Text StatsText;

    public UnitAbilities mainSlotAbilities;

    public void Start()
    {
        instance = this;
    }

    public void SetUnit(Object obj)
    {
        //Debug.Log(obj);
        if(obj == null)
        {
            return;
        }
        currobj = obj;
        Icon.sprite = obj.icon;
        HpText.text = obj.healthPoints.ToString() + "/" + obj.maxhealthPoints.ToString();
        NameText.text = obj.displayName.ToString();
        StatsText.text = obj.getStringStats();
        mainSlotAbilities.SetUnit(obj);
    }

    public void UpdateUnit()
    {
        SetUnit(currobj);
    }

    public void ClearUnit()
    {
        currobj = null;
        Icon.sprite = null;
        HpText.text = null;
        NameText.text = null;
        StatsText.text = null;
        mainSlotAbilities.clearUnit();
    }
}
