using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindRedTeam : MonoBehaviour
{
    public Transform enemyTarget;
    public string enemyTag = "RedTeam";

    float range = 50f;

    //Time between shoot
    private float fireCountdown = 1f;

    public GameObject lightTankCannon;

    
    void Start()
    {
        InvokeRepeating("UpdateEnemy", 0f, 0.5f);
    }

    void UpdateEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDist = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distToEnemy < shortestDist)
            {
                shortestDist = distToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDist <= range)
        {
            enemyTarget = nearestEnemy.transform;
        }
        else
        {
            enemyTarget = null;
        }

        Debug.Log(enemyTarget);
    }

    
    void Update()
    {
        if (enemyTarget == null)
        {
            return;
        }


        if (GetComponent<LightTankEngineBlueTeam>().freeShot == false)
        {
            GetComponent<LightTankEngineBlueTeam>().CannonRotation();
        }
        else if (GetComponent<LightTankEngineBlueTeam>().freeShot == true)
        {
            GetComponent<LightTankEngineBlueTeam>().fireAttack = false;
            GetComponent<LightTankEngineBlueTeam>().CannonRotationToEnemy();

            fireCountdown -= Time.deltaTime;

            if (fireCountdown <= 0)
            {
                GetComponent<LightTankEngineBlueTeam>().Shoot();
                fireCountdown = 1f;
            }
        }
    }
}
