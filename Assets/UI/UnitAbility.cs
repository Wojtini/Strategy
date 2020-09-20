using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAbility : MonoBehaviour
{
    public Ability ability = null;
    public Button button;

    virtual public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPress);
    }

    public void SetSlot(Ability ab)
    {
        ability = ab;
        button.image.sprite = ab.icon;
    }

    public void ClearSlot()
    {
        ability = null;
        button.image.sprite = null;
    }

    public void ButtonPress()
    {
        PlayerControl.instance.ClearCurrentAbility();
        if (ability == null)
            return;
        if (ability.requireConfirmation)
        {
            PlayerControl.instance.setAbility(ability);
        }
        else
        {
            Ability newAbility = ability.CreateNewTask();
            foreach (Object obj in Selection.instance.selectedObjects)
            {
                newAbility.setTarget(new Vector3(), null);
                obj.addTask(Instantiate(newAbility));
            }
            Destroy(newAbility.gameObject);
        }
    }
}
