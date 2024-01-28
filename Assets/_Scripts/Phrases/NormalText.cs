using UnityEngine;
using TMPro;

public class NormalText : MonoBehaviour, PhrasePart
{
    [SerializeField] private TextMeshProUGUI label;

    public void Configure(string text)
    {
        label.text = text;
    }
}
