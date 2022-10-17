using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Time_Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    public int bonus_num = 140;
    public TextMeshProUGUI display;
    public GameObject Timer;
    private HashSet<int> timeSet = new HashSet<int>();
    void Start()
    {

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
        if (time >= 0 && time < 35 && time % 5 == 0 && !timeSet.Contains(time))
        {
            bonus_num -= 20;
            timeSet.Add(time);
        }
        display.text = bonus_num.ToString();
    }
}
