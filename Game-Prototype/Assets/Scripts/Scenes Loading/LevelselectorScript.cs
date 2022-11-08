using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelselectorScript : MonoBehaviour
{
    public static int spaceship_level;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CameraScript.level_num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Planet1Collider")
        {
            spaceship_level = 1;
            SceneManager.LoadScene("LevelSplashScreen");
        }
        else if (collision.gameObject.name == "Planet2Collider")
        {
            if(CameraScript.level_num>=2)
            {
                spaceship_level = 2;
                SceneManager.LoadScene("LevelSplashScreen");
            }
            
        }
        else if (collision.gameObject.name == "Planet3Collider")
        {
            if (CameraScript.level_num >= 3)
            {
                spaceship_level = 3;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet4Collider")
        {
            if (CameraScript.level_num >= 4)
            {
                spaceship_level = 4;
                SceneManager.LoadScene("LevelSplashScreen");

            }
                
        }
        else if (collision.gameObject.name == "Planet5Collider")
        {
            if (CameraScript.level_num >= 5)
            {
                spaceship_level = 5;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet6Collider")
        {
            if (CameraScript.level_num >= 6)
            {
                spaceship_level = 6;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet7Collider")
        {
            if (CameraScript.level_num >= 7)
            {
                spaceship_level = 7;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet8Collider")
        {
            if (CameraScript.level_num >= 8)
            {
                spaceship_level = 8;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }

    }
}
