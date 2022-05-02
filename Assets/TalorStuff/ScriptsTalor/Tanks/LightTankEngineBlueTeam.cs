using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTankEngineBlueTeam : UnitMovement
{
    float range;
    float dist = 0.03f;  // Distance from the agent distanion value.

    // Attack value:
    public bool fireAttack;
    public bool freeShot;
    float fireCountdown = 0f;
    public GameObject lightTankBullet;
    public float attackRange;

    // Tank cannon value:
    public GameObject tankCannon;
    float turnSpeed = 10f;
    public GameObject firePoint;

    // Wheels value:
    public GameObject[] wheels;
    float wheelsSpeed;
    bool inPoint;

    bool tankSelected;
    bool inTriggerPoint;

    public GameObject greenCircle;

    GameObject destroyTrigger;

    GameObject shotSoundEffct;


    // Start is called before the first frame update
    void Start()
    {
        inPoint = true;

        shotSoundEffct = GameObject.Find("SoundManager");

        UnitSelection.Instance.unitList.Add(this.gameObject);  // Add that unit to the main list.

    }

    [System.Obsolete]
    // Update is called once per frame
    void Update()
    {
        // If the player clicked on the right click on mouse button and selected this unit.
        if (Input.GetMouseButtonDown(1) && GetComponent<UnitMovement>().enabled == true)
        {
            if (destroyTrigger != null)
            {
                Destroy(destroyTrigger);
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                agent.speed = 15f;
                distToHitPoint = hit.point;
                wheelsSpeed = 300;
                dist = 0.03f;
                inPoint = false;
                fireAttack = false;
                freeShot = true;

                // Make a trigger point in the same position the player has clicked on the map.
                // That trigger is a condition for stopping moving group at the clicked point.
                destroyTrigger = Instantiate(triggerMovement, hit.point, Quaternion.LookRotation(hit.normal));
            }

            RaycastHit hitFire;

            if (Physics.Raycast(ray, out hitFire, Mathf.Infinity, redTeam))
            {
                distToFire = hitFire.point;
                fireAttack = true;
                freeShot = false;
                CannonRotationToTarget();
            }
        }

        if (fireAttack)
        {
            attackRange = Vector3.Distance(distToFire, transform.position);
        }

        if (attackRange <= 50 && fireAttack)
        {
            agent.speed = 0;
            wheelsSpeed = 0;

            fireCountdown -= Time.deltaTime;
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f;
            }
        }
        else
        {
            fireCountdown = 0f;
        }


        range = Vector3.Distance(distToHitPoint, transform.position);

        if (range <= dist)
        {
            range = dist;
            wheelsSpeed = 0;
            inPoint = true;
            tankSelected = false;
        }
        else if (tankSelected && inTriggerPoint)
        {
            agent.speed = 0;
            wheelsSpeed = 0;
            inPoint = true;
            tankSelected = false;
            inTriggerPoint = false;
        }

        if (inPoint == false && GetComponent<UnitMovement>().enabled == true)
        {
            tankSelected = true;
        }

        if (tankSelected)
        {
            if (GetComponent<FindRedTeam>().enemyTarget == null)
            {
                CannonRotation();

                if (agent.speed == 0)
                {
                    inPoint = true;
                }
            }

            // Rotate all the 4 wheels together
            foreach (GameObject wheel in wheels)
            {
                wheel.transform.Rotate(wheelsSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    public void CannonRotation()
    {
        // Tank cannon rotation - look at the hit point (where the player click on the map).
        Vector3 dir = this.distToHitPoint - tankCannon.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tankCannon.transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        tankCannon.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void CannonRotationToTarget()
    {
        // Tank cannon rotation - look at the enemy point.
        Vector3 dir = this.distToFire - tankCannon.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(tankCannon.transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        tankCannon.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void CannonRotationToEnemy()
    {
        // Tank cannon rotation - look at the nearest enemy.
        Vector3 dir = GetComponent<FindRedTeam>().enemyTarget.position - tankCannon.transform.position;
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
            bullet.Seek(GetComponent<FindRedTeam>().enemyTarget);
        }
    }

    private void OnDestroy()
    {
        // When the unit has been destroyed - remove from the main list.
        UnitSelection.Instance.unitList.Remove(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //  When the player touch in the trigger.
        if (other.gameObject.tag == "TriggerMovement")
        {
            inTriggerPoint = true;
        }
    }
}
