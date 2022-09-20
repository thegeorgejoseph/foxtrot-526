using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Analytics;
using System;

public class AnalyticsManager : MonoBehaviour
{

    private static long sessionID;
    [SerializeField]
    private static Dictionary<string, string> BASE_URLS;

    [SerializeField]
    private static Dictionary<string, List<string>> FORM_FIELDS;

    [RuntimeInitializeOnLoadMethod]
    static void Initialize(){
        BASE_URLS = new Dictionary<string, string>
        {
            { "did_finish", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSfI4qBnZm4DGVz3uVwVhvfDAj0ZukrhMK8HJ0j5WsEk2EcePA/formResponse" },
            {"portal_use", "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdIvCvgLIcmDZ8Rh6-cnuf6dISjI4v8HZxSHpzUN5FMQCnQJg/formResponse"}
        };

        FORM_FIELDS = new Dictionary<string, List<string>>
        {
            { "did_finish", new List<string>
            {
                "entry.299620588",  // sessionID
                "entry.1648586970", // did player finish
                
            }
            },
            { "portal_use", new List<string>
            {
                "entry.1497300299",  // sessionID
                "entry.1903990965", // did player use portal
                
            }
            }
        };
    }

    void Awake()
    {
        sessionID = DateTime.Now.Ticks;
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
