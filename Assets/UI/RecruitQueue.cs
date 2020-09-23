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

    public UI_MainSelect mainSelect;
    // Start is called before the first frame update
    void Start()
    {
        imageButton = this.GetComponent<Image>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(ButtonPress);
        mainSelect = GetComponentInParent<UI_MainSelect>();
    }

    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if(ability == null)
        {
            imageButton.sprite = null;
        }
        else
        {
            imageButton.sprite = this.ability.icon;

        }
    }

    public void SetSlot(Ability ability,int index) //sets icon
    {
        this.ability = ability;
        this.queueNumber = index;
    }

    public void ClearSlot()
    {
        this.queueNumber = -1;
        this.ability = null;
    }

    private void ButtonPress()
    {
        if (this.queueNumber != -1)
        {
            mainSelect.currobj.CancelTask(queueNumber);
        }
    }
}
