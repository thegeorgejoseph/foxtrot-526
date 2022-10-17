using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Time_Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    public int bonus_num = 140;
    public TextMeshProUGUI display;
    public GameObject Timer;
    private HashSet<int> timeSet = new HashSet<int>();
    private Dictionary<string, int> timeLimits = new Dictionary<string, int>
        {
            { "Level_0", 35 },
            { "Level_1", 35 },
            { "Level_2", 45 },
            { "Level_3", 55 },
            { "Level_4", 75 },
            { "Level_5", 105 }
        };
    void Start()
    {
        if (SceneManager.GetActiveScene().name == Loader.Scene.Level_2.ToString())
        {
            bonus_num = 180;
        }
        else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_3.ToString())
        {
            bonus_num = 220;
        }
        else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_4.ToString())
        {
            bonus_num = 300;
        }
        else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_5.ToString())
        {
            bonus_num = 420;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Timer.GetComponent<Timer_Script>().timeValue;
        DisplayBonus(time);
    }

    void DisplayBonus(float timeToDisplay)
    {
        int time = (int) timeToDisplay;
        string level = SceneManager.GetActiveScene().name;
        if (time >= 0 && time < timeLimits[level] && time % 5 == 0 && !timeSet.Contains(time))
        {
            bonus_num -= 20;
            timeSet.Add(time);
        }
        display.text = bonus_num.ToString();
    }
}
