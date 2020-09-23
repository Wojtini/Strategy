using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability : MonoBehaviour
{
    public string abilityName;
    public Sprite icon;
    public bool requireConfirmation = true;

    public GameObject confirmationIndicator;
    public PlayerControl playerControl;
    public string factionCasted = "";

    public Vector3 target; //Zawsze jest
    public Object targetObject; //Nie zawsze jest
    public List<Ability> requiredAbilities = new List<Ability>();

    [Header("Type of unit requirement")]
    public bool requireMobile = false;
    public bool requireUnit = false;
    public bool requireBuilding = false;
    [Header("Ability Requirements")]
    public int goldRequirement = 0;
    public int woodRequirement = 0;
    [Header("Unit Requirements")]
    public int healthRequirement = 0;
    public int manaRequirement = 0;

    virtual public void Start()
    {
        //requiredAbilities.Add(this);
    }

    virtual public Ability CreateNewTask(PlayerControl playerControl)
    {
        setFaction(playerControl, playerControl.PlayerFaction);
        return Instantiate(this);
    }

    virtual public void setTarget(Vector3 pos, Object obj)
    {
        target = pos;
        targetObject = obj;
    }

    virtual public bool Perform(Object obj) //return true upon task completion/cancelation.  After true is returned object moves to another task.
    {
        return true;
    }

    virtual public void setFaction(PlayerControl playerControl,string faction)
    {
        this.playerControl = playerControl;
        factionCasted = faction;
    }
}
