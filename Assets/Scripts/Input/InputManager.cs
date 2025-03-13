using UnityEngine;
using Zenject;

public class InputManager : IInitializable, ITickable
{
    [Inject] private SignalBus _signalBus;

    private bool _canGetInput = false;

    public void Initialize()
    {
        _signalBus.Subscribe<GameSignals.CallLevelLoaded>(OnLevelLoad);
        _signalBus.Subscribe<GameSignals.CallLevelEnd>(x => OnLevelEnd(x.IsSuccess));
    }

    private void OnLevelLoad()
    {
        _canGetInput = true;
    }

    public void OnLevelEnd(bool isSuccess)
    {
        _canGetInput = false;
    }

    public void Tick()
    {
        if (_canGetInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _signalBus.Fire(new GameSignals.CallInputStart());
            }
            if (Input.GetMouseButtonUp(0))
            {
                _signalBus.Fire(new GameSignals.CallInputRelease());
            }
        }
    }
}