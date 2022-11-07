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
        if (movementVec.x < 0)
        {
            Player.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (movementVec.x > 0)
        {
            Player.GetComponent<SpriteRenderer>().flipX = false;
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