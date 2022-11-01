using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Exit_Script : MonoBehaviour
{
    public GameObject Exit_UI; // UI to display when player arrive the exit
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end

    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;
    
    public GameObject playerMovement;
    private Movement2D playerMovementScript;

    // public GameObject playerProfile;
    // private InputNameScript playerProfileScript;

    public bool did_finish;
    public static float enemies_count;
    public static float crystal_count;
    public Enemy_Battle_Scripts battleInfoScript;
    public GameObject battleInfo;
    public GameObject scoreBoard;
    public GameObject highScoreTable;
    public TMP_Text hearts_remaining;
    public TMP_Text enemies_killed;
    public TMP_Text level_passed;
    public TMP_Text level_score;
    public TMP_Text total_score;
    public TMP_Text level_pass_msg;
    public static float level1_score;
    public static float score_till_curr_level;
    public float level_score_metric;
    public static float level_num;
    public GameObject MainMenuBtn;
    //public GameObject time_bonus;
    public GameObject Timer;
    public static int bonus_num;

    public Crystal crystalScript; // Crystal Script object

    // public String username;
    private Dictionary<string, int> timeLimits = new Dictionary<string, int>
        {
            { "Level_0", 1000},
            { "Level_1", 2000 },
            { "Level_2", 3000 },
            { "Level_3", 4000 },
            { "Level_4", 5000 },
            { "Level_5", 6000 }
        };

    [SerializeField] private AudioSource rocketSoundEffect;

    // Start is called before the first frame update
    private void Awake(){
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        battleInfoScript = battleInfo.GetComponent<Enemy_Battle_Scripts>();
        playerMovementScript = playerMovement.GetComponent<Movement2D>();
        // playerProfileScript = playerMovement.GetComponent<InputNameScript>();
    }
    
    void Start()
    {
        //analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        Exit_UI.SetActive(false); // Disable(Hide) the UI at the start of the game
        did_finish = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static int Compare(KeyValuePair<string, float> a, KeyValuePair<string, float> b)
    {
        return b.Value.CompareTo(a.Value);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            float time = Timer.GetComponent<Timer_Script>().timeValue;
            rocketSoundEffect.Play();
            string levels = SceneManager.GetActiveScene().name;
            bonus_num =(int)(timeLimits[levels] / time);

            if (SceneManager.GetActiveScene().name == Loader.Scene.Level_0.ToString())
            {
                scoreBoard.SetActive(true);
                Time.timeScale = 0;
                //Loader.Load(Loader.Scene.Level_1);
            }
            else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_1.ToString())
            {
                Debug.Log("Health Remaining - " + HealthManager.health);
                Debug.Log("Enemies killed - " + battleInfoScript.kills);
                level_num = 1;
                scoreBoard.SetActive(true);
                float heart_count = HealthManager.health;
                enemies_count = battleInfoScript.kills;
                crystal_count = crystalScript.getCrystalNum();
                float total_score_val = 0;

                SceneManager.LoadScene("ScoreScene");
                hearts_remaining.text = heart_count.ToString() + " * 100 = " + heart_count * 100;
                // enemies_killed.text = enemies_count.ToString() + " * 100 = " + enemies_count * 100;
                enemies_killed.text = crystal_count.ToString() + " * 100 = " + crystal_count * 100;
                level_passed.text = "1 * 100 = " + 100;
                // total_score_val = heart_count * 100 + enemies_count * 100 + 100;
                total_score_val = heart_count * 100 + crystal_count * 100 + 100;
                level_score.text = total_score_val.ToString();
                total_score.text = total_score_val.ToString();
                level1_score = total_score_val;
                Time.timeScale = 0;
                level_score_metric = level1_score + bonus_num;

                string level = level_num.ToString();
                did_finish = true;
                analyticsManagerScript.timer.Stop();
                
                // Debug.Log("timer " + analyticsManagerScript.timer.Elapsed);
                Debug.Log("timer " + analyticsManagerScript.timer.ElapsedTicks / 10000000);

                var playerHighscore = new HighScores(total_score_val);
                Debug.Log("Data " + playerHighscore.levelScore);


                var metrics = new Metrics(analyticsManagerScript.clientID,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                                level, did_finish.ToString(),
                                                battleInfoScript.enemies_encountered.ToString(),
                                                battleInfoScript.kills.ToString(),
                                                HealthManager.health.ToString(),
                                                (analyticsManagerScript.timer.ElapsedTicks / 10000000).ToString(),
                                                playerMovementScript.portalUsageCount.ToString(),
                                                level_score_metric.ToString());

                
                DatabaseHandler.PostMetrics<Metrics>(metrics, analyticsManagerScript.startTime, () =>
                    {
                        Debug.Log("done posting to firebase metric"+level);
                    });

                string username ="Michael";
                
            }
            else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_2.ToString())
            {
                Debug.Log("Health Remaining - " + HealthManager.health);
                Debug.Log("Enemies killed - " + battleInfoScript.kills);

                //scoreBoard.SetActive(true);
                //highScoreTable.SetActive(true);
                //level_pass_msg.gameObject.SetActive(true);
                //MainMenuBtn.SetActive(true);
                float heart_count = HealthManager.health;
                enemies_count = battleInfoScript.kills;
                crystal_count = crystalScript.getCrystalNum();
                float total_score_val = 0;
                level_num = 2;
                SceneManager.LoadScene("ScoreScene");
                hearts_remaining.text = heart_count.ToString() + " * 100 = " + heart_count * 100;
                // enemies_killed.text = enemies_count.ToString() + " * 100 = " + enemies_count * 100;
                enemies_killed.text = crystal_count.ToString() + " * 100 = " + crystal_count * 100;
                // enemies_killed.text = currentCrystal.ToString() + " * 100 = " + currentCrystal * 100;
                level_passed.text = "1 * 100 = " + 100;
                // total_score_val = heart_count * 100 + enemies_count * 100 + 100;
                total_score_val = heart_count * 100 + crystal_count * 100 + 100;
                level_score.text = total_score_val.ToString();
                total_score.text = (total_score_val + level1_score).ToString();
                level1_score = total_score_val;
                Time.timeScale = 0;
                level_score_metric = level1_score + bonus_num;


                string level = level_num.ToString();
                did_finish = true;
                analyticsManagerScript.timer.Stop();
                // Debug.Log("timer " + analyticsManagerScript.timer.Elapsed);
                Debug.Log("timer " + analyticsManagerScript.timer.ElapsedTicks / 10000000);


                var metrics = new Metrics(analyticsManagerScript.clientID,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                                level, did_finish.ToString(),
                                                battleInfoScript.enemies_encountered.ToString(),
                                                battleInfoScript.kills.ToString(),
                                                HealthManager.health.ToString(),
                                                (analyticsManagerScript.timer.ElapsedTicks / 10000000).ToString(),
                                                playerMovementScript.portalUsageCount.ToString(),
                                                level_score_metric.ToString());

                // teleportationScript.portalUsageCount.ToString()

                var testMetric = new testMetricStore(analyticsManagerScript.clientID,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                            level, "1", "2", "3");

                DatabaseHandler.PostMetrics<Metrics>(metrics, analyticsManagerScript.startTime, () =>
                    {
                        Debug.Log("done posting to firebase metric");
                    });
                // DatabaseHandler.PostMetrics<testMetricStore>(testMetric, analyticsManagerScript.startTime, () =>
                // {
                //     Debug.Log("done posting to firebase test metric");
                // }, "testMetric");
                //Exit_UI.SetActive(true); // Enable the UI when detects the collision between player and exit
                GameFinishText.text = "Level Passed!";

                Debug.Log("enemies_encountered: " + battleInfoScript.enemies_encountered);
                Debug.Log("health: " + HealthManager.health);
                Time.timeScale = 0; // Freeze the game (Set value to 1 to continue time flow)
            }

            else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_3.ToString())
            {
                enemies_count = battleInfoScript.kills;
                crystal_count = crystalScript.getCrystalNum();
                level_num = 3;
                string level = level_num.ToString();
                SceneManager.LoadScene("ScoreScene");
                

                did_finish = true;
                analyticsManagerScript.timer.Stop();
                // Debug.Log("timer " + analyticsManagerScript.timer.Elapsed);
                Debug.Log("timer " + analyticsManagerScript.timer.ElapsedTicks / 10000000);
                // level_score_metric = HealthManager.health * 100 + enemies_count * 100 + 100  + bonus_num;
                level_score_metric = HealthManager.health * 100 + crystal_count * 100 + 100  + bonus_num;

                var metrics = new Metrics(analyticsManagerScript.clientID,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                                level, did_finish.ToString(),
                                                battleInfoScript.enemies_encountered.ToString(),
                                                battleInfoScript.kills.ToString(),
                                                HealthManager.health.ToString(),
                                                (analyticsManagerScript.timer.ElapsedTicks / 10000000).ToString(),
                                                playerMovementScript.portalUsageCount.ToString(),
                                                level_score_metric.ToString());

                
                DatabaseHandler.PostMetrics<Metrics>(metrics, analyticsManagerScript.startTime, () =>
                    {
                        Debug.Log("done posting to firebase metric"+level);
                    });

            }
            else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_4.ToString())
            {
                enemies_count = battleInfoScript.kills;
                crystal_count = crystalScript.getCrystalNum();
                level_num = 4;
                SceneManager.LoadScene("ScoreScene");

                string level = level_num.ToString();
                did_finish = true;
                analyticsManagerScript.timer.Stop();
                // level_score_metric = HealthManager.health * 100 + enemies_count * 100 + 100 + bonus_num;
                level_score_metric = HealthManager.health * 100 + crystal_count * 100 + 100 + bonus_num;
                // Debug.Log("timer " + analyticsManagerScript.timer.Elapsed);
                Debug.Log("timer " + analyticsManagerScript.timer.ElapsedTicks / 10000000);

                var metrics = new Metrics(analyticsManagerScript.clientID,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                                level, did_finish.ToString(),
                                                battleInfoScript.enemies_encountered.ToString(),
                                                battleInfoScript.kills.ToString(),
                                                HealthManager.health.ToString(),
                                                (analyticsManagerScript.timer.ElapsedTicks / 10000000).ToString(),
                                                playerMovementScript.portalUsageCount.ToString(),
                                                level_score_metric.ToString());

                
                DatabaseHandler.PostMetrics<Metrics>(metrics, analyticsManagerScript.startTime, () =>
                    {
                        Debug.Log("done posting to firebase metric"+level);
                    });

            }
            else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_5.ToString())
            {
                enemies_count = battleInfoScript.kills;
                crystal_count = crystalScript.getCrystalNum();
                level_num = 5;
                SceneManager.LoadScene("ScoreScene");
                // SceneManager.LoadScene("GameHighscore");

                string level = level_num.ToString();
                // level_score_metric = HealthManager.health * 100 + enemies_count * 100 + 100 + bonus_num;
                level_score_metric = HealthManager.health * 100 + crystal_count * 100 + 100 + bonus_num;
                did_finish = true;
                analyticsManagerScript.timer.Stop();
                // Debug.Log("timer " + analyticsManagerScript.timer.Elapsed);
                Debug.Log("timer " + analyticsManagerScript.timer.ElapsedTicks / 10000000);

                var metrics = new Metrics(analyticsManagerScript.clientID,
                    DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                                                level, did_finish.ToString(),
                                                battleInfoScript.enemies_encountered.ToString(),
                                                battleInfoScript.kills.ToString(),
                                                HealthManager.health.ToString(),
                                                (analyticsManagerScript.timer.ElapsedTicks / 10000000).ToString(),
                                                playerMovementScript.portalUsageCount.ToString(),
                                                level_score_metric.ToString());

                
                DatabaseHandler.PostMetrics<Metrics>(metrics, analyticsManagerScript.startTime, () =>
                    {
                        Debug.Log("done posting to firebase metric"+level);
                    });

            }
        }
    }

}
