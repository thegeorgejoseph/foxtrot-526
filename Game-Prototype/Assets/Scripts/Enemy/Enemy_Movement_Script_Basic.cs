using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Enemy_Movement_Script_Basic : MonoBehaviour
{
    // Four boundary wall position to stop enemy's movement
    private GameObject leftWall;
    private GameObject rightWall;
    private GameObject topWall;
    private GameObject bottonWall;

    public Rigidbody2D rb2d; // rb2d for enemy

    private bool canMove; // Var to tell if the enemy can move or not

    public Enemy_Battle_Scripts battleStatus; // Check if player is in battle to stop enemy movement;

    private float movementSpeed = 2.5f;
    private Vector2 movementVec;

    private int frameCount = 0;

    private Dictionary<string, float> movementSpeedMapping = new Dictionary<string, float>
    {
        { "Level_1", 2.5f },
        { "Level_2", 2.0f },
        { "Level_3", 1.5f },
        { "Level_4", 2.0f },
        { "Level_5", 1.5f },
        { "Level_6", 2.5f },
        { "Level_7", 1.0f },
        { "Level_8", 1.5f }
    };

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        leftWall = GameObject.Find("LeftMostWall");
        rightWall = GameObject.Find("RightMostWall");
        topWall = GameObject.Find("TopWall");
        bottonWall = GameObject.Find("BottonWall");
        movementVec.x = 0;
        movementVec.y = 0;

        String sceneName = SceneManager.GetActiveScene().name;
        movementSpeed = movementSpeedMapping[sceneName];
    }

    void FixedUpdate()
    {
        if (canMove && !battleStatus.checkBattleStatus())
        {
            if (frameCount < 25)
            {
                rb2d.MovePosition(rb2d.position + movementVec * movementSpeed * Time.fixedDeltaTime);
                frameCount++;
            }
            else
            {
                frameCount = 0;
                movementVec.x = 0;
                movementVec.y = 0;
                int roll = UnityEngine.Random.Range(0, 5);
                if (roll == 1)
                {
                    movementVec.y = 1;
                }
                else if (roll == 2)
                {
                    movementVec.x = 1;
                }
                else if (roll == 3)
                {
                    movementVec.y = -1;
                }
                else if (roll == 4)
                {
                    movementVec.x = -1;
                }
            }
            // canMove = false;
            // Randomly decide which axis to move (x or y in a 50:50 chance)
            // int roll = Random.Range(0, 5);

            // if (roll == 1)
            // {
            //     rb2d.MovePosition(rb2d.position + new Vector2(0.0f, 1.26f));
            // }
            // else if (roll == 2)
            // {
            //     rb2d.MovePosition(rb2d.position + new Vector2(0.0f, -1.25f));
            // }
            // else if (roll == 3)
            // {
            //     rb2d.MovePosition(rb2d.position + new Vector2(-1.25f, 0.0f));
            // }
            // else if (roll == 4)
            // {
            //     rb2d.MovePosition(rb2d.position + new Vector2(1.25f, 0.0f));
            // }

            // if (roll == 1 && (transform.position.x + 1.25f) < rightWall.transform.position.x)
            // {
            //     // Move in x pos
            //     this.transform.position = new Vector2(transform.position.x + 1.25f, transform.position.y);
            // }
            // else if (roll == 2 && (transform.position.x - 1.25f) > leftWall.transform.position.x)
            // {
            //     // Move in x neg
            //     this.transform.position = new Vector2(transform.position.x - 1.25f, transform.position.y);
            // }
            // else if (roll == 3 && (transform.position.y + 1.25f) < topWall.transform.position.y)
            // {
            //     // Move in y pos
            //     this.transform.position = new Vector2(transform.position.x, transform.position.y + 1.25f);
            // }
            // else if (roll == 4 && (transform.position.y - 1.25f) > bottonWall.transform.position.y)
            // {
            //     // Move in y neg
            //     this.transform.position = new Vector2(transform.position.x, transform.position.y - 1.25f);
            // }
            // Follow line is the basic ver of movement where enemy would move in both x and y
            // this.transform.position = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(-1.5f, 1.5f), 0f);

            // StartCoroutine(CountDown(1)); // Move the enemy for every 1 second
        }
    }

    /*
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" && this.tag == "Defeated")
        {
            canMove = false;
            this.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log("STOP!");
            StartCoroutine(CountDown(1));
        }
    }
    */

    // Coroutine for Enemy's movement cool down
    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        canMove = true;
        //Debug.Log("GO!");
        //this.GetComponent<BoxCollider2D>().isTrigger = false;
    }

}
