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



    public void updateSpeed()
    {
        speedMultEnemy *= 1.06f;
        speedMultPlayer *= 1.03f;

        sliderScript.setEnemySpeed(speedMultEnemy);
        sliderScript.setPlayerSpeed(speedMultPlayer);
    }
}
