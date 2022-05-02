using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightTankEngine : UnitMovement
{
    float range;
    float dist = 0.72f;  // Distance from the agent distanion value.

    // Attack value:
    float attackRange;
    public bool fireAttack;
    float fireCountdown = 0f;
    public GameObject lightTankBullet;

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

        health = 130;

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
                agent.speed = 4.5f;
                distToHitPoint = hit.point;
                wheelsSpeed = 250;
                dist = 0.72f;
                inPoint = false;
                fireAttack = false;

                // Make a trigger point in the same position the player has clicked on the map.
                // That trigger is a condition for stopping moving group at the clicked point.
                destroyTrigger = Instantiate(triggerMovement, hit.point, Quaternion.LookRotation(hit.normal));
            }

            RaycastHit hitFire;

            if (Physics.Raycast(ray, out hitFire, Mathf.Infinity, redTeam))
            {
                distToFire = hitFire.point;
                fireAttack = true;
            }
        }

        attackRange = Vector3.Distance(distToFire, transform.position);

        if (attackRange <= 18 && fireAttack && GetComponent<FindRedTeam>().enemyTarget != null)
        {
            agent.speed = 0;
            wheelsSpeed = 0;
            Debug.Log("what?!");
            
            fireCountdown -= Time.deltaTime;
            if(fireCountdown <= 0)
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
            // Tank cannon rotation - look at the hit point (where the player click on the map).
            Vector3 dir = this.distToHitPoint - tankCannon.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(tankCannon.transform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
            tankCannon.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            // Rotate all the 4 wheels together
            foreach (GameObject wheel in wheels)
            {
                wheel.transform.Rotate(wheelsSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    void Shoot()
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
        //  
        if (other.gameObject.tag == "TriggerMovement")
        {
            inTriggerPoint = true;
        }
    }
}
