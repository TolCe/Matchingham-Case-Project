using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SaveManager : IInitializable
{
    public Action OnSaveLoaded;

    public void Initialize()
    {
        LoadSaveFiles();
    }

    private async void LoadSaveFiles()
    {
        await SetLevelSaveData();

        OnSaveLoaded?.Invoke();
    }

    public LevelSaveData LevelSaveData { get; private set; }
    private async Task SetLevelSaveData()
    {
        LevelSaveData = await InitializeData<LevelSaveData>(LevelSaveData.SaveKeyword);
        LevelSaveData.OnDataModified += () => SaveData(LevelSaveData, LevelSaveData.SaveKeyword);
    }

    private void SaveData<T>(T data, string keyword)
    {
        string jsonData = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + $"/{keyword}.json", jsonData);
    }

    public async Task<T> InitializeData<T>(string keyword) where T : new()
    {
        T data = default;

        if (System.IO.File.Exists(Application.persistentDataPath + $"/{keyword}.json"))
        {
            string jsonData = await System.IO.File.ReadAllTextAsync(Application.persistentDataPath + $"/{keyword}.json");

            data = JsonUtility.FromJson<T>(jsonData);
        }
        else
        {
            data = new T();

            SaveData(data, keyword);
        }

        return data;
    }
}