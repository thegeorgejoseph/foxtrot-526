using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject portal;
    public GameObject player;
    public static bool is_teleport = true;
    public static bool is_trigger_allowed = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (is_teleport)
            {
                is_teleport = false;
                is_trigger_allowed = false;
                player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);   

            }
            else
            {
                is_trigger_allowed = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (is_trigger_allowed)
        {
            is_teleport = true;
        }
        
    }
}
