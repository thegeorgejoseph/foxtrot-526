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
    public static int bonus_num;
    public GameObject MainMenuBtn;
    public GameObject time_bonus;
    
    // public String username;


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
            bonus_num = time_bonus.GetComponent<Time_Bonus>().bonus_num;
            // username = playerProfileScript.Inputname + "_" + analyticsManagerScript.clientID;
            // Debug.Log("Username is printed " + username);

            // DatabaseHandler.GetMetrics<Metrics>(users =>
            // {
            //     foreach (var user in users)
            //     {
            //         Debug.Log($"{user.Value.clientID} {user.Value.level} {user.Value.timestamp}");
            //     }
            // });

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
                float total_score_val = 0;

                SceneManager.LoadScene("ScoreScene");
                hearts_remaining.text = heart_count.ToString() + " * 100 = " + heart_count * 100;
                enemies_killed.text = enemies_count.ToString() + " * 100 = " + enemies_count * 100;
                level_passed.text = "1 * 100 = " + 100;
                total_score_val = heart_count * 100 + enemies_count * 100 + 100;
                level_score.text = total_score_val.ToString();
                total_score.text = total_score_val.ToString();
                level1_score = total_score_val;
                Time.timeScale = 0;
                level_score_metric = level1_score;

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
                
                //if(level1_score > 0){
                    
                //    DatabaseHandler.GetHighScore<HighScores>(users =>
                //        {
                //            Debug.Log("Score " + level1_score);
                //            var myList = new List<KeyValuePair<string, float>>();
                //            var returnList = new TableRow[6];
                            
                //            foreach (KeyValuePair<string, HighScores> kvp in users)
                //            {
                //                myList.Add(new KeyValuePair<string, float>(kvp.Key, kvp.Value.levelScore)); 
                //            }
                //            Debug.Log("finding the data " + myList.Count);
                //            int overallCount = myList.Count;
                //            int found = -1;
                //            for(int i = 0;i<overallCount;i++){
                                
                //                if(myList[i].Key == username){
                //                    found = i;
                //                    break;
                //                }
                //            }
                //            if(found == -1){
                //                myList.Add(new KeyValuePair<string, float>(username, level1_score));
                //            }
                //            else{
                //                if((myList[found].Value < level1_score)){
                //                    myList.Remove(myList[found]);
                //                    myList.Add(new KeyValuePair<string, float>(username, level1_score));
                //                    found = -2;
                //                }
                //            }
                //            int count = 0;
                //            int index = 0;
                //            int searchRank = 0;
                //            myList.Sort(Compare);
                //            foreach(KeyValuePair<string, float> kvp in myList){
                //                Debug.Log("Key " + kvp.Key);
                //                count += 1;
                //                if(kvp.Key == username){
                //                    searchRank = count;
                //                }
                //                if(count < 4){
                //                    returnList[index] = new TableRow(count, kvp.Key,kvp.Value);
                //                    index += 1;
                //                }    
                //            }
                //            int counter = 0;
                //            if(searchRank == 1 || searchRank == 2 ){
                //                Array.Resize(ref returnList, 3);
                //            }
                //            else if(searchRank == count){
                //                Array.Resize(ref returnList, 5);
                //                foreach(KeyValuePair<string, float> kvp in myList){
                //                    counter += 1;
                //                    if(counter == searchRank || counter == searchRank - 1){
                //                        returnList[index] = new TableRow(counter, kvp.Key,kvp.Value);
                //                        index += 1;
                //                    }    
                //                }
                //            }
                //            else if(searchRank == 3){
                //                Array.Resize(ref returnList, 4);
                //                foreach(KeyValuePair<string, float> kvp in myList){
                //                    counter += 1;
                //                    if(counter == searchRank + 1){
                //                        returnList[index] = new TableRow(counter, kvp.Key,kvp.Value);
                //                        index += 1;
                //                    }    
                //                }
                //            }
                //            else if(searchRank == 4){
                //                Array.Resize(ref returnList, 5);
                //                foreach(KeyValuePair<string, float> kvp in myList){
                //                    counter += 1;
                //                    if(counter == searchRank || counter == searchRank + 1){
                //                        returnList[index] = new TableRow(counter, kvp.Key,kvp.Value);
                //                        index += 1;
                //                    }    
                //                }
                //            }
                //            else{
                //                foreach(KeyValuePair<string, float> kvp in myList){
                //                    counter += 1;
                //                    if(counter == searchRank || counter == searchRank - 1 || counter == searchRank + 1){
                //                        returnList[index] = new TableRow(counter, kvp.Key,kvp.Value);
                //                        index += 1;
                //                    }    
                //                }
                //            }
                //            for(int i = 0;i<returnList.Length;i++){
                //                Debug.Log("DDDDD "+ returnList[i].username +" " + returnList[i].levelScore);
                //            }
                //            if (found < 0){
                //                DatabaseHandler.PostHighScore<HighScores>(playerHighscore, level, username , () =>
                //                {
                //                    Debug.Log("done pushing the data" + username );
                //                });
                //            }
                            
                //        });    
                //}
                //else{
                //    DatabaseHandler.GetHighScore<HighScores>(users =>
                //        {
                //            Debug.Log("failed state check");
                //            var myList = new List<KeyValuePair<string, float>>();
                //            var returnList = new TableRow[3];
                //            foreach (KeyValuePair<string, HighScores> kvp in users)
                //            {
                //                myList.Add(new KeyValuePair<string, float>(kvp.Key, kvp.Value.levelScore)); 
                //            }
                //            myList.Sort(Compare);
                //            int count = 0;
                //            foreach(KeyValuePair<string, float> kvp in myList){
                //                if(count < 3){
                //                    returnList[count] = new TableRow(count+1, kvp.Key,kvp.Value);
                //                    count += 1;
                //                }    
                //                else{
                //                    break;
                //                }
                //            }
                //            for(int i = 0;i<returnList.Length;i++){
                //                Debug.Log("CCCC "+ returnList[i].username +" " + returnList[i].levelScore);
                //            }
                //         });
                //}
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
                float total_score_val = 0;
                level_num = 2;
                SceneManager.LoadScene("ScoreScene");
                hearts_remaining.text = heart_count.ToString() + " * 100 = " + heart_count * 100;
                enemies_killed.text = enemies_count.ToString() + " * 100 = " + enemies_count * 100;
                level_passed.text = "1 * 100 = " + 100;
                total_score_val = heart_count * 100 + enemies_count * 100 + 100;
                level_score.text = total_score_val.ToString();
                total_score.text = (total_score_val + level1_score).ToString();
                level1_score = total_score_val;
                Time.timeScale = 0;
                level_score_metric = level1_score;


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
                DatabaseHandler.PostMetrics<testMetricStore>(testMetric, analyticsManagerScript.startTime, () =>
                {
                    Debug.Log("done posting to firebase test metric");
                }, "testMetric");
                // get method test



                // analyticsManagerScript.HandleEvent("master_metrics", new List<object>
                //         {
                //             level,
                //             did_finish,
                //             battleInfoScript.enemies_encountered,
                //             battleInfoScript.kills,
                //             HealthManager.health

                //         });

                // string name = "not";
                // foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
                // {
                //     if (S.enabled)
                //     {
                //         name = S.path.Substring(S.path.LastIndexOf('/')+1);
                //         name = name.Substring(0,name.Length-6);
                //     }
                // }

                //Exit_UI.SetActive(true); // Enable the UI when detects the collision between player and exit
                GameFinishText.text = "Level Passed!";

                Debug.Log("enemies_encountered: " + battleInfoScript.enemies_encountered);
                Debug.Log("health: " + HealthManager.health);
                Time.timeScale = 0; // Freeze the game (Set value to 1 to continue time flow)
            }

            else if (SceneManager.GetActiveScene().name == Loader.Scene.Level_3.ToString())
            {
                enemies_count = battleInfoScript.kills;
                level_num = 3;
                string level = level_num.ToString();
                SceneManager.LoadScene("ScoreScene");
                

                did_finish = true;
                analyticsManagerScript.timer.Stop();
                // Debug.Log("timer " + analyticsManagerScript.timer.Elapsed);
                Debug.Log("timer " + analyticsManagerScript.timer.ElapsedTicks / 10000000);
                level_score_metric = HealthManager.health * 100 + enemies_count * 100 + 100;

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
                level_num = 4;
                SceneManager.LoadScene("ScoreScene");

                string level = level_num.ToString();
                did_finish = true;
                analyticsManagerScript.timer.Stop();
                level_score_metric = HealthManager.health * 100 + enemies_count * 100 + 100;
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
                level_num = 5;
                SceneManager.LoadScene("ScoreScene");

                string level = level_num.ToString();
                level_score_metric = HealthManager.health * 100 + enemies_count * 100 + 100;
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
