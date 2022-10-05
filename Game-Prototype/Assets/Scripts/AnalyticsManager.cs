using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Analytics;
using System;
using Random=UnityEngine.Random;

public class AnalyticsManager : MonoBehaviour
{

    public string sessionID;
    [SerializeField]
    private static Dictionary<string, string> BASE_URLS;

    [SerializeField]
    private static Dictionary<string, List<string>> FORM_FIELDS;

    private static bool set_sessionID = false;

    [RuntimeInitializeOnLoadMethod]
    static void Initialize(){
        BASE_URLS = new Dictionary<string, string>
        {
            {"master_metrics", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSe0qkfw0HmteA6mJr9D8mqOqwWkkOJOlZU4IvOPwmLVc5wIDg/formResponse"},
            { "did_finish", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfI4qBnZm4DGVz3uVwVhvfDAj0ZukrhMK8HJ0j5WsEk2EcePA/formResponse" },
            {"portal_use", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdIvCvgLIcmDZ8Rh6-cnuf6dISjI4v8HZxSHpzUN5FMQCnQJg/formResponse"},
            {"enemies", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSc-MTXGYvqzHRQvUV8i939d3muFMldVOp2RRugqQmGb-vPrwg/formResponse"},
            {"health_metric", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdvasJd-nrmZOG28ubVEzNGTTI_wVNBh6o5BMcsVvv6xWz6ug/formResponse"}
        };

        FORM_FIELDS = new Dictionary<string, List<string>>
        {
            { "master_metrics", new List<string>
            {
                "entry.864264797",  // sessionID
                "entry.1065146865", // level
                "entry.1655820095", // did player finish
                "entry.1249077952", // enemies encountered
                "entry.792414006", // enemies killed
                "entry.784575873" // health at the end
                
            }
            },
            { "did_finish", new List<string>
            {
                "entry.299620588",  // sessionID
                "entry.1648586970", // did player finish
                "entry.833191274" // level
                
            }
            },
            { "portal_use", new List<string>
            {
                "entry.1497300299",  // sessionID
                "entry.1903990965", // did player use portal
                
            }
            },
            {"enemies",new List<string> {
                "entry.1311867372", // sessionID
                "entry.52117501", // enemies enountered
                "entry.1238626422" // enemies killed
            }},
            {"health_metric", new List<string>{
                "entry.1386133269", // sessionID
                "entry.1228219699" // health status
            }}
        };
    }

    void Awake()
    {
        sessionID = Random.Range(0, 100000).ToString();// DateTime.Now.Ticks.ToString();
        sessionID = "session-"+sessionID;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    IEnumerator Post(string eventName, List<object> eventParams)
    {
      
        WWWForm form = new();
        List<string> fieldNames = FORM_FIELDS[eventName];
        form.AddField(fieldNames[0], sessionID.ToString());
        
        for (int i = 1; i < fieldNames.Count; i++)
        {
            form.AddField(fieldNames[i], eventParams[i - 1].ToString());
        }

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URLS[eventName], form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.LogFormat("Form upload complete! {0}", eventName);
            }
        }
    }

    public void HandleEvent(string eventName, List<object> eventParams)
    {
        Debug.Log("EVENT HANDLED");
        StartCoroutine(Post(eventName, eventParams));
    }
    
    
}
