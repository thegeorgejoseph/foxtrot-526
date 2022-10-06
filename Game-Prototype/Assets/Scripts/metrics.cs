using System;



[Serializable] // This makes the class able to be serialized into a JSON
public class Metrics
{
    public string sessionID;

    public string timestamp;
    public string level;
    public string finished;
    public string enemies_encountered;
    public string enemies_killed;
    public string health;
    public Metrics(string sessionID, string timestamp, string level, string finished, string enemies_encountered, string enemies_killed, string health)
    {
        this.sessionID = sessionID;
        this.timestamp = timestamp;
        this.level = level;
        this.finished = finished;
        this.enemies_encountered = enemies_encountered;
        this.enemies_killed = enemies_killed;
        this.health = health;
    }
}