using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public Rigidbody2D rb2d;
    Vector2 movementVec;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movementVec.x = Input.GetAxisRaw("Horizontal");
        movementVec.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, 1.25f));
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            rb2d.MovePosition(rb2d.position + new Vector2(0.0f, -1.25f));
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            rb2d.MovePosition(rb2d.position + new Vector2(-1.25f, 0.0f));
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            rb2d.MovePosition(rb2d.position + new Vector2(1.25f, 0.0f));
        }
    }

    // FixedUpdate is called 50 times per second (50 fps fixed)
    // void FixedUpdate() 
    // {
    //     // rb2d.MovePosition(rb2d.position + movementVec * movementSpeed * Time.fixedDeltaTime); 
    // }

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
