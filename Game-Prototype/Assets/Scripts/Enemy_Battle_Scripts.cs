using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Enemy_Battle_Scripts : MonoBehaviour
{
    public GameObject battleUI; // Battle UI 
    public SliderScript sliderSC; // SliderScript object to call function
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end
    public GameObject GameOver_UI; // UI to display when player loses the battle
    public GameObject SpaceText; // UI to display SpaceBar Hint while attack
    public GameObject player; // Player gameobject to display damage effect

    public Bullet_System bulletSys; // Bullet_System Obejct to gain info about remaining bullet
    public ScreenShaking screenShake; // ScreenShaking object to display screen shaking effect
    public Enemy_Respawn respawn; // Enemy_Respawn object to respawn the enemy that player defeats

    public GameObject analyticsManager; // GameObj to initialize analytic manager
    private AnalyticsManager analyticsManagerScript; // Analytic manager object for metric event handler

    private bool noBullets;

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
        SpaceText.SetActive(false);
        did_finish = false;
        event_called = false;
        battle_started = false;
        noBullets = false;
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
            if (sliderSC.isFinished || noBullets)
            {
                // Battle finished, set battle UI to inactive
                battleUI.SetActive(false);
                SpaceText.SetActive(false);
                // Set battle status to not started
                battle_started = false;

                if (!noBullets)
                {
                    // Update # of remaining bullets
                    bulletSys.setCurBulletNum(1);
                }
                
                // Check if the player has won 
                if (!sliderSC.checkBattleResult() || noBullets)
                {
                    // Player Lost or no bullets
                    HealthManager.health--;

                    // Play effects where player takes damage
                    player.GetComponent<SpriteRenderer>().color = Color.red;
                    StartCoroutine(CountDown(1));

                    if (HealthManager.health <= 0)
                    {
                        // The player lost, gameover!
                        GameFinishText.text = "Game Over!";
                        GameOver_UI.SetActive(true);
                        if (!event_called)
                        {
                        
                            
                            string level = SceneManager.GetActiveScene().name;

                            analyticsManagerScript.HandleEvent("master_metrics", new List<object>
                                    {
                                        level,
                                        did_finish,
                                        enemies_encountered,
                                        kills,
                                        HealthManager.health
                                        
                                    });
                            
                            // analyticsManagerScript.HandleEvent("did_finish", new List<object>
                            // {
                            //     did_finish,
                            //     name
                            // }); // send false to did_finish metric
                            // analyticsManagerScript.HandleEvent("enemies", new List<object>
                            // {
                            //     enemies_encountered,
                            //     kills
                            // });
                            // analyticsManagerScript.HandleEvent("health_metric", new List<object>
                            // {
                            //     HealthManager.health
                            // });
                            event_called = true;
                        }
                        Time.timeScale = 0f;
                    }
                    else
                    {
                        screenShake.TriggerShake();
                        GetComponent<Movement2D>().enabled = true;
                    }
                }
                else
                {
                    HealthManager.health += 0.5f;
                    // The player has won
                    /*  OLD currentEnemy.SetActive(false);   */
                    respawn.DisableEnemy(currentEnemy);
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

            // Check remaining bullets
            if (bulletSys.getBulletNum() == 0)
            {
                // In this case player has no remaining bullets, lost one heart
                noBullets = true;
            }
            else
            {
                // Activate slider UI (battle scene)
                battleUI.SetActive(true);
                if (count == 0)
                {
                    SpaceText.SetActive(true);
                }
                // Reset the slider for next battle
                sliderSC.Reset();
            } 
        }
    }

    // Coroutine for Player's damage effect
    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        player.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public bool checkBattleStatus()
    {
        return battle_started;
    }
}
