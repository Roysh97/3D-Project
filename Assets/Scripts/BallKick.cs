using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallKick : MonoBehaviour
{
    Rigidbody rbBall;

    GameObject levelManger;

    GameObject aim;

    public GameObject computerScored;

    GameObject turnManager;

    float doubleSpeed;

    float posX = -0.3f, posY = 0.141f , posZ = 0;

    public Text Score;

    float myScore;

    public float computerScore;
 
    public bool isPress;

    public Text time;

    public float powerNum;

    float timePress;

    public bool iskick;

    public bool isPlayed;

    float timeLimit;

    string disStringBall;

    int distanceBall;

    bool isRandom;

    public bool isReturnPlace;

    public int computerOdds;

    bool ispracticeReset;

    public bool iscomputerScore;

    float numberPress;

    public AudioSource SoundManager;
    public AudioClip GoalSound;

    // Start is called before the first frame update
    void Start()
    {
        levelManger = GameObject.Find("Level Manager");

        turnManager = GameObject.Find("Turn Manager");

        myScore = 0;

        computerScore = 0;

        numberPress = 0;

        powerNum = 0;

        rbBall = GetComponent<Rigidbody>();

        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;

        timePress = 0;

        isPress = false;

        iskick = false;

        timeLimit = 0;

        isRandom = true;

        isReturnPlace = false;

        ispracticeReset = false;

        computerOdds = Random.Range(1, 10);

        Debug.Log("ComputerOdds" + computerOdds);

        GetComponent<BallKick>().disStringBall = LevelManager.disValue;

        int.TryParse(disStringBall, out distanceBall);      // changes string disStringBall to int distanceBall

        transform.position = new Vector3(posX, posY, posZ - distanceBall);
    }

    // Update is called once per frame
    void Update()
    {
        if (numberPress > 1)
        {
            isPress = false;
            float num = numberPress;
        }

        aim = GameObject.FindWithTag("Aim");

        Debug.Log("numberPress" + numberPress);

        if (turnManager.GetComponent<TurnManager>().computerScored.activeSelf == true)
        {
            numberPress = 0;
            isPress = false;
            iskick = false;
            aim.GetComponent<ButtonsMovement>().isAim = false;
        }

        else if (turnManager.GetComponent<TurnManager>().computerScored.activeSelf == false)
        {
            aim.GetComponent<ButtonsMovement>().isAim = true;
        }

        Score.text = myScore + "-" + computerScore;

        time.text = "" + powerNum;

        if (iskick == true)
        {
            transform.position = Vector3.Lerp(transform.position, aim.transform.position, 1 * doubleSpeed * Time.deltaTime);     //vector3.lerp  =  draw like a line between the ball and the aim and make the ball move through that line     //linearly - קווי
            timeLimit += Time.deltaTime;
        }
       
        if (levelManger != null)       // when player presses the gravity option on  ,  the game object the script is related to wont be destroyed when moving to another scene becasue the script has DontDestroyOnLoad(gameObject) thats why we use if levelManger != null 
        {
            if (levelManger.GetComponent<LevelManager>().isGravity == true)
            {
                rbBall.useGravity = true;
            }
        }

        if (isPress == true)
        {
           timePress += Time.deltaTime;    // represent the time that was decide to 5 seconds
        }
        
        if (timePress >= 0 && isPress == true)
        {
            powerNum++;

            doubleSpeed += 0.1f;
        }

        if (powerNum > 100 && isPress == true)
        {
            powerNum = 0;
            doubleSpeed = 0;
        }

        Debug.Log("timePress" + timePress);

        if (doubleSpeed > 10)
        {
            doubleSpeed = 0;
        }

        //Debug.Log("timeLimit" + timeLimit);

        if (timeLimit >= 2)
        {
            numberPress = 0;
            powerNum = 0;
            timePress = 0;
            doubleSpeed = 0;
            timeLimit = 0;
            iskick = false;
            isPress = false;
            transform.position = new Vector3(posX, posY, posZ - distanceBall);

            if(turnManager.GetComponent<TurnManager>().computerScored.activeSelf == false)
            {
                isReturnPlace = true;
            }
          
            if (levelManger.GetComponent<LevelManager>().isPractice == true)
            {
                rbBall.useGravity = false;
                ispracticeReset = true;
                levelManger.GetComponent<LevelManager>().isGoalMoves = false;
                levelManger.GetComponent<LevelManager>().isDisBall1 = false;
                levelManger.GetComponent<LevelManager>().isDisBall2 = false;
                levelManger.GetComponent<LevelManager>().isDisBall3 = false;
                levelManger.GetComponent<LevelManager>().isPractice = false;
            }
        }

        if (levelManger.GetComponent<LevelManager>().RandDistance == true && isRandom == true)
        {
            posZ = Random.Range(-10, -20);
            transform.position = new Vector3(posX, posY, posZ - distanceBall);
            isRandom = false;
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall1 == true)      //this represents in percentage the odds of the computer to goal
        {
            levelManger.GetComponent<LevelManager>().isDisBall2 = false;
            levelManger.GetComponent<LevelManager>().isDisBall3 = false;

            if (isReturnPlace == true)
            {
                if (computerOdds <= 2)
                {
                    //Debug.Log("Computer Goal");

                    iscomputerScore = true;

                    computerScore += 1;
                }

                else if (computerOdds > 2)
                {
                    //Debug.Log("Computer Missed");
                }
            }
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall2 == true)
        {
            levelManger.GetComponent<LevelManager>().isDisBall1 = false;
            levelManger.GetComponent<LevelManager>().isDisBall3 = false;

            if (isReturnPlace == true)
            {
                if (computerOdds <= 5)
                {
                    //Debug.Log("Computer Goal");

                    iscomputerScore = true;

                    computerScore += 1;
                }

                else if (computerOdds > 5)
                {
                    //Debug.Log("Computer Missed");
                }
            }
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall3 == true)
        {
            levelManger.GetComponent<LevelManager>().isDisBall1 = false;
            levelManger.GetComponent<LevelManager>().isDisBall2 = false;

            if (isReturnPlace == true)
            {
                if (computerOdds <= 8)
                {
                    //Debug.Log("Computer Goal");

                    iscomputerScore = true;

                    computerScore += 1;
                }

                else if (computerOdds > 8)
                {
                    //Debug.Log("Computer Missed");
                }
            }
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall1 == true && isReturnPlace == true)
        {
            Debug.Log("Range" + computerOdds);

            computerOdds = Random.Range(1, 11);

            isReturnPlace = false;
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall2 == true && isReturnPlace == true)
        {
            Debug.Log("Range" + computerOdds);

            computerOdds = Random.Range(1, 11);

            isReturnPlace = false;
        }

        if (levelManger.GetComponent<LevelManager>().isDisBall3 == true && isReturnPlace == true)
        {
            Debug.Log("Range" + computerOdds);

            computerOdds = Random.Range(1, 11);

            isReturnPlace = false;
        }

        if (levelManger.GetComponent<LevelManager>().isPractice == true)     
        {
            posZ = -10;
            rbBall.useGravity = false;
            levelManger.GetComponent<LevelManager>().isGoalMoves = false;
            levelManger.GetComponent<LevelManager>().isDisBall1 = false;
            levelManger.GetComponent<LevelManager>().isDisBall2 = false;
            levelManger.GetComponent<LevelManager>().isDisBall3 = false;
            levelManger.GetComponent<LevelManager>().isPractice = false;
            ispracticeReset = true;
        }

        if (ispracticeReset == true)
        {
            transform.position = new Vector3(posX, posY, posZ - distanceBall);
        }
    }

    public void ButtonPress()
    {
       // Debug.Log("numPress" + numberPress);
        numberPress++;
        if(turnManager.GetComponent<TurnManager>().computerScored.activeSelf == false)
        {
            isPress = true;
        }
    }
    public void ButtonRelease()
    {
        isPress = false;
        timeLimit = 0;
        iskick = true;
        ispracticeReset = false;
        isReturnPlace = false;
        iscomputerScore = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Aim")
        {
            numberPress = 0;
            powerNum = 0;
            timePress = 0;
            doubleSpeed = 0;
            iskick = false;
            isReturnPlace = true;
            ispracticeReset = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            SoundManager.PlayOneShot(GoalSound);

            myScore += 1;
            numberPress = 0;
            powerNum = 0;
            timePress = 0;
            doubleSpeed = 0;
            iskick = false;
            isReturnPlace = true;
            ispracticeReset = true;
        }
    }
}
