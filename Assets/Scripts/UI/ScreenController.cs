using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ScreenController : Singleton<ScreenController>
{
    [Inject] private SignalBus _signalBus;

    [SerializeField] private List<UIScreen> _screenList;

    protected override void Awake()
    {
        base.Awake();

        _signalBus.Subscribe<GameSignals.CallLevelLoaded>(CallLevelLoaded);
        _signalBus.Subscribe<GameSignals.CallLevelEnd>(x => OnLevelEnd(x.IsSuccess));
    }
    private void Start()
    {
        ReturnToLobby();
    }

    private void CallLevelLoaded()
    {
        ShowSingleScreen(Enums.UIScreenTypes.Gameplay);
        (GetScreen(Enums.UIScreenTypes.Gameplay) as GameplayScreen).UpdateLevelText();
    }

    private void OnLevelEnd(bool success)
    {
        if (success)
        {
            ShowSingleScreen(Enums.UIScreenTypes.Success);
        }
        else
        {
            ShowSingleScreen(Enums.UIScreenTypes.Failed);
        }
    }

    public UIScreen GetScreen(Enums.UIScreenTypes type)
    {
        return _screenList.First(x => x.ScreenType == type);
    }

    public void ShowSingleScreen(Enums.UIScreenTypes type)
    {
        foreach (UIScreen screen in _screenList)
        {
            if (screen.ScreenType == type)
            {
                screen.Show();
            }
            else
            {
                screen.Hide();
            }
        }
    }

    public void ReturnToLobby()
    {
        (GetScreen(Enums.UIScreenTypes.Loading) as LoadingScreen).Slide(() => ShowSingleScreen(Enums.UIScreenTypes.Lobby));
    }
}
