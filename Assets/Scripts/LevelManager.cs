using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool isGravity;

    public bool isGoalMoves;

    public bool isPractice;

    public bool isDisBall1;

    public bool isDisBall2;

    public bool isDisBall3;

    public bool isStartPlaying;

    public InputField ballDisField;

    public static string disValue;

    public bool RandDistance;    //Rand = Random

    public Toggle gravityToggle;

    public Toggle gateMovementToggle;

    public Toggle randomDisToggle;

    bool isStartGame;

    // Update is called once per frame
    void Update()
    {
        disValue = ballDisField.text;

        if (gravityToggle.isOn)
        {
            isGravity = true;
        }

        else
        {
            isGravity = false;
        }

        if (gateMovementToggle.isOn)
        {
            isGoalMoves = true;
        }

        else
        {
            isGoalMoves = false;
        }

        if (randomDisToggle.isOn)
        {
            RandDistance = true;
        }

        else
        {
            RandDistance = false;
        }

          if(disValue == "10" || disValue == "11" || disValue == "12" || disValue == "13" || disValue == "14")
          {
              Debug.Log("Correct Distance");
              isStartGame = true;
          }

          else if (disValue == "15" || disValue == "16" || disValue == "17" || disValue == "18" || disValue == "19")
          {
              Debug.Log("Correct Distance");
              isStartGame = true;
          }

          else if (disValue == "20" || disValue == "21" || disValue == "22" || disValue == "23" || disValue == "24")
          {
              Debug.Log("Correct Distance");
              isStartGame = true;
          }

          else if (disValue == "25" || disValue == "26" || disValue == "27" || disValue == "28" || disValue == "29")
          {
              Debug.Log("Correct Distance");
              isStartGame = true;
          }

          else if (disValue == "30")
          {
              Debug.Log("Correct Distance");
              isStartGame = true;
          }

          else
          {
              Debug.Log("Wrong Distance");
              isStartGame = false;
          }
    }

    public void Level1()
    {
        isDisBall1 = true;
        isDisBall2 = false;
        isDisBall3 = false;
    }
    public void Level2()
    {
        isDisBall2 = true;
        isDisBall1 = false;
        isDisBall3 = false;
    }
    public void Level3()
    {
        isDisBall3 = true;
        isDisBall1 = false;
        isDisBall2 = false;
    }

    public void UseGravitionON()
    {
        isGravity = true;
    }

    public void GoalMoves()
    {
        isGoalMoves = true;
    }

    public void RandomDistance()
    {
        RandDistance = true;
    }

    public void PracticeButton()
    {
        isPractice = true;
        SceneManager.LoadScene("GameSceneApk");
        DontDestroyOnLoad(gameObject);
        isDisBall1 = false;
        isDisBall2 = false;
        isDisBall3 = false;
    }

    public void StartGame()
    {
        if(isStartGame == true)
        {
            SceneManager.LoadScene("GameSceneApk");
            DontDestroyOnLoad(gameObject);
        }
        /*SceneManager.LoadScene("GameSceneApk");
        DontDestroyOnLoad(gameObject);*/
    }
}
