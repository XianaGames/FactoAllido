using UnityEngine;
using TMPro;

public class WordSlot : MonoBehaviour, PhrasePart
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Color winColor = Color.magenta;
    [SerializeField] private Color loseColor = Color.red;

    private string _correctWord = "";

    public string GetCorrectWord => _correctWord;

    public void Configure(string text)
    {
        label.text = "__________";
        _correctWord = text;
    }

    public void DiscoverWord()
    {
        label.color = winColor;
        label.text = _correctWord;
    }

    public void ShowWrongWord(string wrongWord)
    {
        label.color = loseColor;
        label.text = wrongWord;
    }
}
