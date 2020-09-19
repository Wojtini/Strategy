using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    public static List<Unit> allUnits = new List<Unit>();

    public int maxSelectedObjects;
    public LayerMask groundLayer;
    public Camera cam;
    public bool usedAbilityPreviousFrame = false;
    public string PlayerFaction = "";

    private GameObject abilityIndicator;
    public Ability currentAbility;
    [Header("Basic Tasks")]
    public Ability BasicMove;
    public Ability BasicAttack;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        usedAbilityPreviousFrame = false;

        if (abilityIndicator != null)
        {
            abilityIndicator.transform.position = GetPointUnderCursor();
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        //Handle Ability
        if (currentAbility != null) //Left Mouse Button
        {
            if (Input.GetMouseButtonUp(0))
            {
                ActivateAbility(currentAbility);
            }else if (Input.GetMouseButtonUp(1))
            {
                ClearCurrentAbility();
                return;
            }
        }

        //Handel easy tasking RMB-tasking
        if (Input.GetMouseButtonUp(1))
        {
            Object obj = GetObjectUnderCursor();
            if (obj == null)
            {
                Ability abilitycast = BasicMove.CreateNewTask();
                ActivateAbility(abilitycast);
            }
            else
            {
                if(obj.Faction == PlayerFaction)
                {
                    //Repair
                }
                else
                {
                    Ability abilitycast = BasicAttack.CreateNewTask();
                    ActivateAbility(abilitycast);
                }
            }
        }
    }

    private void ClearCurrentAbility(Ability ability = null)
    {
        if (currentAbility != null)
        {
            Destroy(currentAbility.gameObject);
            currentAbility = null;
        }
        else
        {
            Destroy(ability.gameObject);
            ability = null;
        }
        if (abilityIndicator != null)
        {
            Destroy(abilityIndicator);
            abilityIndicator = null;
        }
    }

    public void setAbilityCast(Ability ability)
    {
        currentAbility = ability.CreateNewTask();
        if (ability.confirmationIndicator != null)
        {
            abilityIndicator = Instantiate(ability.confirmationIndicator);
        }
    }

    private void ActivateAbility(Ability ability)
    {
        usedAbilityPreviousFrame = true;

        Object clickObj = GetObjectUnderCursor();
        Vector3 clickPos = GetPointUnderCursor();

        foreach (Object obj in Selection.instance.selectedObjects)
        {
            ability.setTarget(clickPos, clickObj);
            obj.addTask(Instantiate(ability));
        }
        ClearCurrentAbility(ability);
    }
    //
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

    private Vector3 GetPointUnderCursor()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundLayer))
        {
            return hit.point;
        }
        return new Vector3();
    }
}
