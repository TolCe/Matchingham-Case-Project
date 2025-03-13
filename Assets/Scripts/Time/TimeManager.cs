using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class TimeManager : IInitializable, ITickable
{
    [Inject] private SignalBus _signalBus;

    [Inject] private LevelManager _levelManager;

    private float _timeLeft = 300;

    private bool _shouldCount;

    private GameplayScreen _gameplayScreen;

    public void Initialize()
    {
        _signalBus.Subscribe<GameSignals.CallLevelLoaded>(OnLevelLoad);
        _signalBus.Subscribe<GameSignals.CallLevelEnd>(x => OnLevelEnd(x.IsSuccess));

        _gameplayScreen = ScreenController.Instance.GetScreen(Enums.UIScreenTypes.Gameplay) as GameplayScreen;
    }

    public void Tick()
    {
        if (_shouldCount)
        {
            _timeLeft -= Time.deltaTime;

            _gameplayScreen.UpdateTimerText(_timeLeft);

            if (_timeLeft <= 0)
            {
                _signalBus.Fire(new GameSignals.CallLevelEnd(false));
            }
        }
    }

    private void OnLevelLoad()
    {
        ResetTimer(_levelManager.GetCurrentLevelData().TimerSeconds);

        StartCounting();
    }

    private void OnLevelEnd(bool success)
    {
        _shouldCount = false;
    }

    private void ResetTimer(int time)
    {
        _timeLeft = time;
        _gameplayScreen.UpdateTimerText(_timeLeft);
    }

    private void StartCounting()
    {
        _shouldCount = true;
    }
}