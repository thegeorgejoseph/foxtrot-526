using System;



[Serializable] // This makes the class able to be serialized into a JSON
public class HighScores
{
    public float levelScore;

    public HighScores(float levelScore)
    {
        this.levelScore = levelScore;
    }
}