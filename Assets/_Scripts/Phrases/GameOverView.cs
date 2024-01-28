using NavigatorTool;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour, IScreen
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button replayButton;

    public void Initialize()
    {
        replayButton.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    internal void ShowWith(string text)
    {
        label.text = text;
    }
}