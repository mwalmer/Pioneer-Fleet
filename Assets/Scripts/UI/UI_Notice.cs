using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Notice : UI_Description
{
    float currentTime;
    float disappearTIme; // if < 0, then will not disappear
    bool isIdle = true;
    int priority = -1;


    // Start is called before the first frame update
    void Start()
    {
        InitUIDescription();
    }

    // Update is called once per frame
    void Update()
    {
        if (disappearTIme < 0)
        {
            isIdle = true;
            return;
        }
        if (currentTime < disappearTIme)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            Clear();
            UpdateSentence();
            isIdle = true;
            priority = -1;
        }
    }
    public void WriteNotice(string _content, Color _color, float appearingTime, int _priority = 1)
    {
        if (_priority < priority) return;
        Clear();
        AddWords(_content, _color);
        disappearTIme = appearingTime;
        currentTime = 0;
        UpdateSentence();
        isIdle = false;
        priority = _priority;
    }
    public bool IsIdle()
    {
        return isIdle;
    }
}
