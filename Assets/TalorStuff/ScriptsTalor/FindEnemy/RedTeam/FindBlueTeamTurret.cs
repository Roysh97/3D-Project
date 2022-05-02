using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindBlueTeamTurret : MonoBehaviour
{
    public Transform enemyTarget;
    public string enemyTag = "BlueTeam";

    float range = 55f;

    //Time between shoot
    float fireCountdown = 3f;


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

        GetComponent<RedTurret>().TurretFollow();

        fireCountdown -= Time.deltaTime;

        if (fireCountdown <= 1.5f && fireCountdown > 1.48f)
        {
            GetComponent<RedTurret>().TurretShootPoint01();
        }
        else if (fireCountdown <= 0)
        {
            GetComponent<RedTurret>().TurretShootPoint02();
            fireCountdown = 3f;
        }
    }
}
