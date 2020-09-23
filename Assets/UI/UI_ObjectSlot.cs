using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ObjectSlot : MonoBehaviour
{
    public Button button;
    public Image image;
    public Object obj;

    public Slider hpBar;

    public Selection selection;
    public UI_MainSelect mainSelect;
    public void Awake()
    {
        button = GetComponent<Button>();
        image = button.image;
        button.onClick.AddListener(ButtonPress);
        hpBar = GetComponentInChildren<Slider>();
        selection = GetComponentInParent<Selection>();
        mainSelect = GetComponentInParent<UI_MainSelect>();
    }

    public void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(obj != null)
        {
            float hp = (float) obj.healthPoints / (float) obj.maxhealthPoints;
            //Debug.Log(hp);
            hpBar.value = hp;
            hpBar.gameObject.SetActive(true);
        }
        else
        {
            hpBar.gameObject.SetActive(false);
        }
    }

    public void ClearSlot()
    {
        image.sprite = null;
        obj = null;
    }

    public void SetSlot(Object obj)
    {
        this.obj = obj;
        image.sprite = obj.icon;
    }

    public void ButtonPress()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            selection.RemoveObject(obj);
        }
        else
        {
            if (obj != null)
            {
                mainSelect.SetUnit(obj);
            }
        }
    }
}
