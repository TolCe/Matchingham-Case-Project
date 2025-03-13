using UnityEngine;
using Zenject;

public class CameraManager : IInitializable
{
    public Camera MainCamera { get; private set; }

    public void Initialize()
    {
        MainCamera = Camera.main;
    }
}