using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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
    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;

    public TMP_Text pos1;
    public TMP_Text pos2;
    public TMP_Text pos3;
    public TMP_Text pos4;
    public TMP_Text pos5;
    public TMP_Text dot1;
    public TMP_Text dot2;
    public TMP_Text dot3;
    public TMP_Text posn1;
    public TMP_Text posn2;
    public TMP_Text posn3;

    public TMP_Text score1;
    public TMP_Text score2;
    public TMP_Text score3;
    public TMP_Text score4;
    public TMP_Text score5;
    public TMP_Text scoren1;
    public TMP_Text scoren2;
    public TMP_Text scoren3;

    public TMP_Text name1;
    public TMP_Text name2;
    public TMP_Text name3;
    public TMP_Text name4;
    public TMP_Text name5;
    public TMP_Text namen1;
    public TMP_Text namen2;
    public TMP_Text namen3;

    public static int totalLevels = 3;

    static int Compare(KeyValuePair<string, float> a, KeyValuePair<string, float> b)
    {
        return b.Value.CompareTo(a.Value);
    }

    private void Awake()
    {
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
    }

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

        // new code

        var playerHighscore = new HighScores(total_score_val);
        Debug.Log("Data " + playerHighscore.levelScore);
        var username = InputNameScript.username + "_" + analyticsManagerScript.clientID;

        if (total_score_val > 0)
        {
            
            DatabaseHandler.GetAggregate<Aggregate>("Level_"+Exit_Script.level_num.ToString(), (data) =>{
                data.levelCompletionTime = ((data.levelCompletionTime * data.playersPassed) + Exit_Script.totalSeconds) / (data.playersPassed + 1);
                if (Exit_Script.did_finish){
                    data.playersPassed += 1;
                }
                data.healthAccquired += Exit_Script.healthValue;
                data.enemiesEncountered += Exit_Script.enemiesEncountered;
                data.enemiesKilled += Exit_Script.enemies_count;
                data.portalUsageCount += Exit_Script.portalUsageCount;
                data.playersPlayed += 1;
                DatabaseHandler.PostAggregate<Aggregate>(data, "Level_"+Exit_Script.level_num.ToString() , ()=>{
                    Debug.Log("Done with Level");
                });    
            });
            
            
           

            DatabaseHandler.GetHighScore<HighScores>("Level_"+Exit_Script.level_num.ToString(), (users) =>
            {
                Debug.Log("Score " + total_score_val);
                var myList = new List<KeyValuePair<string, float>>();
                var returnList = new TableRow[6];

                foreach (KeyValuePair<string, HighScores> kvp in users)
                {
                    myList.Add(new KeyValuePair<string, float>(kvp.Key, kvp.Value.levelScore));
                }
                Debug.Log("finding the data " + myList.Count);
                int overallCount = myList.Count;
                int found = -1;
                for (int i = 0; i < overallCount; i++)
                {

                    if (myList[i].Key == username)
                    {
                        found = i;
                        break;
                    }
                }
                if (found == -1)
                {
                    myList.Add(new KeyValuePair<string, float>(username, total_score_val));
                }
                else
                {
                    if ((myList[found].Value < total_score_val))
                    {
                        myList.Remove(myList[found]);
                        myList.Add(new KeyValuePair<string, float>(username, total_score_val));
                        found = -2;
                    }
                }
                int count = 0;
                int index = 0;
                int searchRank = 0;
                myList.Sort(Compare);
                foreach (KeyValuePair<string, float> kvp in myList)
                {
                    Debug.Log("Key " + kvp.Key);
                    count += 1;
                    if (kvp.Key == username)
                    {
                        searchRank = count;
                    }
                    if (count < 4)
                    {
                        returnList[index] = new TableRow(count, kvp.Key, kvp.Value);
                        index += 1;
                    }
                }
                Debug.Log("Current position to be inserted " + index);
                int counter = 0;
                if (searchRank == 1 || searchRank == 2)
                {
                    Array.Resize(ref returnList, 3);
                }
                else if (searchRank == count)
                {
                    Array.Resize(ref returnList, 5);
                    foreach (KeyValuePair<string, float> kvp in myList)
                    {
                        counter += 1;
                        if (counter == searchRank || counter == searchRank - 1)
                        {
                            returnList[index] = new TableRow(counter, kvp.Key, kvp.Value);
                            index += 1;
                        }
                    }
                }
                else if (searchRank == 3)
                {
                    Array.Resize(ref returnList, 4);
                    foreach (KeyValuePair<string, float> kvp in myList)
                    {
                        counter += 1;
                        if (counter == searchRank + 1)
                        {
                            returnList[index] = new TableRow(counter, kvp.Key, kvp.Value);
                            index += 1;
                        }
                    }
                }
                else if (searchRank == 4)
                {
                    Array.Resize(ref returnList, 5);
                    foreach (KeyValuePair<string, float> kvp in myList)
                    {
                        counter += 1;
                        if (counter == searchRank || counter == searchRank + 1)
                        {
                            returnList[index] = new TableRow(counter, kvp.Key, kvp.Value);
                            index += 1;
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, float> kvp in myList)
                    {
                        counter += 1;
                        if (counter == searchRank || counter == searchRank - 1 || counter == searchRank + 1)
                        {
                            returnList[index] = new TableRow(counter, kvp.Key, kvp.Value);
                            index += 1;
                        }
                    }
                }
                for (int i = 0; i < returnList.Length; i++)
                {
                    Debug.Log("DDDDD " + returnList[i].username + " " + returnList[i].levelScore);
                }
                if (found < 0)
                {
                    DatabaseHandler.PostHighScore<HighScores>(playerHighscore, "Level_"+Exit_Script.level_num.ToString(), username, () =>
                    {
                        Debug.Log("done pushing the data" + username);
                    });
                }
                
                if (Exit_Script.level_num == totalLevels){
                    //write code to push accumulated data into highscore column
                    if(found == -1){
                        var playerTotalscore = new MaxScore(level_total_score);
                        DatabaseHandler.PostTotalScore<MaxScore>(playerTotalscore, username, ()=>{
                            Debug.Log("Updated new HighScore");
                        });
                    }
                    else{
                        DatabaseHandler.GetTotalScore(username, (totScore) =>{
                            Debug.Log("Total Score Val " + totScore);
                            if (level_total_score > totScore){
                                var playerTotalscore = new MaxScore(level_total_score);
                                DatabaseHandler.PostTotalScore<MaxScore>(playerTotalscore, username, ()=>{
                                    Debug.Log("Updated new HighScore");
                                });
                            }
                        });
                    }
                    
                    
                    
                    // Debug.Log(DatabaseHandler.totalScore);
                }


                int resultSize = returnList.Length;
                name1.text = returnList[0].username.Split("_")[0];
                score1.text = returnList[0].levelScore.ToString();

                name2.text = returnList[1].username.Split("_")[0];
                score2.text = returnList[1].levelScore.ToString();

                name3.text = returnList[2].username.Split("_")[0];
                score3.text = returnList[2].levelScore.ToString();

                if (resultSize == 3)
                {
                    
                    if(returnList[0].username == (InputNameScript.username+"_"+ analyticsManagerScript.clientID))
                    {
                        name1.color = Color.green;
                        score1.color = Color.green;
                        pos1.color = Color.green;
                    }
                    else if(returnList[1].username == (InputNameScript.username + "_" + analyticsManagerScript.clientID))
                    {
                        name2.color = Color.green;
                        score2.color = Color.green;
                        pos2.color = Color.green;
                    }
                    else
                    {
                        name3.color = Color.green;
                        score3.color = Color.green;
                        pos3.color = Color.green;
                    }
                    name4.gameObject.SetActive(false);
                    name5.gameObject.SetActive(false);
                    namen1.gameObject.SetActive(false);
                    namen2.gameObject.SetActive(false);
                    namen3.gameObject.SetActive(false);
                    score4.gameObject.SetActive(false);
                    score5.gameObject.SetActive(false);
                    scoren1.gameObject.SetActive(false);
                    scoren2.gameObject.SetActive(false);
                    scoren3.gameObject.SetActive(false);
                    pos4.gameObject.SetActive(false);
                    pos5.gameObject.SetActive(false);
                    posn1.gameObject.SetActive(false);
                    posn2.gameObject.SetActive(false);
                    posn3.gameObject.SetActive(false);
                    dot1.gameObject.SetActive(false);
                    dot2.gameObject.SetActive(false);
                    dot3.gameObject.SetActive(false);
                }

                if (resultSize == 4)
                {
                    name4.gameObject.SetActive(true);
                    score4.gameObject.SetActive(true);
                    pos4.gameObject.SetActive(true);
                    
                    name4.text = returnList[3].username.Split("_")[0];
                    score4.text = returnList[3].levelScore.ToString();
                    pos4.text = "4TH";

                    name3.color = Color.green;
                    score3.color = Color.green;
                    pos3.color = Color.green;

                    name5.gameObject.SetActive(false);
                    namen1.gameObject.SetActive(false);
                    namen2.gameObject.SetActive(false);
                    namen3.gameObject.SetActive(false);
                    score5.gameObject.SetActive(false);
                    scoren1.gameObject.SetActive(false);
                    scoren2.gameObject.SetActive(false);
                    scoren3.gameObject.SetActive(false);
                    pos5.gameObject.SetActive(false);
                    posn1.gameObject.SetActive(false);
                    posn2.gameObject.SetActive(false);
                    posn3.gameObject.SetActive(false);
                    dot1.gameObject.SetActive(false);
                    dot2.gameObject.SetActive(false);
                    dot3.gameObject.SetActive(false);
                }

                if (resultSize == 5)
                {
                    
                    if(returnList[3].rank == 4){
                        name4.gameObject.SetActive(true);
                        score4.gameObject.SetActive(true);
                        pos4.gameObject.SetActive(true);

                        name4.text = returnList[3].username.Split("_")[0];
                        score4.text = returnList[3].levelScore.ToString();
                        pos4.text = returnList[3].rank.ToString()+"TH";
                        name4.color = Color.green;
                        score4.color = Color.green;
                        pos4.color = Color.green;
                        
                        name5.gameObject.SetActive(true);
                        score5.gameObject.SetActive(true);
                        pos5.gameObject.SetActive(true);

                        name5.text = returnList[4].username.Split("_")[0];
                        score5.text = returnList[4].levelScore.ToString();
                        pos5.text = returnList[4].rank.ToString()+"TH";


                        namen1.gameObject.SetActive(false);
                        namen2.gameObject.SetActive(false);
                        namen3.gameObject.SetActive(false);
                        scoren1.gameObject.SetActive(false);
                        scoren2.gameObject.SetActive(false);
                        scoren3.gameObject.SetActive(false);
                        posn1.gameObject.SetActive(false);
                        posn2.gameObject.SetActive(false);
                        posn3.gameObject.SetActive(false);
                        dot1.gameObject.SetActive(false);
                        dot2.gameObject.SetActive(false);
                        dot3.gameObject.SetActive(false);

                    }
                    else{
                        namen2.gameObject.SetActive(true);
                        scoren2.gameObject.SetActive(true);
                        posn2.gameObject.SetActive(true);

                        namen2.text = returnList[3].username.Split("_")[0];
                        scoren2.text = returnList[3].levelScore.ToString();
                        posn2.text = returnList[3].rank.ToString()+"TH";
                        
                        namen3.gameObject.SetActive(true);
                        scoren3.gameObject.SetActive(true);
                        posn3.gameObject.SetActive(true);

                        namen3.text = returnList[4].username.Split("_")[0];
                        scoren3.text = returnList[4].levelScore.ToString();
                        posn3.text = returnList[4].rank.ToString()+"TH";
                        namen3.color = Color.green;
                        scoren3.color = Color.green;
                        posn3.color = Color.green;
                        
                        
                        name4.gameObject.SetActive(false);
                        name5.gameObject.SetActive(false);
                        namen1.gameObject.SetActive(false);
                        score4.gameObject.SetActive(false);
                        score5.gameObject.SetActive(false);
                        scoren1.gameObject.SetActive(false);
                        pos4.gameObject.SetActive(false);
                        pos5.gameObject.SetActive(false);
                        posn1.gameObject.SetActive(false);
                        
                    }
                                        
                }
                
                if(resultSize == 6)
                {
                    namen1.text = returnList[3].username.Split("_")[0];
                    scoren1.text = returnList[3].levelScore.ToString();
                    posn1.text = returnList[3].rank.ToString()+"TH";

                    namen2.text = returnList[4].username.Split("_")[0];
                    scoren2.text = returnList[4].levelScore.ToString();
                    posn2.text = returnList[4].rank.ToString() + "TH";
                    namen2.color = Color.green;
                    scoren2.color = Color.green;
                    posn2.color = Color.green;

                    namen3.text = returnList[5].username.Split("_")[0];
                    scoren3.text = returnList[5].levelScore.ToString();
                    posn3.text = returnList[5].rank.ToString() + "TH"; ;

                    name4.gameObject.SetActive(false);
                    name5.gameObject.SetActive(false);
                    score4.gameObject.SetActive(false);
                    score5.gameObject.SetActive(false);
                    pos4.gameObject.SetActive(false);
                    pos5.gameObject.SetActive(false);
                }
            });
        }
        else
        {
            DatabaseHandler.GetHighScore<HighScores>("Level_"+Exit_Script.level_num.ToString(), (users) =>
            {
                Debug.Log("failed state check");
                var myList = new List<KeyValuePair<string, float>>();
                var returnList = new TableRow[3];
                foreach (KeyValuePair<string, HighScores> kvp in users)
                {
                    myList.Add(new KeyValuePair<string, float>(kvp.Key, kvp.Value.levelScore));
                }
                myList.Sort(Compare);
                int count = 0;
                foreach (KeyValuePair<string, float> kvp in myList)
                {
                    if (count < 3)
                    {
                        returnList[count] = new TableRow(count + 1, kvp.Key, kvp.Value);
                        count += 1;
                    }
                    else
                    {
                        break;
                    }
                }
                Debug.Log("result count"+returnList.Length);
                for (int i = 0; i < returnList.Length; i++)
                {
                    Debug.Log("CCCC " + returnList[i].username + " " + returnList[i].levelScore);
                }
            });

            // end
            

        }

        Exit_Script.score_till_curr_level = level_total_score;
        if (Exit_Script.level_num == 3)
        {
            NextLevelButton.SetActive(false);
            MainMenuBtn.SetActive(true);
            level_pass_msg.text = "Game Over";
        }
        else
        {
            Debug.Log("LEVEL NUM - " + Exit_Script.level_num.ToString());
            MainMenuBtn.SetActive(false);
            NextLevelButton.SetActive(true);
            if (Exit_Script.level_num == 1)
            {
                level_pass_msg.text = "Level 1 Finished";
            }
            else if (Exit_Script.level_num == 2)
            {
                level_pass_msg.text = "Level 2 Finished";
            }

        }

    }
        // Update is called once per frame
        void Update()
        {

        }

        public void NextLevel()
        {
            if (Exit_Script.level_num == 1)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Level_2");
            }
            else if (Exit_Script.level_num == 2)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Level_3");
            }
        }
    }

