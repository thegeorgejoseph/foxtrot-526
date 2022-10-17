using System;



[Serializable] // This makes the class able to be serialized into a JSON
public class Aggregate
{
    
    public float playersPlayed;
    public float playersPassed;
    public float enemiesEncountered;
    public float enemiesKilled;
    public float healthAccquired;
    public float portalUsageCount;
    public float levelCompletionTime;

    public Aggregate(float playersPlayed, float playersPassed, float enemiesEncountered, float enemiesKilled, float healthAccquired, float portalUsageCount, float levelCompletionTime)
    {
        this.playersPlayed = playersPlayed;
        this.playersPassed = playersPassed;
        this.enemiesEncountered = enemiesEncountered;
        this.enemiesKilled = enemiesKilled;
        this.healthAccquired = healthAccquired;
        this.portalUsageCount = portalUsageCount;
        this.levelCompletionTime = levelCompletionTime;
    }
}
