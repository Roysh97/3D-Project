using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
     float speed;

     GameObject canonTag;

    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
    }

    // Update is called once per frame
    void Update()
    {
        canonTag = GameObject.FindWithTag("Canon");

        transform.Translate(0, 0, speed * Time.deltaTime);

        if (canonTag != null)
        {
            Destroy(gameObject, 3);
        }
    }
}
