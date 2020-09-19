using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static LayerMask groundLayer;
    public static Camera cam;
    public static List<Unit> allUnits = new List<Unit>();

    public Object GetObjectUnderCursor()
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

    public Vector3 GetPointUnderCursor()
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
