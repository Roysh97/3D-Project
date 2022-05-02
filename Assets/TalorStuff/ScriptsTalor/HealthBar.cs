using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Army
{
    public GameObject healthBarCanvas;
    public Image healthBar;
    Quaternion healthBarRotaion;

    public GameObject explosion;
    GameObject explosionSoundEffect;

    GameObject damageAmount;

    // Start is called before the first frame update
    void Start()
    {
        explosionSoundEffect = GameObject.Find("SoundManager");

        damageAmount = GameObject.Find("DamageAmountManager");
        healthBarRotaion = Camera.main.transform.rotation;

        startHealth = 370;
        health = startHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBarCanvas.transform.rotation = healthBarRotaion;

        if (health <= 0)
        {
            explosionSoundEffect.GetComponent<SoundManager>().LightTankExplosion();
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
