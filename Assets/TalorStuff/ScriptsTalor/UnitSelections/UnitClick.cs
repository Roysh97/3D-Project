using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    public LayerMask clickable;
    public LayerMask ground;

    void Start()
    {
        myCam = Camera.main;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                // If we hit a clickable object
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    // Shift clicked
                    UnitSelection.Instance.ShiftSelect(hit.collider.gameObject);
                }
                else
                {
                    // Normal clicked
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                }

            }
            else
            {
                // if we didn't && we're not shift clicking
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }
                
            }
        }

    }
}
