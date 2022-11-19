using System;

[Serializable] // This makes the class able to be serialized into a JSON
public class TableRow
{
    public int rank;
    public string username;
    public float levelScore;

    public TableRow(int rank, string username, float levelScore)
    {
        this.rank = rank;
        this.username = username;
        this.levelScore = levelScore;
    }
}