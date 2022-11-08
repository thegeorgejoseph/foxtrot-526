using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup_Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TimerIcon;
    public GameObject Timer;
    public static int pauseTime;
    public bool condition = false;

    // dropping status
    private bool droppingEnabled;

    void Start()
    {
        pauseTime = 10;
        droppingEnabled = true; // enabled by default
    }

    // public getter for enemy_battle_script
    public bool getDroppingStatus()
    {
        return droppingEnabled;
    }

    // function to change dropping status
    public void changeDroppingStatus(bool newStatus)
    {
        droppingEnabled = newStatus;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "TimerPowerup")
        {
            collider.gameObject.SetActive(false);
            Timer.GetComponent<Timer_Script>().freeze = true;
            TimerIcon.GetComponent<Image>().color = Color.white;
            condition = true;
            changeDroppingStatus(false);
            StartCoroutine(CountDown(pauseTime));
        }

    }

    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        condition = false;
        Timer.GetComponent<Timer_Script>().freeze = false; 
        TimerIcon.GetComponent<Image>().color = Color.grey;
        changeDroppingStatus(true);
    }
}
