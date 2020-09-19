using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAbility : MonoBehaviour
{
    public Ability ability = null;
    public Button button;

    public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPress);
    }

    public void SetSlot(Ability ab)
    {
        Debug.Log(ab);
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
        if (ability == null)
            return;
        if (ability.requireConfirmation)
        {
            PlayerControl.instance.setAbilityCast(ability);
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
