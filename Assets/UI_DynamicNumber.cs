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
                presenter.color = Color.white;
            }
        }
    }
    public void SetValue(float _value)
    {
        previousValue = presentingValue;
        value = _value;

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
}
