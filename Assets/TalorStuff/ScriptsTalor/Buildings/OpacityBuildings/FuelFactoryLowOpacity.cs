using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelFactoryLowOpacity : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask groundArea;
    RaycastHit hit;

    public GameObject fuelFactory;

    GameObject moneyToSpend;

    public static bool haveFuelFactory;

    private void Start()
    {
        moneyToSpend = GameObject.Find("MoneyManager");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            transform.position = hit.point;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
        {
            gameObject.SetActive(true);
            transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundArea))
            {
                return;
            }
            else
            {
                Instantiate(fuelFactory, new Vector3(transform.position.x, transform.position.y + 4.22f, transform.position.z), transform.rotation);
                moneyToSpend.GetComponent<MoneyManager>().numberOfCoins -= 2000;
                haveFuelFactory = true;
                Destroy(gameObject);
            }


        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
