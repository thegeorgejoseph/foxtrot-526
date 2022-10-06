using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject portal;
    public GameObject player;
    public static bool is_teleport = true;
    public static bool is_trigger_allowed = false;

    public GameObject analyticsManager; // GameObj to initialize analytic manager
    private AnalyticsManager analyticsManagerScript; // Analytic manager object for metric event handler

    public bool event_called; // bool to prevent calling analytics handler multiple times inside update()

    private void Awake(){
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
    
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        event_called = false;

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
                 if(!event_called){
                    
                    // analyticsManagerScript.HandleEvent("portal_use", new List<object>
                    // {
                    //     "yes"
                    // }); // send false to did_finish metric
                    event_called = true;
                }  

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
