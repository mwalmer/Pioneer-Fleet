using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProgressBar : MonoBehaviour
{
    public float value; // range: 0~1
    [Space]
    public RectTransform residualFillBar;
    public float barAnimeTime = 0.5f;
    public float residualDelay = 1f; // in sec
    public float residualAnimeTime = 0.5f;
    public Slider fillBar;
    public bool isLeftToRight = false;
    public RectTransform board;
    private float currentTimeForBar;
    private float currentTimeForResidual;
    private RectTransform rect;
    private bool residualAnimeLock = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!fillBar)
        {
            fillBar = GetComponent<Slider>();
        }
        if (!rect)
        {
            rect = GetComponent<RectTransform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (value > 1) value = 1;
        if (value < 0) value = 0;
        BarAnimation();
    }

    void BarAnimation()
    {
        float rectWidth = Screen.width + board.sizeDelta.x;
        Debug.Log(rectWidth + "| gameboard length" + board.sizeDelta.x);
        if (fillBar.value != value)
        {
            currentTimeForBar += Time.deltaTime;
            fillBar.value = Mathf.Lerp(fillBar.value, value, currentTimeForBar / barAnimeTime);
            if (currentTimeForBar > barAnimeTime || fillBar.value == value)
            {
                currentTimeForBar = 0;
                currentTimeForResidual = 0;
                if (isLeftToRight && residualFillBar.offsetMax.x * -1f > rectWidth * (1 - value))
                {
                    residualFillBar.offsetMax = new Vector2(rectWidth * (1 - value) * -1, Mathf.RoundToInt(residualFillBar.offsetMax.y * 100) / 100f);
                }
                else if (!isLeftToRight && residualFillBar.offsetMin.x > rectWidth * (1 - value))
                {
                    residualFillBar.offsetMin = new Vector2(rectWidth * (1 - value), Mathf.RoundToInt(residualFillBar.offsetMin.y * 100) / 100f);
                }
                else
                {
                    residualAnimeLock = false;
                }
            }
        }
        else
        {
            currentTimeForBar = 0;
        }

        if (residualAnimeLock == false)
        {
            currentTimeForResidual += Time.deltaTime;
            if (currentTimeForResidual > residualDelay)
            {
                float width = Mathf.Lerp((isLeftToRight ? residualFillBar.offsetMax.x * -1f : residualFillBar.offsetMin.x), rectWidth * (1 - value), (currentTimeForResidual - residualDelay) / residualAnimeTime);
                width = Mathf.RoundToInt(width * 100) / 100f;
                Debug.Log(residualFillBar.offsetMax.x + " | " + residualFillBar.offsetMin.x + " |" + width);
                if (isLeftToRight)
                {
                    residualFillBar.offsetMax = new Vector2(width * -1, Mathf.RoundToInt(residualFillBar.offsetMax.y * 100) / 100f);
                }
                else
                {
                    residualFillBar.offsetMin = new Vector2(width, Mathf.RoundToInt(residualFillBar.offsetMin.y * 100) / 100f);
                }
            }
            if (currentTimeForResidual > residualAnimeTime + residualDelay)
            {
                residualAnimeLock = true;
                currentTimeForResidual = 0;
            }
        }
    }
}
