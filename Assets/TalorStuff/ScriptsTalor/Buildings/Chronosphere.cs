using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronosphere : BuildingsManager
{
    GameObject damageAmount;
    public GameObject explosion;
    GameObject explosionSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 5000;
        health = startHealth;
        damageAmount = GameObject.Find("DamageAmountManager");

    }

    // Update is called once per frame
    void Update()
    {

        if (health < 5000)
        {
            healthBarCanvas.SetActive(true);
        }

        if (health <= 0)
        {
/*            explosionSoundEffect.GetComponent<SoundManager>().LightTankExplosion();
            GameObject destroyExplostion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(destroyExplostion, 5f);*/
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
