using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelselectorScript : MonoBehaviour
{
    public static int spaceship_level;
    public TMP_Text planet_1;
    public TMP_Text planet_2;
    public TMP_Text planet_3;
    public TMP_Text planet_4;
    public TMP_Text planet_5;
    public TMP_Text planet_6;
    public TMP_Text planet_7;
    public TMP_Text planet_8;

    public static bool level1Completed = false;
    public static bool level2Completed = false;
    public static bool level3Completed = false;
    public static bool level4Completed = false;
    public static bool level5Completed = false;
    public static bool level6Completed = false;
    public static bool level7Completed = false;
    public static bool level8Completed = false;

    public static float level1Score = 0;
    public static float level2Score = 0;
    public static float level3Score = 0;
    public static float level4Score = 0;
    public static float level5Score = 0;
    public static float level6Score = 0;
    public static float level7Score = 0;
    public static float level8Score = 0;
    public static float overallGameScore = 0;

    public TMP_Text score;
    public TMP_Text planet_1_score;
    public TMP_Text planet_2_score;
    public TMP_Text planet_3_score;
    public TMP_Text planet_4_score;
    public TMP_Text planet_5_score;
    public TMP_Text planet_6_score;
    public TMP_Text planet_7_score;
    public TMP_Text planet_8_score;

    public GameObject[] gameObject;
    public GameObject[] levelObject;


    // Start is called before the first frame update
    void Start()
    {
        planet_1_score.text = level1Score.ToString();
        Debug.Log("L1 score " + level1Score);
        planet_2_score.text = level2Score.ToString();
        planet_3_score.text = level3Score.ToString();
        planet_4_score.text = level4Score.ToString();
        planet_5_score.text = level5Score.ToString();
        planet_6_score.text = level6Score.ToString();
        planet_7_score.text = level7Score.ToString();
        planet_8_score.text = level8Score.ToString();
        overallGameScore = level1Score + level2Score + level3Score + level4Score + level5Score + level6Score + level7Score + level8Score;
        Debug.Log("Init Score " + overallGameScore);
        score.text = overallGameScore.ToString();
        

        gameObject = GameObject.FindGameObjectsWithTag ("Highscore");
        levelObject = GameObject.FindGameObjectsWithTag ("LevelScore");

        foreach(GameObject go in gameObject)
        {
            go.SetActive(false);           
        }

        foreach(GameObject level in levelObject)
        {
            level.SetActive(false);
        }

        if(level1Completed){
           planet_1.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_1"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet1_score"){
                    level.SetActive(true);
                }   
            } 
        }

        if(level2Completed){
           planet_2.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_2"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet2_score"){
                    level.SetActive(true);
                }   
            } 
        }

        if(level3Completed){
           planet_3.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_3"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet3_score"){
                    level.SetActive(true);
                }   
            } 
        }

        if(level4Completed){
           planet_4.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_4"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet4_score"){
                    level.SetActive(true);
                }   
            } 
        }
        
        if(level5Completed){
           planet_5.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_5"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet5_score"){
                    level.SetActive(true);
                }   
            } 
        }
        
        if(level6Completed){
           planet_6.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_6"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet6_score"){
                    level.SetActive(true);
                }   
            } 
        }

        if(level7Completed){
           planet_7.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_7"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet7_score"){
                    level.SetActive(true);
                }   
            } 
        }

        if(level8Completed){
           planet_8.alignment = TextAlignmentOptions.Top;
           foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_8"){
                    go.SetActive(true);
                }
            }

            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet8_score"){
                    level.SetActive(true);
                }   
            } 
        }

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
           

            foreach(GameObject go in gameObject)
            {
                if(go.name == "highscore_2" || go.name == "highscore_1"){
                    go.SetActive(true);
                }
            }
            
            foreach(GameObject level in levelObject)
            {
                if(level.name == "planet1_score" || level.name == "planet2_score"){
                    level.SetActive(true);
                }   
            }

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
            
            planet_5.text = "Titan";
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
            
            planet_5.text = "Titan";
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
            
            planet_5.text = "Titan";
            planet_5.color = Color.green;
            
            planet_6.text = "IO";
            planet_6.color = Color.green;
            
            planet_7.text = "Asteroid Belt";
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
            
            planet_5.text = "Titan";
            planet_5.color = Color.green;
            
            planet_6.text = "IO";
            planet_6.color = Color.green;

            planet_7.text = "Asteroid Belt";
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
    
    public void unlockAllLevels() {
            Debug.Log("In the level");
            planet_2.text = "Venus";
            planet_2.color = Color.green;

            planet_3.text = "Moon";
            planet_3.color = Color.green;

            planet_4.text = "Mars";
            planet_4.color = Color.green;

            planet_5.text = "Titan";
            planet_5.color = Color.green;

            planet_6.text = "IO";
            planet_6.color = Color.green;

            planet_7.text = "Asteroid Belt";
            planet_7.color = Color.green;

            planet_8.text = "Charon";
            planet_8.color = Color.green;

            CameraScript.level_num = 8;
            Exit_Script.level_num = 7;

    }
}
