using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBuillding : BuildingsManager
{
    public LayerMask powerBuilding;
    bool setActive;

    GameObject damageAmount;

    public GameObject explosion;
    public GameObject[] firePos;
    public GameObject[] fire;
    GameObject destroyFire01;
    GameObject destroyFire02;
    GameObject destroyFire03;
    GameObject destroyBigFire;
    bool above800;
    bool above600;
    bool above400;
    bool above200;

    GameObject startOverAttackRange;

    GameObject explosionSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 1200;
        health = startHealth;

        damageAmount = GameObject.Find("DamageAmountManager");
        above800 = true;

        startOverAttackRange = GameObject.FindWithTag("BlueTeam");

        explosionSoundEffect = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) && setActive)
        {
            healthBarCanvas.SetActive(false);
            setActive = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, powerBuilding))
            {
                healthBarCanvas.SetActive(true);
                setActive = true;
            }
            
        }

        if (health <= 800 && above800)
        {
            destroyFire01 = Instantiate(fire[0], firePos[0].transform.position, firePos[0].transform.rotation);
            above800 = false;
            above600 = true;
        }
        else if (health <= 600 && above600)
        {
            destroyFire02 = Instantiate(fire[1], firePos[1].transform.position, firePos[1].transform.rotation);
            above600 = false;
            above400 = true;
        }
        else if (health <= 400 && above400)
        {
            destroyFire03 = Instantiate(fire[2], firePos[2].transform.position, firePos[2].transform.rotation);
            above400 = false;
            above200 = true;
        }
        else if (health <= 200 && above200)
        {
            destroyBigFire = Instantiate(fire[3], firePos[3].transform.position, firePos[3].transform.rotation);
            above200 = false;
        }
        else if (health <= 0)
        {
            explosionSoundEffect.GetComponent<SoundManager>().PowerBuildingExplosion();
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(destroyFire01);
            Destroy(destroyFire02);
            Destroy(destroyFire03);
            Destroy(destroyBigFire);
            startOverAttackRange.GetComponent<LightTankEngineBlueTeam>().freeShot = true;

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
