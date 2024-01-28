using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PhraseGenerator
{
    private readonly PhrasesData _data;

    private List<string> wordOptions = new();
    private List<Phrase> _phrases = new();

    public PhraseGenerator(PhrasesData data)
    {
        _phrases.AddRange(data.phrases);
        _data = data;
        Initialize();
    }

    public void Initialize()
    {
        wordOptions = _data.phrases.SelectMany(phrases => phrases.structure).Where(structure => structure.isWordSlot).Select(word => word.text).ToList();
    }

    //@todo recursive
    public List<string> GetRandomWords(int amount, List<string> correctOptions)
    {
        List<string> randomWords = new();
        List<string> wordOptionsTemp = new();

        wordOptionsTemp.AddRange(wordOptions);

        for (int i = 0; i < correctOptions.Count; i++)
        {
            wordOptionsTemp.Remove(correctOptions[i]);
        }

        var amountFixed = Mathf.Clamp(amount, 0, wordOptionsTemp.Count - 1); 

        for (int i = 0; i < amountFixed; i++)
        {
            var max = wordOptionsTemp.Count;
            var randomPosition = Random.Range(0, max);
            randomWords.Add(wordOptionsTemp.ElementAt(randomPosition));
            wordOptionsTemp.RemoveAt(randomPosition);
        }

        randomWords.AddRange(correctOptions);

        return randomWords;
    }

    public Phrase GetRandomPhrase()
    {
        var randomPosition = Random.Range(0, _phrases.Count);
        var selectedPhrase = _phrases.ElementAt(randomPosition);
        _phrases.RemoveAt(randomPosition);

        return selectedPhrase;
    }

    public int RemainingPhrasesAmount => _phrases.Count;
}
