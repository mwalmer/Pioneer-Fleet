using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Description : MonoBehaviour
{
    string sentence;

    TextMeshProUGUI textRender;

    void Start()
    {
        if (!textRender)
            textRender = GetComponent<TextMeshProUGUI>();

    }

    //API
    public void UpdateSentence()
    {
        textRender.text = sentence;
    }
    public void AddWords(string words)
    {
        sentence = sentence + words;
    }
    public void AddWords(string words, Color color)
    {
        AddWords(ColorWords(words, color));
    }
    public void Clear()
    {
        sentence = "";
    }
    public void Enter()
    {
        sentence += "\n";
    }
    public void InsertWords(int charIndex, string words)
    {
        sentence.Substring(0, charIndex);
        sentence = sentence.Substring(0, charIndex) + words + sentence.Substring(charIndex, sentence.Length);
    }
    public void InsertWords(int charIndex, string words, Color color)
    {
        InsertWords(charIndex, ColorWords(words, color));
    }
    public void DeleteWords(int startIndex, int endIndex)
    {
        sentence = sentence.Substring(0, startIndex) + sentence.Substring(endIndex, sentence.Length);
    }


    // Data Processing Functions
    byte ToByte(float f)
    {
        f = Mathf.Clamp01(f);
        return (byte)(f * 255);
    }
    string ColorWords(string words, Color color)
    {
        string colorCode = string.Format("#{0:X2}{1:X2}{2:x2}", ToByte(color.r), ToByte(color.g), ToByte(color.b));
        return ("<color=" + colorCode + ">" + words + "</color>");
    }
}
