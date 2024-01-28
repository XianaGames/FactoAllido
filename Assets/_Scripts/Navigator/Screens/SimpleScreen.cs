using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NavigatorTool;

public class SimpleScreen : MonoBehaviour, IScreen
{
    [Header("Buttons")]
    [SerializeField] private List<NavigationButton> navigationButtons;

    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Properties")]
    [SerializeField] private string closeTrigger;

    public UnityEvent OnInitialze;
    public UnityEvent OnShow;
    public UnityEvent OnHide;

    public void Initialize()
    {
        foreach (var navigationButton in navigationButtons)
        {
            navigationButton.button.onClick.AddListener(() => FrameNavigatorProvider.FrameNavigator.OpenFrameById(navigationButton.toScreenId));
        }

        OnInitialze?.Invoke();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        OnShow?.Invoke();
    }

    public void Hide()
    {
        if(animator!=null && animator.isActiveAndEnabled) animator?.SetTrigger(closeTrigger);

        Invoke(nameof(Disabled), 0.2f);
    }

    private void Disabled()
    {
        OnHide?.Invoke();
        gameObject.SetActive(false);
    }
}