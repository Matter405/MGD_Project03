using System;

/// <Summary>
/// This class stores all of our save data values.
/// Serializable means we can save this data to objects later
/// </Summary>
[Serializable]

public class SaveData
{
    public int CurrentScore = 0;
    public int HighScore = 0;
}