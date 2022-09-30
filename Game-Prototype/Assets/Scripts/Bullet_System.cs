using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bullet_System : MonoBehaviour
{
    public TextMeshProUGUI bulletDisplay; // Text box to display remaining bullet
    public GameObject player; // To display slider battle & some other
    public GameObject enemies; // To get the total number of enemies in this level

    private int bulletNum; // # of remaining bullet

    void setBulletNum()
    {
        // Current logic: the # of bullet = total # of enemies of this level + 1
        bulletNum = enemies.transform.childCount + 1; 
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
    }

    public int getBulletNum()
    {
        // Get method to retrive the # of remaining bullet
        return bulletNum;
    }
}
