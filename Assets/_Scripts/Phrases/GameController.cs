using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NavigatorTool;
using UnityEngine;
using System;

public class GameController : MonoBehaviour, IScreen
{
    [SerializeField] private PhrasesData data;
    [SerializeField] private PhraseView phraseView;
    [SerializeField] private WordsPanel wordsPanel;
    [SerializeField] private int wordOptionsAmount = 2;

    private PhraseGenerator generator;
    private PhraseGenerator Generator => generator ??= new(data);

    private List<string> correctOptions = new();

    private int points = 0;
    private int elementAt = 0;

    public void Initialize()
    {
    }

    private IEnumerator NextPhrase()
    {
        yield return new WaitForSeconds(1f);

        points = 0;
        elementAt = 0;

        phraseView.Clear();
        wordsPanel.Clear();

        var phraseToDraw = Generator.GetRandomPhrase();
        phraseView.Draw(phraseToDraw);

        correctOptions = phraseToDraw.structure.FindAll(word => word.isWordSlot).Select(word => word.text).ToList();
        var words = Generator.GetRandomWords(wordOptionsAmount, correctOptions);
        wordsPanel.CreateOptions(words, OnPlayerSelectOption);

    }

    private void OnPlayerSelectOption(WordItem wordItem)
    {
        StopAllCoroutines();
        //@todo correct order, if not, show first the first clicked
        if (correctOptions.Contains(wordItem.GetWord))
        {
            phraseView.AddWord(wordItem);
            points++;
            elementAt++;
        }
        else
        {
            phraseView.ShowLose(wordItem);
            StartCoroutine(Lose());
        }

        if (points >= correctOptions.Count && Generator.RemainingPhrasesAmount > 0)
        {
            StartCoroutine(NextPhrase());
        }
        else if (points >= correctOptions.Count && Generator.RemainingPhrasesAmount <= 0)
        {
            StartCoroutine(Win());
        }
    }

    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(2f);

        var gameOverView = FrameNavigatorProvider.FrameNavigator.OpenFrameByType<GameOverView>();
        gameOverView.ShowWith("Perdiste! jajaja");
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(1f);
        var gameOverView = FrameNavigatorProvider.FrameNavigator.OpenFrameByType<GameOverView>();
        gameOverView.ShowWith("Ganaste! jajaja");
    }

    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(NextPhrase());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
