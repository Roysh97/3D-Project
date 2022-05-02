using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLowOpacity : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask groundArea;
    RaycastHit hit;

    public GameObject turret;

    GameObject moneyToSpend;

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
                Instantiate(turret, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation);
                moneyToSpend.GetComponent<MoneyManager>().numberOfCoins -= 700;
                Destroy(gameObject);
            }


        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
