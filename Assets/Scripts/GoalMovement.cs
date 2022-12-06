using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMovement : MonoBehaviour
{
    public bool isStartMove;

    bool isMoveLeft;

    bool isMoveRight;

    public GameObject moveButtonPress;

    // Start is called before the first frame update
    void Start()
    {
        moveButtonPress = GameObject.Find("Level Manager");

        isStartMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveButtonPress.GetComponent<LevelManager>().isGoalMoves == true)    //when player press the gate movement button
        {
            if (isStartMove == true)
            {
                transform.Translate(-1.7f * Time.deltaTime, 0, 0);
            }
        }
        
        if (isMoveRight == true)
        {
            transform.Translate(1.7f * Time.deltaTime, 0, 0);
        }

        else if (isMoveLeft == true)
        {
            transform.Translate(-1.7f * Time.deltaTime, 0, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "CheckMovementEndR")
        {
            isMoveRight = true;
            isMoveLeft = false;
            isStartMove = false;
        }

        else if (collision.gameObject.name == "CheckMovementEndL")
        {
            isMoveLeft = true;
            isMoveRight = false;
            isStartMove = false;
        }
    }
}
