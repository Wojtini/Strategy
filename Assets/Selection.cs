using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour
{
    public delegate void OnSelectionChanged();
    public OnSelectionChanged onItemSelectionChange;

    public List<Object> firstGroup = new List<Object>();
    public List<Object> secondGroup = new List<Object>();
    public List<List<Object>> allGroups = new List<List<Object>>();

    public static Selection instance;

    public List<Object> selectedObjects = new List<Object>();
    //For unwanted selectionbox spawn
    private bool wasObjectSelectedLastMouseDownInput = false;
    //Selection Box stuff
    public Vector2 startPos;
    public RectTransform selectionRect;
    public float selectionBoxSpawnTime = 0.1f;
    public float timePressedButton = 0f;
    public bool isDrawingSelectBox = false;

    private void Awake()
    {
        instance = this;
        allGroups.Add(firstGroup);
        allGroups.Add(secondGroup);
    }

    public void Update()
    {
        if (PlayerControl.instance.currentAbility != null || PlayerControl.instance.usedAbilityPreviousFrame)
        {
            return;
        }
        HandleGroup();
        if (!wasObjectSelectedLastMouseDownInput)
        {
            SelectionBox();
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        TrySelect();
    }

    private void TrySelect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Object pom = GetObjectUnderCursor();
            if (pom != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                        AddObject(pom);
                        wasObjectSelectedLastMouseDownInput = true;
                }
                else
                {
                    ClearSelection();
                    AddObject(pom);
                    wasObjectSelectedLastMouseDownInput = true;
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    ClearSelection();
                    wasObjectSelectedLastMouseDownInput = false;
                    //Didnt select any object in this case but we 
                }
                else
                {
                    wasObjectSelectedLastMouseDownInput = false;
                }
            }
        }
    }

    private Object GetObjectUnderCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Object pom = hit.transform.GetComponent<Object>();
            if (pom != null)
            {
                return pom;
            }
        }
        return null;
    }

    private void SelectionBox()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                timePressedButton += Time.deltaTime;
            }
            if (timePressedButton > selectionBoxSpawnTime)
            {
                selectionRect.gameObject.SetActive(true);
                isDrawingSelectBox = true;
            }
            float width = Input.mousePosition.x - startPos.x;
            float height = Input.mousePosition.y - startPos.y;

            selectionRect.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            selectionRect.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
        }
        if (Input.GetMouseButtonUp(0) && isDrawingSelectBox)
        {
            selectionRect.gameObject.SetActive(false);

            Vector2 min = selectionRect.anchoredPosition - (selectionRect.sizeDelta / 2);
            Vector2 max = selectionRect.anchoredPosition + (selectionRect.sizeDelta / 2);

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                Selection.instance.ClearSelection();
            }
            foreach (Unit unit in PlayerControl.allUnits)
            {
                Vector3 screenpos = Camera.main.WorldToScreenPoint(unit.transform.position);
                if (screenpos.x > min.x && screenpos.x < max.x && screenpos.y > min.y && screenpos.y < max.y)
                {
                    Selection.instance.AddObject(unit);

                }
            }
            timePressedButton = 0f;
            isDrawingSelectBox = false;
        }
    }

    private void HandleGroup()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                allGroups[0] = selectedObjects;
            }
            else
            {
                SelectGroup(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                allGroups[1] = selectedObjects;
            }
            else
            {
                SelectGroup(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                allGroups[1] = selectedObjects;
            }
            else
            {
                SelectGroup(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                allGroups[1] = selectedObjects;
            }
            else
            {
                SelectGroup(1);
            }
        }
    }

    private void SelectGroup(int group)
    {
        ClearSelection();
        foreach (Object obj in allGroups[group])
        {
            AddObject(obj);
        }
    }

    public void RemoveObjectFromSelections(Object obj)
    {
        Selection.instance.RemoveObject(obj);
    }

    public void AddObject(Object obj)
    {
        if (selectedObjects.Contains(obj))
            return;
        selectedObjects.Add(obj);

        if (onItemSelectionChange != null)
            onItemSelectionChange.Invoke();
    }

    public void RemoveObject(Object obj)
    {
        selectedObjects.Remove(obj);

        if(onItemSelectionChange != null)
            onItemSelectionChange.Invoke();

        foreach(List<Object> list in allGroups)
        {
            list.Remove(obj);
        }
    }

    public void ClearSelection()
    {
        selectedObjects = new List<Object>();

        if (onItemSelectionChange != null)
            onItemSelectionChange.Invoke();
    }
}
