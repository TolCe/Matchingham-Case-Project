using System;
using UnityEngine;
using Zenject;

public class LevelManager : IInitializable
{
    [Inject] private SaveManager _saveManager;

    [Inject] private LevelDatabase _levelDatabase;

    [Inject] private SignalBus _signalBus;

    public int CurrentLevelIndex { get { return _saveManager.LevelSaveData.LevelIndex; } }

    public LevelData GetCurrentLevelData()
    {
        return _levelDatabase.LevelDataList[_saveManager.LevelSaveData.LevelIndex % _levelDatabase.LevelDataList.Count];
    }

    public void Initialize()
    {
        _signalBus.Subscribe<GameSignals.CallLevelEnd>(x => OnLevelEnd(x.IsSuccess));
    }

    public void LoadLevel()
    {
        MergeObjectsPool.Instance.GenerateItems(GetCurrentLevelData());

        _signalBus.Fire(new GameSignals.CallLevelLoaded());
    }

    public void OnLevelEnd(bool isSuccess)
    {
        if (isSuccess)
        {
            _saveManager.LevelSaveData.OnLevelSuccess();
        }
    }

    public void RestartLevel()
    {
        LoadLevel();
    }
}
