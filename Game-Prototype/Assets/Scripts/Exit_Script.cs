using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exit_Script : MonoBehaviour
{
    public GameObject Exit_UI; // UI to display when player arrive the exit
    public TextMeshProUGUI GameFinishText; // Text box to display the text when the game reaches to an end

    // Start is called before the first frame update
    void Start()
    {
        Exit_UI.SetActive(false); // Disable(Hide) the UI at the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Exit_UI.SetActive(true); // Enable the UI when detects the collision between player and exit
        GameFinishText.text = "Level Passed!";
        Time.timeScale = 0; // Freeze the game (Set value to 1 to continue time flow)
    }
}
