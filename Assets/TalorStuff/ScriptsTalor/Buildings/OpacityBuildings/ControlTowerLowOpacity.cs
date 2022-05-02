using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTowerLowOpacity : MonoBehaviour
{
    public LayerMask ground;
    public LayerMask groundArea;
    RaycastHit hit;

    public GameObject controlTower;

    GameObject moneyToSpend;

    public static bool haveControlTower;

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
                Instantiate(controlTower, new Vector3(transform.position.x, transform.position.y - 0.14f, transform.position.z), transform.rotation);
                moneyToSpend.GetComponent<MoneyManager>().numberOfCoins -= 1000;
                haveControlTower = true;
                Destroy(gameObject);
            }


        }
        else if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }
}
