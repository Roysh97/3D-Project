using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    // Tank:
    public AudioClip lightTankShot;
    public AudioClip lightTankExplosion;

    //Bullets:
    public AudioClip lightTankBullet;
    public AudioClip turretFire;

    // Building:
    public AudioClip powerBuildingExplosion;
    public AudioClip fuelFactoryExplosion;


    // Tank functions:
    public void LightTankShot()
    {
        audioSource.PlayOneShot(lightTankShot);
    }

    public void LightTankExplosion()
    {
        audioSource.PlayOneShot(lightTankExplosion);
    }


    //Bullet functions:
    public void LightTankBullet()
    {
        audioSource.PlayOneShot(lightTankBullet);
    }

    public void TurretFire()
    {
        audioSource.PlayOneShot(turretFire);
    }

    // Building functions:
    public void PowerBuildingExplosion()
    {
        audioSource.PlayOneShot(powerBuildingExplosion);
    }

    public void FuelFactoryExplosion()
    {
        audioSource.PlayOneShot(fuelFactoryExplosion);
    }
}
