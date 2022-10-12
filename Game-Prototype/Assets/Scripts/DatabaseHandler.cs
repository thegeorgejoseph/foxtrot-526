using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using Debug=UnityEngine.Debug;
using Newtonsoft.Json.Linq;
using System;

public static class DatabaseHandler{

    private const string projectID = "foxtrot-analytics-95472";
    private static readonly string databaseURL = $"https://foxtrot-analytics-95472-default-rtdb.firebaseio.com/";
    private static fsSerializer serializer = new fsSerializer();

    public delegate void PostUserCallback();
    // public delegate void GetMetricCallback(Metrics metrics);
    public delegate void GetMetricCallback<T>(Dictionary<string, T> metrics);
    public delegate void GetAggregateCallback(Aggregate aggregate);
    
    public delegate void GetHighScoreCallback(float totVal);
    
    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    
    
    public static void PostMetrics<T>(T metrics, string sessionID, PostUserCallback callback, string section="metrics")
    {
        RestClient.Put<T>($"{databaseURL}{section}/{sessionID}.json", metrics).Then(response => { 
            callback();
            });
    }


    public static void PostHighScore<T>(T highscores, string level, string username, PostUserCallback callback, string section = "highscores")
    {
        RestClient.Put<T>($"{databaseURL}{section}/{level}/{username}.json", highscores).Then(response => { 
            callback();
            Debug.Log("The user was successfully uploaded to the database");
            });
    }

    public static void GetHighScore<T>(string level, GetMetricCallback<T> callback, string section = "highscores"){
        RestClient.Get($"{databaseURL}{section}/{level}.json").Then(response =>{
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, T>), ref deserialized);
            var metrics = deserialized as Dictionary<string, T>;
            callback(metrics);
            Debug.Log("Response Data " + metrics);
        });
    }


    public static void GetTotalScore(string username, GetHighScoreCallback callback, string section="highscores", string level ="totalScore", string param = "totalGameScore"){
        
        RestClient.Get($"{databaseURL}{section}/{level}/{username}/{param}.json").Then(response =>{
            var responseJson = response.Text;
            callback(float.Parse(response.Text));
        });
    }

    public static void PostTotalScore<T>(T maxscores, string username, PostUserCallback callback, string section = "highscores", string level = "totalScore")
    {
        RestClient.Put<T>($"{databaseURL}{section}/{level}/{username}.json", maxscores).Then(response => { 
            Debug.Log("Data inserted to highscores table");
            callback();
        });
    }    

    public static void PostAggregate<T>(T aggregate, string level, PostUserCallback callback, string section = "aggregate")
    {
        RestClient.Put<T>($"{databaseURL}{section}/{level}.json", aggregate).Then(response => { 
            Debug.Log("Data inserted to aggregate table");
            callback();
        });
    }

    public static void GetAggregate<T>(string level, GetAggregateCallback callback, string section = "aggregate"){
        RestClient.Get($"{databaseURL}{section}/{level}.json").Then(response =>{
            var responseJson = response.Text;
            Debug.Log("Type val "+ responseJson);
            char[] spearator = { ',', ':','{','}' };
            String[] strlist = responseJson.Split(spearator);
            float param1 = float.Parse(strlist[2]);
            float param2 = float.Parse(strlist[4]);
            float param3 = float.Parse(strlist[6]);
            float param4 = float.Parse(strlist[8]);
            float param5 = float.Parse(strlist[10]);
            float param6 = float.Parse(strlist[12]);
            float param7 = float.Parse(strlist[14]);
            Debug.Log("enemeies encountered res val "+ param1 +" "+ param2 +" " + param3 +" " + param4 + " "+ param5 +" "+ param6 +" "+ param7);
            var aggregateVal = new Aggregate(param6, param5,param1,param2,param3,param7, param4);
            callback(aggregateVal);
        });
    }
    


     /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetMetrics<T>(GetMetricCallback<T> callback, string section = "metrics")
    {
        RestClient.Get($"{databaseURL}{section}.json").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, T>), ref deserialized);

            var metrics = deserialized as Dictionary<string, T>;
            callback(metrics);
        });
    }
}