using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyBuilding : BuildingsManager
{
    public GameObject moneyToGet;
    public Text moneytextDisplay;

    GameObject addMoney;

    float countDown;

    bool ismoney;

    GameObject damageAmount;
    public GameObject explosion;
    GameObject explosionSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 800;
        health = startHealth;
        damageAmount = GameObject.Find("DamageAmountManager");

        countDown = 3;
        addMoney = GameObject.Find("MoneyManager");
        ismoney = true;

        explosionSoundEffect = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        addMoney.GetComponent<MoneyManager>().moneyValue.text = addMoney.GetComponent<MoneyManager>().numberOfCoins + "$";

        if (ismoney == true)
        {

            countDown -= Time.deltaTime;

            if( countDown <= 0)
            {
                if(!moneyToGet.activeSelf)
                {
                    moneyToGet.SetActive(true);
                    addMoney.GetComponent<MoneyManager>().numberOfCoins += 25;
                    countDown = 10;
                }

                else if(moneyToGet.activeSelf)
                {
                    moneyToGet.SetActive(false);
                }
            }
            
            if (addMoney.GetComponent<MoneyManager>().numberOfCoins >= 999999)      
            {
                ismoney = false;
            }
        }

        if (health <= 0)
        {
            explosionSoundEffect.GetComponent<SoundManager>().FuelFactoryExplosion();
            GameObject destroyExplostion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(destroyExplostion, 5f);
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
