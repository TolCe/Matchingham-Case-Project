using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LobbyScreen : UIScreen
{
    [Inject] private LevelManager _levelManager;

    [SerializeField] private Button _playButton;

    [SerializeField] private TMP_Text _levelText;

    private void Start()
    {
        _playButton.onClick.AddListener(OnPlayPressed);
    }
    private void OnEnable()
    {
        WriteLevel();
    }

    private void OnPlayPressed()
    {
        _levelManager.LoadLevel();

        Hide();
    }

    private void WriteLevel()
    {
        _levelText.text = $"{_levelManager.CurrentLevelIndex + 1}";
    }
}
