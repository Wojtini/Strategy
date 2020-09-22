using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitQueue : MonoBehaviour
{
    public Ability ability;
    public int queueNumber;
    public Image imageButton;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        imageButton = this.GetComponent<Image>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(ButtonPress);
    }

    public void SetSlot(Ability ability,int index) //sets icon
    {
        this.ability = ability;
        this.queueNumber = index;
        imageButton.sprite = this.ability.icon;
    }

    public void ClearSlot()
    {
        imageButton.sprite = null;
        this.queueNumber = -1;
        this.ability = null;
    }

    private void ButtonPress()
    {
        if (this.queueNumber != -1)
        {
            UI_MainSelect.instance.currobj.CancelTask(queueNumber);
        }
    }
}
