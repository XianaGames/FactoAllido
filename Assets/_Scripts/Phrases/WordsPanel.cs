using System;
using System.Collections.Generic;
using UnityEngine;

public class WordsPanel : MonoBehaviour
{
    [SerializeField] private WordItem wordItem;

    public void CreateOptions(List<string> words, Action<WordItem> OnOptionClick)
    {
        words.ForEach(word =>
        {
            var wordInstance = Instantiate(wordItem, transform);
            wordInstance.Configure(word, () => OnOptionClick(wordInstance));
        });
    }

    //@todo pool object
    public void Clear()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
