using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedFuelFactory : BuildingsManager
{
    public GameObject moneyToGet;
    public Text moneytextDisplay;

    float countDown = 10;

    GameObject damageAmount;
    public GameObject explosion;
    GameObject explosionSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 800;
        health = startHealth;
        damageAmount = GameObject.Find("DamageAmountManager");

        explosionSoundEffect = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            if (!moneyToGet.activeSelf)
            {
                moneyToGet.SetActive(true);
                countDown = 10;
            }
            else if (moneyToGet.activeSelf)
            {
                moneyToGet.SetActive(false);
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
