using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exit_Script : MonoBehaviour
{
    public GameObject Exit_UI; // UI to display when player arrive the exit
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end

    // public GameObject analyticsManager;
    // private AnalyticsManager analyticsManagerScript;
    public int kills;

    // Start is called before the first frame update
    private void Awake(){
        // analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
    //analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
    }
    
    void Start()
    {
        //analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        Exit_UI.SetActive(false); // Disable(Hide) the UI at the start of the game
        kills = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        kills++;
        Exit_UI.SetActive(true); // Enable the UI when detects the collision between player and exit
        // analyticsManagerScript.HandleEvent("kills", new List<object>
        //             {
        //                 kills
        //             });
        GameFinishText.text = "Level Passed!";
        Time.timeScale = 0; // Freeze the game (Set value to 1 to continue time flow)
    }
}
