using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private Enums.UIScreenTypes _screenType;
    public Enums.UIScreenTypes ScreenType { get { return _screenType; } }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
