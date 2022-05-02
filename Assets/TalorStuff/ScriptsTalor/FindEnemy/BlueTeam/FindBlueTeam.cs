using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindBlueTeam : MonoBehaviour
{
    public Transform enemyTarget;
    public string enemyTag = "BlueTeam";

    float range = 65f;

    //Time between shoot
    private float fireCountdown = 1f;

    public LayerMask building;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget == null)
        {
            return;
        }

        GetComponent<LightTankEngineRedTeam>().CannonRotationToEnemy();
        

        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 0)
        {
            GetComponent<LightTankEngineRedTeam>().Shoot();
            fireCountdown = 1f;
        }


    }
}
