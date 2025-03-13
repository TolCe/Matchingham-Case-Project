using UnityEngine;
using UnityEngine.UI;

public class SuccessScreen : UIScreen
{
    [SerializeField] private Button _resumeButton;

    private void Start()
    {
        _resumeButton.onClick.AddListener(OnResumeClicked);
    }

    private void OnResumeClicked()
    {
        ScreenController.Instance.ShowSingleScreen(Enums.UIScreenTypes.Lobby);

        Hide();
    }
}