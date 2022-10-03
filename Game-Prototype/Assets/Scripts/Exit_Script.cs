using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Exit_Script : MonoBehaviour
{
    public GameObject Exit_UI; // UI to display when player arrive the exit
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end

    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;
    public bool did_finish;
    public Enemy_Battle_Scripts battleInfoScript;
    public Bullet_System bulletSys; // Bullet_System Obejct to gain info about remaining bullet
    public GameObject battleInfo;
    public GameObject scoreBoard;
    public TMP_Text bul_remaining;
    public TMP_Text hearts_remaining;
    public TMP_Text enemies_killed;
    public TMP_Text level_passed;
    public TMP_Text level_score;
    public TMP_Text total_score;
    public static float level1_score;

    // Start is called before the first frame update
    private void Awake(){
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        battleInfoScript = battleInfo.GetComponent<Enemy_Battle_Scripts>();
    //analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == Loader.Scene.Level_1.ToString() ){
                Debug.Log("Bullets Remaining - "+bulletSys.getBulletNum());
                Debug.Log("Health Remaining - "+HealthManager.health);
                Debug.Log("Enemies killed - " + battleInfoScript.kills);
                scoreBoard.SetActive(true);
                float bul_count = bulletSys.getBulletNum();
                float heart_count = HealthManager.health;
                float enemies_count = battleInfoScript.kills;
                float total_score_val = 0;

                bul_remaining.text = bul_count.ToString()+ " * 100 = "+ bul_count * 100;
                hearts_remaining.text = heart_count.ToString() + " * 100 = "+ heart_count*100;
                enemies_killed.text = enemies_count.ToString() + " * 100 = " + enemies_count * 100;
                level_passed.text = "1 * 100 = " +100;
                total_score_val = bul_count * 100 + heart_count * 100 + enemies_count * 100 + 100;
                level_score.text = total_score_val.ToString();
                total_score.text = total_score_val.ToString();
                level1_score = total_score_val;
                Time.timeScale = 0;
                //Loader.Load(Loader.Scene.Level_2);

                string level = SceneManager.GetActiveScene().name;

                analyticsManagerScript.HandleEvent("master_metrics", new List<object>
                        {
                            level,
                            did_finish,
                            battleInfoScript.enemies_encountered,
                            battleInfoScript.kills,
                            HealthManager.health
                            
                        });

            } else if(SceneManager.GetActiveScene().name == Loader.Scene.Level_2.ToString()){
                Debug.Log("Bullets Remaining - " + bulletSys.getBulletNum());
                Debug.Log("Health Remaining - " + HealthManager.health);
                Debug.Log("Enemies killed - " + battleInfoScript.kills);

                scoreBoard.SetActive(true);
                float bul_count = bulletSys.getBulletNum();
                float heart_count = HealthManager.health;
                float enemies_count = battleInfoScript.kills;
                float total_score_val = 0;

                bul_remaining.text = bul_count.ToString() + " * 100 = " + bul_count * 100;
                hearts_remaining.text = heart_count.ToString() + " * 100 = " + heart_count * 100;
                enemies_killed.text = enemies_count.ToString() + " * 100 = " + enemies_count * 100;
                level_passed.text = "1 * 100 = " + 100;
                total_score_val = bul_count * 100 + heart_count * 100 + enemies_count * 100 + 100;
                level_score.text = total_score_val.ToString();
                total_score.text = (total_score_val+level1_score).ToString();
                level1_score = total_score_val;
                Time.timeScale = 0;
                did_finish = true;

                string level = SceneManager.GetActiveScene().name;

                analyticsManagerScript.HandleEvent("master_metrics", new List<object>
                        {
                            level,
                            did_finish,
                            battleInfoScript.enemies_encountered,
                            battleInfoScript.kills,
                            HealthManager.health
                            
                        });

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
                // analyticsManagerScript.HandleEvent("did_finish", new List<object>
                //         {
                //             did_finish,
                //             name
                //         });

                // analyticsManagerScript.HandleEvent("enemies", new List<object>
                // {
                //     battleInfoScript.enemies_encountered,
                //     battleInfoScript.kills
                   
                // });
                // analyticsManagerScript.HandleEvent("health_metric", new List<object>
                // {
                //     HealthManager.health
                // });
                GameFinishText.text = "Level Passed!";

                Debug.Log("enemies_encountered: "+ battleInfoScript.enemies_encountered);
                Debug.Log("health: "+HealthManager.health);
                Time.timeScale = 0; // Freeze the game (Set value to 1 to continue time flow)
            }
            
        }
    }

    

}
