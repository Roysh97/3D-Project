using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject timerCanvas;
    public Text countDownMaster;
    TimeSpan timePlaying;
    float currentTime;

    public GameObject chronosphere;
    public GameObject commandCenter;

    // Start is called before the first frame update
    void Start()
    {
        countDownMaster.text = "20:00";
        currentTime = 1200;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(currentTime);
        string timePlayingStr = timePlaying.ToString("mm':'ss");
        countDownMaster.text = timePlayingStr;

        if (currentTime <= 0 || commandCenter == null)
        {
            currentTime = 0;
            SceneManager.LoadScene("LoseScene");
        }
        else if (chronosphere == null)
        {
            SceneManager.LoadScene("WinScene");
        }

    }

}
