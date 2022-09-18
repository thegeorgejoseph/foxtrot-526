using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_Battle_Scripts : MonoBehaviour
{
    public GameObject battleUI; // Battle UI 
    public SliderScript sliderSC; // SliderScript object to call function
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end
    public GameObject GameOver_UI; // UI to display when player loses the battle


    private GameObject currentEnemy; // Temp var to record which enemy the player encountered

    // Start is called before the first frame update
    void Start()
    {
        // Set battle UI to be inactive in the beginning
        battleUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has finished the battle
        if (sliderSC.isFinished)
        {
            // Battle finished, set battle UI to inactive
            battleUI.SetActive(false);

            // Check if the player has won
            if (!sliderSC.checkBattleResult())
            {
                // The player lost, gameover!
                GameFinishText.text = "Game Over!";
                GameOver_UI.SetActive(true);
            }
            else
            {
                // The player has won
                currentEnemy.SetActive(false);
                // Enable player movement
                GetComponent<Movement2D>().enabled = true;
            }
        }
    }


    // Detect if the player has collided with enemy
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "Enemy")
        {
            // Record which enemy the player encountered
            currentEnemy = collider.gameObject;
            // Disable player movement
            GetComponent<Movement2D>().enabled = false;
            // Activate slider UI (battle scene)
            battleUI.SetActive(true);
            // Reset the slider for next battle
            sliderSC.Reset();
        }
    }
}
