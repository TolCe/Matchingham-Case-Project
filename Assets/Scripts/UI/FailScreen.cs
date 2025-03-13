using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FailScreen : UIScreen
{
    [Inject] private LevelManager _levelManager;

    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _retryButton;

    private void Start()
    {
        _quitButton.onClick.AddListener(OnQuitClicked);
        _retryButton.onClick.AddListener(OnRetryClicked);
    }

    private void OnQuitClicked()
    {
        ScreenController.Instance.ReturnToLobby();

        Hide();
    }

    private void OnRetryClicked()
    {
        _levelManager.RestartLevel();

        Hide();
    }
}
