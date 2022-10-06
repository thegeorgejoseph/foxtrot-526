using System.Collections.Generic;
using FullSerializer;
using Proyecto26;


public static class DatabaseHandler{

    private const string projectID = "foxtrot-analytics-95472";
    private static readonly string databaseURL = $"https://foxtrot-analytics-95472-default-rtdb.firebaseio.com/";
    private static fsSerializer serializer = new fsSerializer();


    public delegate void PostUserCallback();
    // public delegate void GetMetricCallback(Metrics metrics);
    public delegate void GetMetricCallback(Dictionary<string, Metrics> metrics);
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
            // Debug.Log("The user was successfully uploaded to the database");; 
            });
    }

     /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetMetrics(GetMetricCallback callback)
    {
        RestClient.Get($"{databaseURL}metrics.json").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Metrics>), ref deserialized);

            var metrics = deserialized as Dictionary<string, Metrics>;
            callback(metrics);
        });
    }
}