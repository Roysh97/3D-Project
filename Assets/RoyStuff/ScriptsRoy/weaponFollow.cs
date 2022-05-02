using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponFollow : MonoBehaviour
{
    GameObject enemy;
    Transform enemyTrn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.FindWithTag("Player2");

        if (enemy != null)
        {
            enemyTrn = GameObject.FindWithTag("Player2").transform;
            transform.LookAt(enemyTrn);
        }

        else
        {

        }

    }
}
