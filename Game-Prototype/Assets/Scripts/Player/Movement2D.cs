using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
            
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
        
        if(movementVec.y<0){
            Player.GetComponent<SpriteRenderer>().sprite=idleDownSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = idleDownAnimator;
        }
        else if(movementVec.y>0){
            Player.GetComponent<SpriteRenderer>().sprite=idleUpSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = idleUpAnimator;
        }
        // else{
        //     Player.GetComponent<SpriteRenderer>().sprite=idleSideSprite;
        // }
        if (movementVec.x < 0)
        {
            Player.GetComponent<SpriteRenderer>().sprite=idleSideSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = idleSideAnimator;
            Player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (movementVec.x > 0)
        {
            Player.GetComponent<SpriteRenderer>().sprite=idleSideSprite;
            Player.GetComponent<Animator>().runtimeAnimatorController = idleSideAnimator;
            Player.GetComponent<SpriteRenderer>().flipX = true;
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