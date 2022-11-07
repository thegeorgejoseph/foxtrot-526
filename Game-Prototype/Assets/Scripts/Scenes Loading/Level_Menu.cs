using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoadLevel_0(){
        SceneManager.LoadScene("Level_0");
        Time.timeScale = 1f;
    }
    public void RoadLevel_1()
    {
        SceneManager.LoadScene("Level_1");
        Time.timeScale = 1f;
    }

    public void RoadLevel_2()
    {
        SceneManager.LoadScene("Level_2");
        Time.timeScale = 1f;
    }

    public void RoadLevel_3()
    {
        SceneManager.LoadScene("Level_3");
        Time.timeScale = 1f;
    }

    public void RoadLevel_4()
    {
        SceneManager.LoadScene("Level_4");
        Time.timeScale = 1f;
    }

    public void RoadLevel_5()
    {
        SceneManager.LoadScene("Level_5");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
