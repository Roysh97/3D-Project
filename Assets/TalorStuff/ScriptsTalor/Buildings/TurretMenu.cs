using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMenu : BuildingsManager
{
    public LayerMask controlTower;
    public GameObject turrentCanvas;
    bool setActive;

    public GameObject turret;
    bool building;

    GameObject moneyToSpend;

    GameObject damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 1400;
        health = startHealth;

        damageAmount = GameObject.Find("DamageAmountManager");
        moneyToSpend = GameObject.Find("MoneyManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && setActive && building)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                turrentCanvas.SetActive(false);
                healthBarCanvas.SetActive(false);
                setActive = false;
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, controlTower))
            {
                turrentCanvas.SetActive(true);
                healthBarCanvas.SetActive(true);
                setActive = true;
                building = false;
            }

        }

    }

    public void TurretButton()
    {
        building = true;

        if (moneyToSpend.GetComponent<MoneyManager>().numberOfCoins >= 700)
        {
            Instantiate(turret, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= damageAmount.GetComponent<DamageAmountManager>().damageAmount;
            healthBar.fillAmount = health / startHealth;
        }
    }
}

