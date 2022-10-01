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
                //Loader.Load(Loader.Scene.Level_2);

            } else if(SceneManager.GetActiveScene().name == Loader.Scene.Level_2.ToString()){
                Debug.Log("Bullets Remaining - " + bulletSys.getBulletNum());
                Debug.Log("Health Remaining - " + HealthManager.health);
                Debug.Log("Enemies killed - " + battleInfoScript.kills);
                did_finish = true;
                Exit_UI.SetActive(true); // Enable the UI when detects the collision between player and exit
                analyticsManagerScript.HandleEvent("did_finish", new List<object>
                        {
                            did_finish
                        });

                analyticsManagerScript.HandleEvent("enemies", new List<object>
                {
                    battleInfoScript.enemies_encountered,
                    battleInfoScript.kills
                });
                analyticsManagerScript.HandleEvent("health_metric", new List<object>
                {
                    HealthManager.health
                });
                GameFinishText.text = "Level Passed!";

                Debug.Log("enemies_encountered: "+ battleInfoScript.enemies_encountered);
                Debug.Log("health: "+HealthManager.health);
                Time.timeScale = 0; // Freeze the game (Set value to 1 to continue time flow)
            }
            
        }
    }

    public void loadLevel2()
    {
        Loader.Load(Loader.Scene.Level_2);
    }

}
