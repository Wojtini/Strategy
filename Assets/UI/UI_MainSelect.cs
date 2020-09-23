using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainSelect : MonoBehaviour
{
    public Object currobj;
    public Image Icon;
    public Text HpText;
    public Text NameText;
    public Text StatsText;
    [Header("Building Stuff")]
    public GameObject buildingProduction;
    public Slider progressBar;
    public RecruitQueue[] recruitmentQueue;

    public UnitAbilities mainSlotAbilities;

    public void Start()
    {
        buildingProduction.gameObject.SetActive(false);
    }

    public void Update()
    {
        UpdateMainUnitUI();
        UpdateUnit();
    }

    public void SetUnit(Object obj)
    {
        //things that dont change at runtime
        currobj = obj;
        NameText.text = currobj.displayName.ToString();
        mainSlotAbilities.SetUnit(currobj);
        StatsText.text = currobj.getStringStats();
    }

    private void UpdateMainUnitUI()
    {
        if (currobj == null)
        {
            buildingProduction.SetActive(false);
            return;
        }
        HpText.text = currobj.healthPoints.ToString() + "/" + currobj.maxhealthPoints.ToString();
        Icon.sprite = currobj.icon;
        if (currobj is Building)
        {
            UpdateProgressBar((Building)currobj);
            buildingProduction.SetActive(true);
        }
        else
        {
            buildingProduction.SetActive(false);
        }
    }

    private void UpdateProgressBar(Building building)
    {
        if(building.taskList.Count == 0)
        {
            progressBar.value = 0f;
        }
        else if(building.taskList[0] is RecruitmentAbility)
        {
            RecruitmentAbility recruitment = (RecruitmentAbility)building.taskList[0];
            float progress = recruitment.currentTime / recruitment.timeToRecruit;
            progressBar.value = progress;
        }
        for (int i = 0; i < recruitmentQueue.Length; i++)
        {
            if (i < building.taskList.Count)
            {
                recruitmentQueue[i].SetSlot(building.taskList[i],i);
            }
            else
            {
                recruitmentQueue[i].ClearSlot();
            }
        }
    }

    public void UpdateUnit()
    {
        if(currobj != null)
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
