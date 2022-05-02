using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTurret : BuildingsManager
{
    public GameObject cannon;
    public GameObject firePoint01;
    public GameObject firePoint02;
    float cannonTurnSpeed = 10f;

    public GameObject turretBullet;
    public GameObject fireSmoke;
    float fireCountDown = 2f;

    GameObject damageAmount;

    GameObject shotSoundEffct;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = 700;
        health = startHealth;

        damageAmount = GameObject.Find("DamageAmountManager");

        shotSoundEffct = GameObject.Find("SoundManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            shotSoundEffct.GetComponent<SoundManager>().LightTankExplosion();
            GameObject destroyExplostion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(destroyExplostion, 5f);
            Destroy(gameObject);
        }
    }

    public void TurretFollow()
    {
        // Turret cannon rotation - look at the nearest enemy.
        Vector3 dir = GetComponent<FindBlueTeamTurret>().enemyTarget.position - cannon.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(cannon.transform.rotation, lookRotation, cannonTurnSpeed * Time.deltaTime).eulerAngles;
        cannon.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void TurretShootPoint01()
    {
        shotSoundEffct.GetComponent<SoundManager>().TurretFire();
        GameObject bulletOut = Instantiate(turretBullet, firePoint01.transform.position, firePoint01.transform.rotation);
        Instantiate(fireSmoke, firePoint01.transform.position, firePoint01.transform.rotation);
        TurretBullet bullet = bulletOut.GetComponent<TurretBullet>();

        if (bullet != null)
        {
            bullet.Seek(GetComponent<FindBlueTeamTurret>().enemyTarget);
        }
    }

    public void TurretShootPoint02()
    {
        shotSoundEffct.GetComponent<SoundManager>().TurretFire();
        GameObject bulletOut = Instantiate(turretBullet, firePoint02.transform.position, firePoint02.transform.rotation);
        Instantiate(fireSmoke, new Vector3(firePoint02.transform.position.x, firePoint02.transform.position.y, firePoint02.transform.position.z +0.5f), firePoint02.transform.rotation);
        TurretBullet bullet = bulletOut.GetComponent<TurretBullet>();

        if (bullet != null)
        {
            bullet.Seek(GetComponent<FindBlueTeamTurret>().enemyTarget);
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
