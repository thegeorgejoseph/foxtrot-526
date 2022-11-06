using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TimerPower;
    public GameObject Timer;
    public static int pauseTime;
    public bool condition = false;

    void Start()
    {
        pauseTime = 10;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "TimerPowerup")
        {
            collider.gameObject.SetActive(false);
            StartCoroutine(CountDown(pauseTime));
        }

    }

    private IEnumerator CountDown(int duration)
    {
        Timer.GetComponent<Timer_Script>().freeze = true;
        condition = true;
        yield return new WaitForSeconds(duration);
        condition = false;
        Timer.GetComponent<Timer_Script>().freeze = false;
    }
}
