using System;

public enum QuestLevel
{
    Easy,
    Normal,
    Hard,
    Extreme
}

[Serializable]
public class Quest
{
    public string id;
    public string title;
    public QuestLevel level;
    public bool isCompleted;

    public Quest(string title, QuestLevel level)
    {
        id = Guid.NewGuid().ToString();
        this.title = title;
        this.level = level;
        isCompleted = false;
    }

    public string GetLevelShort()
    {
        switch (level)
        {
            case QuestLevel.Easy: return "E";
            case QuestLevel.Normal: return "N";
            case QuestLevel.Hard: return "H";
            case QuestLevel.Extreme: return "EX";
            default: return "?";
        }
    }

    public int GetRewardBronze()
    {
        switch (level)
        {
            case QuestLevel.Easy: return 10;
            case QuestLevel.Normal: return 50;
            case QuestLevel.Hard: return 250;
            case QuestLevel.Extreme: return 1000;
            default: return 0;
        }
    }
}