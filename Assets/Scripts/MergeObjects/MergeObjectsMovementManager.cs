using UnityEngine;
using Zenject;

public class MergeObjectsMovementManager : IInitializable
{
    [Inject] private CameraManager _cameraManager;

    [Inject] private TilesManager _tilesManager;

    [Inject] private SignalBus _signalBus;

    public void Initialize()
    {
        _signalBus.Subscribe<GameSignals.CallInputStart>(OnInputGiven);
        _signalBus.Subscribe<GameSignals.CallInputRelease>(OnInputReleased);
    }

    private void OnInputGiven()
    {
    }

    private void OnInputReleased()
    {
        Ray ray = _cameraManager.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            MergeObject mergeObj = hit.collider.GetComponent<MergeObject>();

            mergeObj?.OnSelected();
            _tilesManager.OnObjectSelected(mergeObj);
        }
    }
}