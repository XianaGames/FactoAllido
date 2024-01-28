using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NavigatorTool;

public class SimplePopUp : MonoBehaviour, IPopUp
{
    [SerializeField] private List<CloseButton> closeButton;
    [SerializeField] private List<NavigationButton> navigationButtons;

    public UnityEvent OnInitialze;
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    public void Initialize()
    {
        closeButton.ForEach(data => data.button.onClick.AddListener(() => FrameNavigatorProvider.FrameNavigator.CloseFrameById(data.frameIdToClose)));

        navigationButtons.ForEach(data => data.button.onClick.AddListener(() => FrameNavigatorProvider.FrameNavigator.OpenFrameById(data.toScreenId)));

        OnInitialze?.Invoke();
    }

    public void Show()
    {
       gameObject.SetActive(true);
        OnShow?.Invoke();
    }

    public void Hide()
    {
        OnHide?.Invoke();
        gameObject.SetActive(false);
    }
}
