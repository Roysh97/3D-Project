using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMovement : MonoBehaviour
{
    Rigidbody rb;

    public bool isAim;

    float speed = 50;

    GameObject ballKick;

    float posX = -0.32f, posY = 0.445f, posZ = -0.332f;

    // Start is called before the first frame update
    void Start()
    {
        //isAim = true;

        rb = GetComponent<Rigidbody>();

        ballKick = GameObject.FindWithTag("Ball");

        //ballKick = GameObject.FindWithTag("Ball");

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void MoveUp()
    {
        if(ballKick.GetComponent<BallKick>().iskick == false)
        {
            rb.velocity = new Vector3(0, speed * Time.deltaTime, 0);
        }

        /*if (isAim == true)
        {
            rb.velocity = new Vector3(0, speed * Time.deltaTime, 0);
        }*/
    }

    public void MoveDown()
    {
        if(ballKick.GetComponent<BallKick>().iskick == false)
        {
            rb.velocity = new Vector3(0, -speed * Time.deltaTime, 0);
        }

       /* if (isAim == true)
        {
            rb.velocity = new Vector3(0, -speed * Time.deltaTime, 0);
        }*/
    }

    public void MoveRight()
    {
        if(ballKick.GetComponent<BallKick>().iskick == false)
        {
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        }

      /*  if (isAim == true)
        {
            rb.velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        }*/
    }

    public void MoveLeft()
    {
        if(ballKick.GetComponent<BallKick>().iskick == false)
        {
            rb.velocity = new Vector3(-speed * Time.deltaTime, 0, 0);
        }

       /* if (isAim == true)
        {
            rb.velocity = new Vector3(-speed * Time.deltaTime, 0, 0);
        }*/
    }

    public void StopMove()
    {
        rb.velocity = new Vector3(0, 0, 0);

     /*   if (isAim == true)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            transform.position = new Vector3(posX, posY, posZ);

            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
