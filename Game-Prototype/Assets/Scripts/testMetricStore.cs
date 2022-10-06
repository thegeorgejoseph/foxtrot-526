using System;



[Serializable] 
public class testMetricStore
{
    public string sessionID;

    public string timestamp;

    public string level;
    public string bullets_used;
    public string portal_use_count;
    public string high_score;
    
    public testMetricStore(string sessionID, string timestamp, string level, string bullets_used, string portal_use_count, string high_score)
    {
        this.sessionID = sessionID;
        this.timestamp = timestamp;
        this.level = level;
        this.bullets_used = bullets_used;
        this.portal_use_count = portal_use_count;
        this.high_score = high_score;
    }
}