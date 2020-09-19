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

    public Vector3 target; //Zawsze jest
    public Object targetObject; //Nie zawsze jest
    public Ability[] requiredAbilities;

    [Header("Type of unit requirement")]
    public bool requireMobile = false;
    public bool requireUnit = false;
    public bool requireBuilding = false;
    public bool requireWorker = false;
    [Header("Ability Requirements")]
    public int goldRequirement = 0;
    public int woodRequirement = 0;
    [Header("Unit Requirements")]
    public int healthRequirement = 0;
    public int manaRequirement = 0;

    virtual public Ability CreateNewTask()
    {
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
}
