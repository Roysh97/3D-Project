using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public GameObject computerScored;

    GameObject ballKick;

    GameObject levelManger;

    // Start is called before the first frame update
    void Start()
    {
        ballKick = GameObject.FindWithTag("Ball");

        levelManger = GameObject.Find("Level Manager");
    }

    // Update is called once per frame
    void Update()
    {
        if(levelManger.GetComponent<LevelManager>().isPractice == true)
        {
            computerScored.SetActive(false);
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall1 == true)
        {
            levelManger.GetComponent<LevelManager>().isDisBall2 = false;
            levelManger.GetComponent<LevelManager>().isDisBall3 = false;

            if (ballKick.GetComponent<BallKick>().iscomputerScore == true)
            {
                computerScored.SetActive(true);
            }
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall2 == true)
        {
            levelManger.GetComponent<LevelManager>().isDisBall1 = false;
            levelManger.GetComponent<LevelManager>().isDisBall3 = false;

            if (ballKick.GetComponent<BallKick>().iscomputerScore == true)
            {
                computerScored.SetActive(true);
            }
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall3 == true)
        {
            levelManger.GetComponent<LevelManager>().isDisBall1 = false;
            levelManger.GetComponent<LevelManager>().isDisBall2 = false;

            if (ballKick.GetComponent<BallKick>().iscomputerScore == true)
            {
                computerScored.SetActive(true);
            }
        }
    }

    public void ComputerGoal()
    {
        ballKick.GetComponent<BallKick>().isReturnPlace = false;

        ballKick.GetComponent<BallKick>().iscomputerScore = false;

        computerScored.SetActive(false);
    }
}

   
