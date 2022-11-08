using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelselectorScript : MonoBehaviour
{
    public static int spaceship_level;
    public TMP_Text planet_2;
    public TMP_Text planet_3;
    public TMP_Text planet_4;
    public TMP_Text planet_5;
    public TMP_Text planet_6;
    public TMP_Text planet_7;
    public TMP_Text planet_8;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CameraScript.level_num);
        if (CameraScript.level_num == 2 || Exit_Script.level_num == 1)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;
        }
        if (CameraScript.level_num == 3 || Exit_Script.level_num == 2)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;
        }
        if (CameraScript.level_num == 4 || Exit_Script.level_num == 3)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;

            planet_4.text = "Mars";
            planet_4.color = Color.green;
        }
        if (CameraScript.level_num == 5 || Exit_Script.level_num == 4)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;

            planet_4.text = "Mars";
            planet_4.color = Color.green;

            planet_5.text = "Asteroid Belt";
            planet_5.color = Color.green;
        }
        if (CameraScript.level_num == 6 || Exit_Script.level_num == 5)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;

            planet_4.text = "Mars";
            planet_4.color = Color.green;

            planet_5.text = "Asteroid Belt";
            planet_5.color = Color.green;

            planet_6.text = "IO";
            planet_6.color = Color.green;
        }
        if (CameraScript.level_num == 7 || Exit_Script.level_num == 6)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;

            planet_4.text = "Mars";
            planet_4.color = Color.green;

            planet_5.text = "Asteroid Belt";
            planet_5.color = Color.green;

            planet_6.text = "IO";
            planet_6.color = Color.green;

            planet_7.text = "Titan";
            planet_7.color = Color.green;
        }
        if (CameraScript.level_num == 8 || Exit_Script.level_num == 7)
        {
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;

            planet_4.text = "Mars";
            planet_4.color = Color.green;

            planet_5.text = "Asteroid Belt";
            planet_5.color = Color.green;

            planet_6.text = "IO";
            planet_6.color = Color.green;

            planet_7.text = "Titan";
            planet_7.color = Color.green;

            planet_8.text = "Charon";
            planet_8.color = Color.green;
        }
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
            if(CameraScript.level_num>=2 || Exit_Script.level_num == 1)
            {
                spaceship_level = 2;
                SceneManager.LoadScene("LevelSplashScreen");
            }
            
        }
        else if (collision.gameObject.name == "Planet3Collider")
        {
            if (CameraScript.level_num >= 3 || Exit_Script.level_num == 2)
            {
                spaceship_level = 3;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet4Collider")
        {
            if (CameraScript.level_num >= 4 || Exit_Script.level_num == 3)
            { 
                spaceship_level = 4;
                SceneManager.LoadScene("LevelSplashScreen");

            }
                
        }
        else if (collision.gameObject.name == "Planet5Collider")
        {
            if (CameraScript.level_num >= 5 || Exit_Script.level_num == 4)
            {
                spaceship_level = 5;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet6Collider")
        {
            if (CameraScript.level_num >= 6 || Exit_Script.level_num == 5)
            {
                spaceship_level = 6;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet7Collider")
        {
            if (CameraScript.level_num >= 7 || Exit_Script.level_num == 6)
            {
                spaceship_level = 7;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }
        else if (collision.gameObject.name == "Planet8Collider")
        {
            if (CameraScript.level_num >= 8 || Exit_Script.level_num == 7)
            {
                spaceship_level = 8;
                SceneManager.LoadScene("LevelSplashScreen");
            }
                
        }

    }
}
