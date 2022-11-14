using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMovingSpeed : MonoBehaviour
{
    public SliderScript sliderScript;

    private float speedMultPlayer; // Speed multiplier for Player
    private float speedMultEnemy; // Speed multiplier for Enemy

    void Start()
    {
        speedMultPlayer = 1f;
        speedMultEnemy = 1f;
    }



    public void increaseSpeed()
    {
        speedMultEnemy = 1.15f;
        speedMultPlayer = 1.07f;

        sliderScript.setEnemySpeed(speedMultEnemy);
        sliderScript.setPlayerSpeed(speedMultPlayer);
    }

    public void decreaseSpeed()
    {
        speedMultEnemy = 0.90f;
        speedMultPlayer = 0.92f;

        sliderScript.setEnemySpeed(speedMultEnemy);
        sliderScript.setPlayerSpeed(speedMultPlayer);
    }

}
