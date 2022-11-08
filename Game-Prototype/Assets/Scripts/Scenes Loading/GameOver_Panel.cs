using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Panel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        HealthManager.health = 1.0f;
    }

    public void BackToSelect()
    {
        SceneManager.LoadScene("Level_Menu");
        //Time.timeScale = 1f;
    }

    public void loadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_Menu");
        HealthManager.health = 1.0f;
    }

    public void loadLevel2()
    {
        Time.timeScale = 1;
        Loader.Load(Loader.Scene.Level_2);
    }

    public void loadLevel0()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_0");
        HealthManager.health = 1.0f;
    }

    public void loadLevelNameEnterScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("InputNameScene");
    }

    public void loadHighScoreTotal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameHighScore");
    }
}
