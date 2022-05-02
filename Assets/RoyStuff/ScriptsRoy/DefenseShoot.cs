using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseShoot : MonoBehaviour
{
    Transform enemyTrn;
    GameObject enemy;

    public GameObject canon;
    float fireCount;

    public GameObject shootPoint;
    public GameObject shootPoint2;

    public AudioSource soundManager;
    public AudioClip shootingSound;

    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        fireCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fireCount);
        enemy = GameObject.FindWithTag("Player2");

        if (enemy != null)
        {
            enemyTrn = GameObject.FindWithTag("Player2").transform;

            if (Vector3.Distance(transform.position, enemyTrn.position) <= 15)  //if the distance is smaller or equals to 8 than the canon are firing a bomb
            {
                //Debug.Log(fireCount);
                fireCount++;
                
                if (fireCount == 10)
                {
                    Instantiate(explosion, shootPoint.transform.position, shootPoint.transform.rotation);
                    Instantiate(canon, shootPoint.transform.position, shootPoint.transform.rotation);
                    soundManager.PlayOneShot(shootingSound);
                }

                if (fireCount == 20)
                {
                    Instantiate(explosion, shootPoint2.transform.position, shootPoint2.transform.rotation);
                    Instantiate(canon, shootPoint2.transform.position, shootPoint2.transform.rotation);
                    soundManager.PlayOneShot(shootingSound);
                }
            }
        }

        else
        {
            fireCount = 0;
        }

        if (fireCount > 20)
        {
            fireCount = 0;
        }

        else
        {

        }
    }
}
