using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightTankEngineRedTeam : MonoBehaviour
{
    NavMeshAgent agent;

    // Attack value:
    public GameObject lightTankBullet;

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

        shotSoundEffct = GameObject.Find("SoundManager");
    }

    [System.Obsolete]
    // Update is called once per frame
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

        if (GetComponent<FindBlueTeam>().enemyTarget != null)
        {
            float range = Vector3.Distance(GetComponent<FindBlueTeam>().enemyTarget.position , transform.position);

            if (range >= 28)
            {
                agent.SetDestination(GetComponent<FindBlueTeam>().enemyTarget.position);
                agent.speed = 15f;
                wheelsSpeed = 300;
            }
            else
            {
                agent.speed = 0f;
                wheelsSpeed = 0;
            }

        }
        else if (GetComponent<FindBlueTeam>().enemyTarget == null)
        {
            agent.speed = 0f;
            wheelsSpeed = 0;
        }

        // Rotate all the 4 wheels together
        foreach (GameObject wheel in wheels)
        {
            wheel.transform.Rotate(wheelsSpeed * Time.deltaTime, 0, 0);
        }

    }

    public void CannonRotationToEnemy()
    {
        // Tank cannon rotation - look at the nearest enemy.
        Vector3 dir = GetComponent<FindBlueTeam>().enemyTarget.position - tankCannon.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tankCannon.transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        tankCannon.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Shoot()
    {
        shotSoundEffct.GetComponent<SoundManager>().LightTankShot();
        GameObject bulletOut = Instantiate(lightTankBullet, firePoint.transform.position, firePoint.transform.rotation);
        LightTankBullet bullet = bulletOut.GetComponent<LightTankBullet>();

        if (bullet != null)
        {
            bullet.Seek(GetComponent<FindBlueTeam>().enemyTarget);
        }
    }

}
