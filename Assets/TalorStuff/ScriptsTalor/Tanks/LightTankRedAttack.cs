using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightTankRedAttack : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
 
    int wavePointIndex = 0;

    public Transform commandCenter;

    // Attack value:
    public GameObject lightTankBullet;
    float countDown = 1f;

    // Tank cannon value:
    public GameObject tankCannon;
    float turnSpeed = 10f;
    public GameObject firePoint;

    // Wheels value:
    public GameObject[] wheels;
    float wheelsSpeed;

    public GameObject healthBarCanvas;

    GameObject shotSoundEffct;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = EnemyManager.wayPoints[0];

        shotSoundEffct = GameObject.Find("SoundManager");
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "LightTankRed")
                {
                    healthBarCanvas.SetActive(true);
                }

            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            healthBarCanvas.SetActive(false);
        }

        agent.SetDestination(target.position);
        agent.speed = 15f;
        wheelsSpeed = 300;


        if (Vector3.Distance(transform.position, target.position) <= 10f)
        {
            GetNextWayPoint();
        }

        if (GetComponent<TankAttackFindBlueTeam>().enemyTarget != null)
        {
            float range = Vector3.Distance(GetComponent<TankAttackFindBlueTeam>().enemyTarget.position, transform.position);

            if (range >= 28)
            {
                CannonRotationToEnemy();
                countDown -= Time.deltaTime;
                if (countDown <=0)
                {
                    Shoot();
                    countDown = 1f;
                }
                
                agent.SetDestination(GetComponent<TankAttackFindBlueTeam>().enemyTarget.position);
                agent.speed = 15f;
                wheelsSpeed = 300;
            }
            else
            {
                agent.speed = 0f;
                wheelsSpeed = 0;
            }

        }

        // Rotate all the 4 wheels together
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(wheelsSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void GetNextWayPoint()
    {
        wavePointIndex++;
        target = EnemyManager.wayPoints[wavePointIndex];
    }

    public void CannonRotationToEnemy()
    {
        // Tank cannon rotation - look at the nearest enemy.
        Vector3 dir = GetComponent<TankAttackFindBlueTeam>().enemyTarget.position - tankCannon.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tankCannon.transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        tankCannon.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    // Shot function - search if there is an enemy nearby, if not destroy the bullet in FindBlueTeam script.
    public void Shoot()
    {
        shotSoundEffct.GetComponent<SoundManager>().LightTankShot();
        GameObject bulletOut = Instantiate(lightTankBullet, firePoint.transform.position, firePoint.transform.rotation);
        LightTankBullet bullet = bulletOut.GetComponent<LightTankBullet>();

        if (bullet != null)
        {
            bullet.Seek(GetComponent<TankAttackFindBlueTeam>().enemyTarget);
        }
    }
}
