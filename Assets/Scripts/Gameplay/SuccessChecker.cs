using UnityEngine;
using Zenject;

public class SuccessChecker : IInitializable
{
    [Inject] private SignalBus _signalBus;

    [Inject] private LevelManager _levelManager;

    private int _amountToSuccess;

    private Enums.ObjectTypes _goalItemType;

    public void Initialize()
    {
        _signalBus.Subscribe<GameSignals.CallLevelLoaded>(OnLevelLoad);
    }

    private void OnLevelLoad()
    {
        SetGoal(_levelManager.GetCurrentLevelData());
    }

    private void SetGoal(LevelData levelData)
    {
        _goalItemType = levelData.GoalType;
        _amountToSuccess = levelData.GoalAmount;

        MergeObjectData data = MergeObjectsPool.Instance.GetItemDataByType(_goalItemType);

        (ScreenController.Instance.GetScreen(Enums.UIScreenTypes.Gameplay) as GameplayScreen).Initialize(data.Icon, _amountToSuccess);
    }

    public void OnMerge(MergeObject item)
    {
        if (item.ObjectType != _goalItemType)
        {
            return;
        }

        _amountToSuccess--;

        (ScreenController.Instance.GetScreen(Enums.UIScreenTypes.Gameplay) as GameplayScreen).UpdateTargetAmount(_amountToSuccess);

        if (_amountToSuccess <= 0)
        {
            _signalBus.Fire(new GameSignals.CallLevelEnd(true));
        }
    }
}