using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Button button;

    public void Configure(string word, Action OnClick)
    {
        label.text = word;
        button.onClick.AddListener(() => OnClick());
    }

    public string GetWord => label.text;
}