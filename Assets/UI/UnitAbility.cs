using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAbility : MonoBehaviour
{
    public Ability ability = null;
    public Button button;
    public PlayerControl playerControl;
    public Selection selection;

    virtual public void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPress);
        playerControl = GetComponentInParent<PlayerControl>();
        selection = GetComponentInParent<Selection>();
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
        playerControl.ClearCurrentAbility();
        if (ability == null)
            return;
        if (ability.requireConfirmation)
        {
            playerControl.setAbility(ability);
        }
        else
        {
            Ability newAbility = ability.CreateNewTask(playerControl);
            foreach (Object obj in selection.selectedObjects)
            {
                newAbility.setTarget(new Vector3(), null);
                obj.addTask(Instantiate(newAbility));
            }
            Destroy(newAbility.gameObject);
        }
    }
}
