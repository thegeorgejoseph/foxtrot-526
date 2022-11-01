using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class Enemy_Battle_Scripts : MonoBehaviour
{
    public GameObject battleUI; // Battle UI 
    private SliderScript sliderSC; // SliderScript object to call function
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end
    public GameObject GameOver_UI; // UI to display when player loses the battle
    public GameObject SpaceText; // UI to display SpaceBar Hint while attack
    public GameObject player; // Player gameobject to display damage effect

    public Crystal crystalScript; // Crystal Script object
    public DynamicMovingSpeed DMS; // Script object
    public Heart_Animation HA; // Script object

    public ScreenShaking screenShake; // ScreenShaking object to display screen shaking effect
    public Enemy_Respawn respawn; // Enemy_Respawn object to respawn the enemy that player defeats

    public GameObject analyticsManager; // GameObj to initialize analytic manager
    private AnalyticsManager analyticsManagerScript; // Analytic manager object for metric event handler

    public bool did_finish; // to record analytics - did the player reach goal state
    public bool event_called; // bool to prevent calling analytics handler multiple times inside update()


    private GameObject currentEnemy; // Temp var to record which enemy the player encountered
    public GameObject deathScore;
    public TMP_Text total_score;

    private Color transColor; // Color for defeated player

    private bool battle_started; // Local bool to tell blocks inside Update() whether to check battle status

    public float health = 1.0f;
    public int count = 0;

    public int kills;
    public int enemies_encountered;

    public GameObject playerMovement;
    private Movement2D playerMovementScript;

    [SerializeField] private AudioSource gemSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private AudioSource shakeSoundEffect;
    private void Awake(){
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        playerMovementScript = playerMovement.GetComponent<Movement2D>();
        // teleportationScript = teleportation.GetComponent<Teleportation>();
   
    }
    // Start is called before the first frame update
    void Start()
    {
        sliderSC = battleUI.GetComponent<SliderScript>();
        // Set battle UI to be inactive in the beginning
        battleUI.SetActive(false);
        SpaceText.SetActive(false);
        did_finish = false;
        event_called = false;
        battle_started = false;
        HA.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        crystalScript.GetComponent<SpriteRenderer>().enabled = false;
        kills = 0;
        enemies_encountered = 0;
        transColor = player.GetComponent<SpriteRenderer>().color;
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
                    // Player Lost or no bullets
                    HealthManager.health--;
                    // Play heart animation
                    HA.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    HA.heartLose();
                    // Set tag to be Invisible so that player won't get into battle;
                    player.tag = "Invisible";
                    transColor.a = 0.2f;
                    // Play effects where player takes damage
                    player.GetComponent<SpriteRenderer>().color = Color.red;
                    StartCoroutine(CountDown(1));
                    

                    if (HealthManager.health <= 0)
                    {
                        // The player lost, gameover!
                        GameFinishText.text = "Game Over!";
                        //deathScore.SetActive(true);
                        deathSoundEffect.Play();
                        if (SceneManager.GetActiveScene().name != Loader.Scene.Level_0.ToString())
                        {
                            SceneManager.LoadScene("GameHighscore");
                        }
                        else
                        {
                            deathScore.SetActive(true);
                        }

                        // if(SceneManager.GetActiveScene().name == Loader.Scene.Level_2.ToString())
                        // {
                        //     total_score.text = Exit_Script.score_till_curr_level.ToString();
                        // }
                        // else if(SceneManager.GetActiveScene().name == Loader.Scene.Level_3.ToString())
                        // {
                        //     total_score.text = Exit_Script.score_till_curr_level.ToString();
                        // }
                        // else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_4.ToString())
                        // {
                        //     total_score.text = Exit_Script.score_till_curr_level.ToString();
                        // }
                        // else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_5.ToString())
                        // {
                        //     total_score.text = Exit_Script.score_till_curr_level.ToString();
                        // }


                        if (!event_called)
                        {
                        
                            
                            string level = SceneManager.GetActiveScene().name;

                          
                            analyticsManagerScript.timer.Stop();
                          

                            var metrics = new Metrics(analyticsManagerScript.clientID,
                            DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), 
                                            level, did_finish.ToString(), 
                                            enemies_encountered.ToString(), 
                                            kills.ToString(),
                                            HealthManager.health.ToString(),
                                            (analyticsManagerScript.timer.ElapsedTicks / 10000000).ToString(),
                                            playerMovementScript.portalUsageCount.ToString(),
                                            "0");

                            var testMetric = new testMetricStore(analyticsManagerScript.clientID, 
                            DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                            level,"1", "2", "3");
                            DatabaseHandler.PostMetrics<Metrics>(metrics, analyticsManagerScript.startTime, () =>
                            {
                                Debug.Log("done posting to firebase metric");
                            });

                            event_called = true;
                        }
                        Time.timeScale = 0f;
                    }
                    else
                    {
                        screenShake.TriggerShake();
                        shakeSoundEffect.Play();
                        GetComponent<Movement2D>().enabled = true;
                    }
                }
                else
                {
                    HealthManager.health += 0.5f;
                    // The player has won
                    /*  OLD currentEnemy.SetActive(false);   */
                    respawn.DisableEnemy(currentEnemy);
                    // Player will not get into battle for 3 secs.
                    player.tag = "Invisible";
                    StartCoroutine(Invisible(3));
                    // Gain one crystal
                    crystalScript.GetComponent<SpriteRenderer>().enabled = true;
                    crystalScript.gainCrystal(1);
                    // Play heart animation
                    HA.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    HA.heartGain();
                    // Enable player movement
                    kills += 1;
                    gemSoundEffect.Play();
                    DMS.updateSpeed();
                    GetComponent<Movement2D>().enabled = true;
                    
                }
            }
        }
    }


    // Detect if the player has collided with enemy
    void OnCollisionEnter2D(Collision2D collider)
    {
        // Using tags to check if the player has actually met enemy
        if (collider.gameObject.tag == "Enemy" && player.tag == "Player")
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
            // Disable some UIs
            HA.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            crystalScript.GetComponent<SpriteRenderer>().enabled = false;

            // Activate slider UI (battle scene)
            battleUI.SetActive(true);
            if (count == 0)
            {
                SpaceText.SetActive(true);
            }
            // Reset the slider for next battle
            sliderSC.Reset(currentEnemy, this.gameObject);
        }
    }

    // Coroutine for Player's damage effect
    private IEnumerator CountDown(int duration)
    {
        yield return new WaitForSeconds(duration);
        HA.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().color = transColor;
        StartCoroutine(Invisible(3));
    }

    // Coroutine for Player's invisible status after being defeated by enemy
    private IEnumerator Invisible(int duration)
    {
        yield return new WaitForSeconds(duration);
        player.tag = "Player";
        transColor.a = 1f;
        player.GetComponent<SpriteRenderer>().color = transColor;
    }

    public bool checkBattleStatus()
    {
        return battle_started;
    }
}
