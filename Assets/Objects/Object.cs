using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Object : MonoBehaviour
{
    [Header("Task List")]
    public List<Ability> taskList = new List<Ability>();

    [Header("Object Info")]
    public string displayName = "NAME PLACEHOLDER";
    public string description = "DESC PLACEHOLDER";
    public Sprite icon;
    public string Faction = "NONE";
    public float size = 0f;

    public Image selectioncircle;
    //public bool isSelected = false;

    [Header("Object Stats")]
    public int maxhealthPoints = 1;
    public int healthPoints = 1;
    public int maxmanaPoints = 1;
    public int manaPoints = 1;

    public List<Ability> abilities = new List<Ability>();

    virtual public void Start()
    {
        Map.instance.onObjectSpawn(this);
        selectioncircle = GetComponentInChildren<Image>();
        toggleSelection(false);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, size);
    }

    virtual public void Update()
    {
        if (taskList.Count >= 1)
        {
            if (taskList[0].Perform(this))
            {
                Destroy(taskList[0].gameObject);
                taskList.Remove(taskList[0]);
            }
        }
    }

    virtual public void Destroy()
    {
        Map.instance.onObjectDestroy(this);
        clearTaskList();
        Selection.instance.RemoveObjectFromSelections(this);
        Destroy(gameObject);
    }

    virtual public void dealDamage(int amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0)
        {
            Destroy();
        }
        UI_MainSelect.instance.UpdateUnit();
    }

    virtual public void healDamage(int amount)
    {
        healthPoints += amount;
        if (healthPoints > maxhealthPoints)
        {
            healthPoints = maxhealthPoints;
        }
        UI_MainSelect.instance.UpdateUnit();
    }

    virtual public void addTask(Ability ability,int priority = -1)
    {
        if (CheckRequirements(ability))
        {
            if (priority != -1)
            {
                taskList.Insert(priority, ability);
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    this.clearTaskList();
                }
                taskList.Add(ability);
            }
        }
        else
        {
            Destroy(ability.gameObject);
        }
    }

    virtual public bool CheckRequirements(Ability ability)
    {
        //Start checking requirements
        //Check if unit has ability (problem with all selected units)
        foreach(Ability reqAbility in ability.requiredAbilities)
        {
            bool foundAbility = false;
            //Debug.Log("Sprawdzam czy unit zawiera: " + reqAbility.abilityName);
            foreach(Ability availableAbility in this.abilities)
            {
                //Debug.Log("Aktualne ability: " + availableAbility.abilityName);
                if (availableAbility.abilityName == reqAbility.abilityName)
                {
                    //Debug.Log("Znalazlem: " + availableAbility.abilityName);
                    foundAbility = true;
                }
            }
            if (!foundAbility)
            {
                //Debug.Log("Nie znalazlem: " + reqAbility.abilityName);
                return false;
            }
        }
        //Check is object type is good
        if (ability.requireBuilding)
        {
            if(!(this is Building))
                return false;
        }
        if (ability.requireUnit)
        {
            if (!(this is Unit))
                return false;
        }
        if (ability.requireMobile)
        {
            if (!(this is Mobile))
                return false;
        }
        //Check Requirements Resources
        PlayerResources PR = PlayerResources.instance;
        if (ability.woodRequirement > PR.WoodAmount || ability.goldRequirement > PR.GoldAmount)
        {
            return false;
            //Not enough resources
        }
        //Costing
        PR.WoodAmount -= ability.woodRequirement;
        PR.GoldAmount -= ability.goldRequirement;

        return true;
    }

    virtual public void FailCast()
    {

    }

    virtual public void clearTaskList()
    {
        foreach(Ability ability in taskList)
        {
            Destroy(ability.gameObject);
        }
        taskList = new List<Ability>();
    }

    virtual public string getStringStats()
    {
        string str = "";

        return str;
    }

    virtual public void toggleSelection(bool toggle)
    {
        if (selectioncircle == null)
        {
            return;
        }
        selectioncircle.enabled = toggle;
    }
}
