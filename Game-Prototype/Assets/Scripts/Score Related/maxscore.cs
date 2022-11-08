using System;



[Serializable] // This makes the class able to be serialized into a JSON
public class MaxScore
{
    public float totalGameScore;

    public MaxScore(float totalGameScore)
    {
        this.totalGameScore = totalGameScore;
    }
}