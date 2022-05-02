using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private Transform enemyTarget;

    float speedBollet = 60f;
    GameObject turretDamage;

    public GameObject explosionBullet;
    GameObject stopFire;

    GameObject bulletSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        turretDamage = GameObject.Find("DamageAmountManager");
        bulletSoundEffect = GameObject.Find("SoundManager");
    }

    public void Seek(Transform _enemyTarget)
    {
        enemyTarget = _enemyTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(0, 0, speedBollet * Time.deltaTime);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RedTeam" || other.gameObject.tag == "BlueTeam" || other.gameObject.tag == "Ground")
        {
            bulletSoundEffect.GetComponent<SoundManager>().LightTankBullet();
            turretDamage.GetComponent<DamageAmountManager>().DamageAmount(70);
            GameObject explosionDestroy = Instantiate(explosionBullet, transform.position, transform.rotation);
            Destroy(explosionDestroy, 5f);
            Destroy(gameObject);
        }
    }
}
