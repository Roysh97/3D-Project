using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject lightTank;
    public GameObject spawnPosion;
    public static Transform[] wayPoints;

    float countDownWaveAttack;
    float countDownSpawnPosion;

    public static bool waveIsOver;


    void Awake()
    {
        wayPoints = new Transform[transform.childCount];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        countDownWaveAttack = 120f;
        countDownSpawnPosion = 5f;
    }


    void Update()
    {
        countDownWaveAttack -= Time.deltaTime;

        if (countDownWaveAttack <= 0)
        {
            int i = 5;

            while (i >0)
            {
                countDownSpawnPosion -= Time.deltaTime;

                if (countDownSpawnPosion <= 0)
                {
                    Instantiate(lightTank, spawnPosion.transform.position, spawnPosion.transform.rotation);
                    countDownSpawnPosion = 5f;
                    i--;
                }  
            }

            countDownWaveAttack = 120f;
        }


    }
}
