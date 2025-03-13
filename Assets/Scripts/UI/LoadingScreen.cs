using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : UIScreen
{
    [SerializeField] private Slider _slider;

    public async void Slide(Action actionAfterSlide)
    {
        ScreenController.Instance.ShowSingleScreen(Enums.UIScreenTypes.Loading);

        float value = 0;

        await DOTween.To(() => value, x => value = x, 1f, 1f).OnUpdate(() =>
         {
             _slider.value = value;
         }).AsyncWaitForCompletion();

        actionAfterSlide();

        Hide();
    }
}