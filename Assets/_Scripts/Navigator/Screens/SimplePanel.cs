using UnityEngine;
using NavigatorTool;

public class SimplePanel : MonoBehaviour, IPopUp
{
    [Header("Components")]
    [SerializeField] private Animator animator;

    [Header("Properties")]
    [SerializeField] private string closeTrigger;

    private bool _isEnabled;

    public bool IsEnabled => _isEnabled;

    public void Initialize()
    {
        //Load all first notifications and references the buttons
    }

    public void Show()
    {
        //Load new notifications or unexpired notifications 
        //Do animation
        gameObject.SetActive(true);
        _isEnabled = true;
    }

    public void Hide()
    {
        _isEnabled = false;
        animator.SetTrigger(closeTrigger);

        Invoke(nameof(Disabled), 0.2f);
    }

    private void Disabled()
    {
        gameObject.SetActive(false);
    }
}
