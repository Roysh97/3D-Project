using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    // Reference to checkpoint position
    [SerializeField]
    private Transform checkpoint;

    // Reference to UI text that shows the distance value
    [SerializeField]
    private Text distanceText;

    // Calculated distance value
    private float distance;

    //if player chose a distance that is higher than 30 or lower than 10
    //then the game randomly pick a distance between 10 - 30

    float posX = -0.3f, posY = 0.141f, posZ = 0;

    int distanceBall;

    bool isRandom;

    GameObject levelManger;

    GameObject ballKick;

    // Update is called once per frame
    void Update()
    {
        levelManger = GameObject.Find("Level Manager");

        ballKick = GameObject.FindWithTag("Ball");

        // Calculate distance value between character and checkpoint
        distance = (checkpoint.transform.position - transform.position).magnitude;

        // Display distance value via UI text
        // distance.ToString("F0") shows value with 0 digit after period
        // so 30.5 will be shown as 30 for example
        // distance.ToString("F1") shows value with 1 digit after period
        // so 12.234 will be shown as 12.2 for example
        // distance.ToString("F2") will show 12.23 in this case
        distanceText.text = "Distance: " + distance.ToString("F0") + "m";

        if (levelManger.GetComponent<LevelManager>().isDisBall1 == false && levelManger.GetComponent<LevelManager>().isDisBall2 == false && levelManger.GetComponent<LevelManager>().isDisBall3 == false)
        {
            if (distance > 30 && ballKick.GetComponent<BallKick>().isReturnPlace == true)
            {
                isRandom = true;

                if (isRandom == true)
                {
                    posZ = Random.Range(-10, -20);
                    transform.position = new Vector3(posX, posY, posZ - distanceBall);
                    isRandom = false;
                }
            }

            else if (distance <= 9 && ballKick.GetComponent<BallKick>().isReturnPlace == true)
            {
                isRandom = true;

                if (isRandom == true)
                {
                    posZ = Random.Range(-10, -20);
                    transform.position = new Vector3(posX, posY, posZ - distanceBall);
                    isRandom = false;
                }
            }
        }
    }
}
