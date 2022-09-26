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
    public GameObject SpaceText; // UI to display SpaceBar Hint while attack

    public GameObject analyticsManager; // GameObj to initialize analytic manager
    private AnalyticsManager analyticsManagerScript; // Analytic manager object for metric event handler

  

    public bool did_finish; // to record analytics - did the player reach goal state
    public bool event_called; // bool to prevent calling analytics handler multiple times inside update()


    private GameObject currentEnemy; // Temp var to record which enemy the player encountered
    private bool battle_started; // Local bool to tell blocks inside Update() whether to check battle status

    public float health = 1.0f;
    public int count = 0;

    public int kills;
    public int enemies_encountered;

     private void Awake(){
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        

    
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set battle UI to be inactive in the beginning
        battleUI.SetActive(false);
        did_finish = false;
        event_called = false;
        battle_started = false;
        kills = 0;
        enemies_encountered = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the battle is activated
        if (battle_started)
        {
            count += 1;
            // Check if the player has finished the battle
            if (sliderSC.isFinished)
            {
                // Battle finished, set battle UI to inactive
                battleUI.SetActive(false);
                SpaceText.SetActive(false);
                // Set battle status to not started
                battle_started = false;

                // Check if the player has won
                if (!sliderSC.checkBattleResult())
                {
                    HealthManager.health--;
                    if (HealthManager.health <= 0)
                    {
                        // The player lost, gameover!
                        GameFinishText.text = "Game Over!";
                        GameOver_UI.SetActive(true);
                        if (!event_called)
                        {

                            analyticsManagerScript.HandleEvent("did_finish", new List<object>
                            {
                                did_finish
                            }); // send false to did_finish metric
                            analyticsManagerScript.HandleEvent("enemies", new List<object>
                            {
                                enemies_encountered,
                                kills
                            });
                            analyticsManagerScript.HandleEvent("health_metric", new List<object>
                            {
                                HealthManager.health
                            });
                            event_called = true;
                        }
                    }
                    else
                    {
                        GetComponent<Movement2D>().enabled = true;
                    }
                }
                else
                {
                    HealthManager.health += 0.5f;
                    // The player has won
                    currentEnemy.SetActive(false);
                    // Enable player movement
                    kills += 1;
                    GetComponent<Movement2D>().enabled = true;
                }
            }
        }
    }


    // Detect if the player has collided with enemy
    void OnCollisionEnter2D(Collision2D collider)
    {
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "Enemy")
        {
            // Record which enemy the player encountered
            enemies_encountered += 1;
            Debug.Log("enemy encountered: "+enemies_encountered);
            currentEnemy = collider.gameObject;
            // Stop movements
            currentEnemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            currentEnemy.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().angularVelocity = 0f;
            // Set battle status to activated
            battle_started = true;
            // Disable player movement
            GetComponent<Movement2D>().enabled = false;
            // Activate slider UI (battle scene)
            battleUI.SetActive(true);
            if(count == 0)
            {
                SpaceText.SetActive(true);

            }
            // Reset the slider for next battle
            sliderSC.Reset();
        }
    }


    public bool checkBattleStatus()
    {
        return battle_started;
    }
}
