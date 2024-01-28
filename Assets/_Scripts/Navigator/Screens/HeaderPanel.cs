using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NavigatorTool;
using System.Threading.Tasks;

public class HeaderPanel : MonoBehaviour
{
    [SerializeField] private Button closeOpenButton;
    [SerializeField] private float valor;
    [SerializeField] private Vector2 position;
    [SerializeField] private float duration;

    private bool isActive = false;

    public void Awake()
    {
        closeOpenButton.onClick.AddListener(async () => {
            closeOpenButton.enabled = false;
            HandlerPopUp();
            await Task.Delay(1000);
            closeOpenButton.enabled = true;
        });
        position = transform.position;
    }

    private void OnEnable()
    {
        if (isActive) Hide();
    }

    private void HandlerPopUp()
    {
        if (isActive)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        isActive = true;
        position = transform.position;
        transform.DOMoveX(position.x - valor, duration);
    }

    public void Hide()
    {
        isActive = false;
        transform.DOMoveX(position.x, duration);
    }
}
