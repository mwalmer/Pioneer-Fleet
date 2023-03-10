using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IconIndicator : MonoBehaviour
{
    public Sprite iconSprite;
    public string indicatorText;
    public int remainNumber;
    public Color colorForRemains;
    public bool isUnavailable;

    public Image icon;
    public UI_Description text;
    public UI_Description number;
    private Color unavailableColor = new Color(0.3f, 0.3f, 0.3f);
    // Start is called before the first frame update
    void Start()
    {
        if (!icon) icon = GetComponentInChildren<Image>();
        if (!text) text = GetComponentInChildren<UI_Description>();
    }
    void Update()
    {
        if (isUnavailable)
        {
            UpdateText(indicatorText, unavailableColor);
            icon.color = unavailableColor;
        }
        else
        {
            UpdateText(indicatorText, Color.white);
            icon.color = Color.white;
        }
        if (number)
        {
            UpdateRemainNumber();
        }
    }

    public void UpdateIcon(Sprite _iconSprite)
    {
        if (icon)
        {
            icon.sprite = _iconSprite;
        }
        else
        {
            Debug.Log("UI Error: Icon Indicator is not working since there has not registered Image.");
        }
    }
    public UI_Description GetDescriptionControl()
    {
        return text;
    }
    public void UpdateText(string _text, Color _color)
    {
        text.Clear();
        text.AddWords(_text, _color);
        if (remainNumber > 0)
        {
            text.AddWords(" " + remainNumber, colorForRemains);
        }
        text.UpdateSentence();
    }
    public void UpdateRemainNumber()
    {
        number.Clear();
        number.AddWords(remainNumber.ToString(), colorForRemains);
        number.UpdateSentence();
    }
    public void ChangeRemainNumber(int _number)
    {
        remainNumber = _number;
    }
}

