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
    }

    public void loadLevel2()
    {
        Time.timeScale = 1;
        Loader.Load(Loader.Scene.Level_2);
    }
}
