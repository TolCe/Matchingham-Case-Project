using Zenject;

public class SuccessChecker : IInitializable
{
    [Inject] private SignalBus _signalBus;

    [Inject] private LevelManager _levelManager;

    public int AmountToSuccess { get; private set; }

    public Enums.ObjectTypes GoalItemType { get; private set; }

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
        GoalItemType = levelData.GoalType;
        AmountToSuccess = levelData.GoalAmount;

        MergeObjectData data = MergeObjectsPool.Instance.GetItemDataByType(GoalItemType);

        (ScreenController.Instance.GetScreen(Enums.UIScreenTypes.Gameplay) as GameplayScreen).Initialize(data.Icon, AmountToSuccess);
    }

    public void OnMerge(MergeObject item)
    {
        if (item.ObjectType != GoalItemType)
        {
            return;
        }

        AmountToSuccess--;

        (ScreenController.Instance.GetScreen(Enums.UIScreenTypes.Gameplay) as GameplayScreen).UpdateTargetAmount(AmountToSuccess);

        if (AmountToSuccess <= 0)
        {
            _signalBus.Fire(new GameSignals.CallLevelEnd(true));
        }
    }
}