using UnityEngine;
using Zenject;

public class CameraManager : IInitializable
{
    public Camera MainCamera { get; private set; }

    public void Initialize()
    {
        MainCamera = Camera.main;

        AdjustCameraSize();
    }

    private void AdjustCameraSize()
    {
        MainCamera.fieldOfView *= 0.46f / ((float)Screen.width / Screen.height);
    }
}