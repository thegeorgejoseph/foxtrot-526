using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Movement2D : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public Rigidbody2D rb2d;
    Vector2 movementVec;
    public GameObject Player;
    public float portalUsageCount = 0;
    public Sprite idleUpSprite;
    public Sprite idleDownSprite;
    public Sprite idleSideSprite;
    public RuntimeAnimatorController idleUpAnimator;
    public RuntimeAnimatorController idleDownAnimator;
    public RuntimeAnimatorController idleSideAnimator;
    public Sprite runUpSprite;
    public Sprite runDownSprite;
    public Sprite runSideSprite;
    public RuntimeAnimatorController runUpAnimator;
    public RuntimeAnimatorController runDownAnimator;
    public RuntimeAnimatorController runSideAnimator;
    int direction;

    private Dictionary<string, float> movementSpeedMapping = new Dictionary<string, float>
    {
        { "Level Selector", 40.0f },
        { "Level_0", 5.0f },
        { "Level_1", 5.0f },
        { "Level_2", 4.5f },
        { "Level_3", 2.5f },
        { "Level_4", 4.0f },
        { "Level_5", 3.0f },
        { "Level_6", 5.0f },
        { "Level_7", 2.5f },
        { "Level_8", 2.5f }
    };

    // Start is called before the first frame update
    void Start()
    {
        String sceneName = SceneManager.GetActiveScene().name;
        movementSpeed = movementSpeedMapping[sceneName];
    }

    // Update is called once per frame
    void Update()
    {
        movementVec.x = Input.GetAxisRaw("Horizontal");
        movementVec.y = Input.GetAxisRaw("Vertical");
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     rb2d.MovePosition(rb2d.position + new Vector2(0.0f, 1.25f));
        // }
        // else if (Input.GetKeyDown(KeyCode.DownArrow))
        // {
        //     rb2d.MovePosition(rb2d.position + new Vector2(0.0f, -1.25f));
        // }
        // else if (Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     rb2d.MovePosition(rb2d.position + new Vector2(-1.25f, 0.0f));
        // }
        // else if (Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     rb2d.MovePosition(rb2d.position + new Vector2(1.25f, 0.0f));
        // }
    }


    // FixedUpdate is called 50 times per second (50 fps fixed)
    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movementVec * movementSpeed * Time.fixedDeltaTime);

        if (movementVec.y < 0)
        {
            Player.GetComponent<SpriteRenderer>().sprite = runDownSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = runDownAnimator;
            direction = 1;
        }
        else if (movementVec.y > 0)
        {
            Player.GetComponent<SpriteRenderer>().sprite = runUpSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = runUpAnimator;
            direction = 2;
        }
        // else{
        //     Player.GetComponent<SpriteRenderer>().sprite=idleSideSprite;
        // }
        if (movementVec.x < 0)
        {
            Player.GetComponent<SpriteRenderer>().sprite = runSideSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = runSideAnimator;
            Player.GetComponent<SpriteRenderer>().flipX = false;
            direction = 3;
        }
        else if (movementVec.x > 0)
        {
            Player.GetComponent<SpriteRenderer>().sprite = runSideSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = runSideAnimator;
            Player.GetComponent<SpriteRenderer>().flipX = true;
            direction = 3;
        }
        if (movementVec.x == 0 && movementVec.y == 0)
        {
            if (direction == 1)
            {
                Player.GetComponent<SpriteRenderer>().sprite = idleDownSprite;
                Player.GetComponent<Animator>().runtimeAnimatorController = idleDownAnimator;
            }
            if (direction == 2)
            {
                Player.GetComponent<SpriteRenderer>().sprite = idleUpSprite;
                Player.GetComponent<Animator>().runtimeAnimatorController = idleUpAnimator;
            }
            if (direction == 3)
            {
                Player.GetComponent<SpriteRenderer>().sprite = idleSideSprite;
                Player.GetComponent<Animator>().runtimeAnimatorController = idleSideAnimator;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Portal")
        {
            portalUsageCount += 0.5f;
        }
    }
    /*     Block below has been replaced by slider script
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall"){
            Debug.Log("Wall Hit");
        }

        if (collision.gameObject.tag == "Enemy"){
            Debug.Log("Enemy Encounter");
            collision.gameObject.active = false;
        }
    }
    */

}