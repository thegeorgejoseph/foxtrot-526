using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using Debug=UnityEngine.Debug;
using Newtonsoft.Json.Linq;

public static class DatabaseHandler{

    private const string projectID = "foxtrot-analytics-95472";
    private static readonly string databaseURL = $"https://foxtrot-analytics-95472-default-rtdb.firebaseio.com/";
    private static fsSerializer serializer = new fsSerializer();


    public delegate void PostUserCallback();
    // public delegate void GetMetricCallback(Metrics metrics);
    public delegate void GetMetricCallback<T>(Dictionary<string, T> metrics);
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

    public static void GetHighScore<T>(GetMetricCallback<T> callback, string section = "highscores", string level = "Level_1"){
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