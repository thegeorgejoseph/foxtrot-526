using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleEnemy : MonoBehaviour
{
    /** !!! Experimental Feature !!! **/
    // The purpose of this script is the make enemy invisible if the 
    // player has a distance greater than x
    public GameObject Enemies;
    public float visibleDistance; // How close could the player see the enemy
    
    // Start is called before the first frame update
    void Start()
    {
        visibleDistance = 2.5f;
    }

    void FixedUpdate()
    {
        makeVisible();
    }

    void makeVisible()
    {
        for (int i = 0; i < Enemies.transform.childCount; i++)
        {
            Transform curChild = Enemies.transform.GetChild(i);
            float dist = Vector2.Distance(this.transform.position, curChild.position);
            if (dist > visibleDistance)
            {
                curChild.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                curChild.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
