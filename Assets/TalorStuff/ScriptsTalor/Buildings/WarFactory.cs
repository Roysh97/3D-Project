using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarFactory : BuildingsManager
{
    public LayerMask warFactory;
    public GameObject warFactoryShop;
    bool setActive;

    float countDown;
    float blackPanelCountDown;
    public GameObject blackPanel;
    public Image lightTankBlackPanel;

    public GameObject spawnPoint;
    public GameObject lightTank;

    bool buyTank;
    GameObject moneyToSpend;

    GameObject damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 1000;
        health = startHealth;

        moneyToSpend = GameObject.Find("MoneyManager");

        damageAmount = GameObject.Find("DamageAmountManager");

        countDown = 5f;
        blackPanelCountDown = countDown;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && setActive && buyTank == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                warFactoryShop.SetActive(false);
                healthBarCanvas.SetActive(false);
                setActive = false;
            }

        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, redTeam))
            {
                this.healthBarCanvas.SetActive(true);
                this.setActive = true;
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, warFactory))
            {
                this.warFactoryShop.SetActive(true);
                this.healthBarCanvas.SetActive(true);
                this.setActive = true;
            }

        }


        if (buyTank)
        {
            blackPanelCountDown -= Time.deltaTime;
            lightTankBlackPanel.fillAmount = blackPanelCountDown / countDown;
            if (blackPanelCountDown <= 0)
            {
                Instantiate(lightTank, spawnPoint.transform.position, spawnPoint.transform.rotation);
                countDown = 5f;
                blackPanelCountDown = countDown;
                buyTank = false;
                blackPanel.SetActive(false);
                lightTankBlackPanel.fillAmount = 1f;
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void LightTankButton()
    {
        if (moneyToSpend.GetComponent<MoneyManager>().numberOfCoins >= 500)
        {
            buyTank = true;
            blackPanel.SetActive(true);
            moneyToSpend.GetComponent<MoneyManager>().numberOfCoins -= 500;
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
