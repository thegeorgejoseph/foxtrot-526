using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProtectorScript : MonoBehaviour
{
    public GameObject rocketProtector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collider) {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player detected, disabling protector!");
            rocketProtector.SetActive(false);
        }
    }
}
