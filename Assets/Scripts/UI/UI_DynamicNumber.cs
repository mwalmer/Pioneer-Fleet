using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_DynamicNumber : MonoBehaviour
{
    private float value = 0;
    private float presentingValue = 0;
    private float previousValue = 0;
    public float dynamicTime = 1f;
    private float currentDynamicTime = -1;
    private TextMeshProUGUI presenter;
    public bool isInt = false;
    public bool colorWhenChangingValue = true;
    private Color normalColor = Color.white;
    // Start is called before the first frame update
    void Start()
    {
        presenter = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (previousValue != value && currentDynamicTime == -1)
        {
            currentDynamicTime = 0;
        }

        if (currentDynamicTime >= 0)
        {
            currentDynamicTime += Time.deltaTime;

            presentingValue = Mathf.Lerp(previousValue, value, currentDynamicTime / dynamicTime);
            if (isInt) presentingValue = Mathf.RoundToInt(presentingValue);
            presenter.text = presentingValue.ToString();

            if (currentDynamicTime >= dynamicTime)
            {
                currentDynamicTime = -1;
                presentingValue = value;
                previousValue = value;
                presenter.color = normalColor;
            }
        }
    }
    public void SetValue(float _value)
    {
        previousValue = presentingValue;
        value = _value;
        currentDynamicTime = 0;

        if (colorWhenChangingValue == false) return;

        if (value >= previousValue)
        {
            presenter.color = Color.green;
        }
        else
        {
            presenter.color = Color.red;
        }
    }
    public void AddValue(float _value)
    {
        SetValue(value + _value);
    }
    public float GetValue()
    {
        return value;
    }

    public void SetColor(Color _color)
    {
        normalColor = _color;
    }
}
