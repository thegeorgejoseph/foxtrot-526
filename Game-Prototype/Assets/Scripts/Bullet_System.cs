using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bullet_System : MonoBehaviour
{
    public TextMeshProUGUI bulletDisplay; // Text box to display remaining bullet
    public TextMeshProUGUI bulletDisplay_slider_hit;
    public TextMeshProUGUI bulletDisplay_slider_miss;
    public GameObject player; // To display slider battle & some other
    public GameObject enemies; // To get the total number of enemies in this level

    private int bulletNum; // # of remaining bullet
    private int remaining;

    void setBulletNum()
    {
        // Current logic: the # of bullet = total # of enemies of this level
        bulletNum = enemies.transform.childCount; 
    }

    public void setCurBulletNum(int decreaseNum)
    {
        if (bulletNum - decreaseNum >= 0)
        {
            bulletNum -= decreaseNum;

        }  
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        setBulletNum();
        //bulletNum = 1; // For testing only
    }

    // Update is called once per frame
    void Update()
    {
        bulletDisplay.text = "" + bulletNum;
        //bulletDisplay_slider_hit.text = "Bullets Remaining: " + (getBulletNum()-1);
        //bulletDisplay_slider_miss.text = "Bullets Remaining: " + (getBulletNum()-1);
        
    }

    public int getBulletNum()
    {
        // Get method to retrive the # of remaining bullet
        return bulletNum;
    }
}
