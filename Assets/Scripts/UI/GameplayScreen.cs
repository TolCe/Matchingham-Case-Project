using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameplayScreen : UIScreen
{
    [Inject] private LevelManager _levelManager;

    [SerializeField] private Image _targetImage;

    [SerializeField] private TMP_Text _targetAmountText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _levelText;

    [SerializeField] private Button _backButton;

    private void Start()
    {
        _backButton.onClick.AddListener(OnBackButtonClicked);
    }

    private void OnBackButtonClicked()
    {
        ScreenController.Instance.ReturnToLobby();
    }

    public void Initialize(Sprite sprite, int amount)
    {
        _targetImage.sprite = sprite;
        UpdateTargetAmount(amount);
    }

    public void UpdateLevelText()
    {
        _levelText.text = $"Level {_levelManager.CurrentLevelIndex + 1}";
    }

    public void UpdateTargetAmount(int amount)
    {
        _targetAmountText.text = $"{amount}";
    }

    public void UpdateTimerText(float timeLeft)
    {
        int min = Mathf.CeilToInt(timeLeft / 60);
        string minText = min >= 10 ? $"{min}" : $"0{min}";

        int sec = Mathf.CeilToInt(timeLeft % 60);
        string secText = sec >= 10 ? $"{sec}" : $"0{sec}";

        _timerText.text = $"{minText}:{secText}";
    }
}