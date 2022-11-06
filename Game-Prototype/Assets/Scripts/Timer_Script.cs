using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer_Script : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeValue;
    public Text timeText;
    public bool freeze = false;
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            timeValue += Time.deltaTime;
        }


        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        //if (timeToDisplay < 0)
        //{
        //    timeToDisplay = 0;
        //}

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}",minutes, seconds);
    }

}
