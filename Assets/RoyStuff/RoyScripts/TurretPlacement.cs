using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPlacement : MonoBehaviour
{
    RaycastHit hit;

    Vector3 place;

    bool isplacing;

    public GameObject turret;

    bool isbought;

    // Update is called once per frame
    void Update()
    {
         if (Input.GetMouseButtonDown(0) && isplacing == true)
         {
              if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
              {
                  if(hit.transform.tag == "terrain")
                  {
                      place = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                      isplacing = false;

                      if (isbought == true)
                      {
                         Instantiate(turret, place, transform.rotation);
                      }
                  }
              }
         }
    }

    public void BuyTurret()
    {
        isplacing = true;
    }
}
