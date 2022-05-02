using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject lightTankBullet;
    public GameObject firePoint;

    float speedBollet = 40f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //lightTankBullet.transform.localRotation = Quaternion.Euler(firePoint.transform.rotation.x, firePoint.transform.rotation.y, firePoint.transform.rotation.z);

        //lightTankBullet.transform.Translate(Vector3.forward * speedBollet * Time.deltaTime, Space.World);
    }
}
