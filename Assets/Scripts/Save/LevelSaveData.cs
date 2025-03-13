using System;

[Serializable]
public class LevelSaveData : SaveData
{
    public const string SaveKeyword = "Save_Level";

    public int LevelIndex;

    public LevelSaveData()
    {
        LevelIndex = 0;
    }

    public void OnLevelSuccess()
    {
        LevelIndex++;

        OnChange();
    }

    private void OnChange()
    {
        OnDataModified?.Invoke();
    }
}