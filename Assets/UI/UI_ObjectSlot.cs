using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ObjectSlot : MonoBehaviour
{
    public Button button;
    public Image image;
    public Object obj;

    public void Awake()
    {
        button = GetComponent<Button>();
        image = button.image;
        button.onClick.AddListener(ButtonPress);
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
            Selection.instance.RemoveObject(obj);
        }
        else
        {
            if (obj != null)
            {
                UI_MainSelect.instance.SetUnit(obj);
            }
        }
    }
}
