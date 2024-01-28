using System;
using System.Collections.Generic;
using UnityEngine;

public class PhraseView : MonoBehaviour
{
    [SerializeField] private NormalText normalText;
    [SerializeField] private WordSlot wordSlot;

    private Dictionary<string, WordSlot> wordToInstance = new();

    public void Draw(Phrase phraseToDraw)
    {
        phraseToDraw.structure.ForEach(structure =>
        {
            PhrasePart phrase;

            if (!structure.isWordSlot)
                phrase = Instantiate(normalText, transform);
            else
            {
                phrase = Instantiate(wordSlot, transform);
                wordToInstance.Add(structure.text, phrase as WordSlot);
            }

            phrase.Configure(structure.text);

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

    public void AddWord(WordItem wordItem)
    {
        if (wordToInstance.TryGetValue(wordItem.GetWord, out WordSlot slot))
        {
            slot.DiscoverWord();
        }
    }

    public void ShowLose(WordItem wordItem)
    {
        var correctWord = "";

        foreach (var item in wordToInstance.Values)
        {
            correctWord = item.GetCorrectWord;
        }

        if (wordToInstance.TryGetValue(correctWord, out WordSlot slot))
        {
            slot.ShowWrongWord(wordItem.GetWord);
        }
    }
}

public interface PhrasePart
{
    void Configure(string text);
}
