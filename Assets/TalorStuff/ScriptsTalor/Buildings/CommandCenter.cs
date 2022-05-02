using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCenter : BuildingsManager
{
    public LayerMask commandCenter;
    public GameObject commandCenterCanvas;
    bool setActive;

    float countDown;
    float blackPanelCountDown;
    public GameObject blackPanel;

    bool building;

    GameObject damageAmount;

    void Start()
    {
        startHealth = 4000;
        health = startHealth;

        damageAmount = GameObject.Find("DamageAmountManager");

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && setActive && building)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                commandCenterCanvas.SetActive(false);
                healthBarCanvas.SetActive(false);
                setActive = false;
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, commandCenter))
            {
                commandCenterCanvas.SetActive(true);
                healthBarCanvas.SetActive(true);
                setActive = true;
            }

        }

        if (health < 4000)
        {
            healthBarCanvas.SetActive(true);
        }
        else if (health <= 0)
        {
            Destroy(gameObject);
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
