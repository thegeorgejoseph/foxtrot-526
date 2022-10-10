using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public GameObject scoreBoard;
    public GameObject highScoreTable;
    public TMP_Text hearts_remaining;
    public TMP_Text enemies_killed;
    public TMP_Text level_passed;
    public TMP_Text level_score;
    public TMP_Text total_score;
    public TMP_Text level_pass_msg;
    public static float level1_score;
    public GameObject MainMenuBtn;
    public GameObject NextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        float heart_count = HealthManager.health;
        float total_score_val = 0;
        float level_total_score = 0;
        hearts_remaining.text = heart_count.ToString() + " * 100 = " + heart_count * 100;
        enemies_killed.text = Exit_Script.enemies_count.ToString() + " * 100 = " + Exit_Script.enemies_count * 100;
        level_passed.text = "1 * 100 = " + 100;
        total_score_val = heart_count * 100 + Exit_Script.enemies_count * 100 + 100;
        level_score.text = total_score_val.ToString();

        level_total_score = total_score_val + Exit_Script.score_till_curr_level;
        total_score.text = (level_total_score).ToString();
        level1_score = level1_score + total_score_val;
        Exit_Script.score_till_curr_level = level_total_score;
        if(Exit_Script.level_num == 3)
        {
            NextLevelButton.SetActive(false);
            MainMenuBtn.SetActive(true);
        }
        else
        {
            MainMenuBtn.SetActive(false);
            NextLevelButton.SetActive(true);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        if(Exit_Script.level_num == 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level_2");
        }
        else if(Exit_Script.level_num == 2)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level_3");
        }
    }
}
