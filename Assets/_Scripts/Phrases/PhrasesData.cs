using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PhrasesData", fileName = "PhrasesData")]
public class PhrasesData : ScriptableObject
{
    [SerializeField] public List<Phrase> phrases = new();
}

[Serializable]
public class Phrase
{
    public string id;
    public List<PhraseStructure> structure = new();
}

[Serializable]
public struct PhraseStructure
{
    public string text;
    public bool isWordSlot;
}
