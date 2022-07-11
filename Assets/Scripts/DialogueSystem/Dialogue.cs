using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField]string characterName;

    [TextArea(3,10)]
    [SerializeField] string[] sentences;

    public string[] Sentences
    {
        get { return sentences; }
        set { sentences = value; }
    }
    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
}
