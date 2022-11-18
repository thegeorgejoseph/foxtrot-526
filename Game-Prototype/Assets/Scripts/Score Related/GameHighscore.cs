using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameHighscore : MonoBehaviour
{
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
    public static string failureScene;

    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;

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
        HealthManager.health = 1.0f;
        var username = InputNameScript.username + "_" + analyticsManagerScript.clientID;
        Debug.Log("highscore username " + username);
        Debug.Log("Failure Level " + Exit_Script.level_num.ToString());
        DatabaseHandler.GetHighScore<HighScores>(failureScene, (users) =>
        {
            var myList = new List<KeyValuePair<string, float>>();
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
            Debug.Log("found val " + found);
            if(found == -1){
                var playerHighscore = new HighScores(0);
                DatabaseHandler.PostHighScore<HighScores>(playerHighscore, failureScene, username, () =>
                {
                    Debug.Log("done pushing the data " + username + " " + playerHighscore.levelScore);
                });
            }
        });
        DatabaseHandler.GetAllTotalScore<MaxScore>((users) => 
        {
            var myList = new List<KeyValuePair<string, float>>();
            var returnList = new TableRow[6];

            foreach (KeyValuePair<string, MaxScore> kvp in users)
            {
                myList.Add(new KeyValuePair<string, float>(kvp.Key, kvp.Value.totalGameScore));
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
            Debug.Log("P " + found);
            if (found == -1)
            {
                myList.Add(new KeyValuePair<string, float>(username, 0));
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
            if (found == -1)
            {
                var playerTotalscore = new MaxScore(0);
                DatabaseHandler.PostTotalScore<MaxScore>(playerTotalscore, username, ()=>{
                    Debug.Log("Updated new HighScore");
                });
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
