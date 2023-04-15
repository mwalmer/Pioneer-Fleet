using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ScoreTab : MonoBehaviour
{

    private bool isDisappeared = false;
    public float entranceTime = 0.5f;
    private float currentEntranceTime = -1;
    public float disappearTime = 0.5f;
    private float currentDisappearTime = -1;
    public float presentingTime = 1f;
    public Sprite highlightBackground;
    public Sprite normalBackground;
    public Image highlightTarget;
    public bool hasIcon = true;
    private float currentPresentingTime = -1;
    private CanvasGroup group;
    private Image img;
    private Image icon;
    private TextMeshProUGUI text;
    private RectTransform rect;
    private float width;
    private float height;
    private bool isSetup = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEntranceTime >= 0)
        {
            currentEntranceTime += Time.deltaTime;

            rect.localPosition = new Vector3(Mathf.Lerp(width * -1, 0, currentEntranceTime / entranceTime), rect.localPosition.y, rect.localPosition.z);
            group.alpha = Mathf.Lerp(0, 1, currentEntranceTime / entranceTime * 1.2f);

            if (currentEntranceTime >= entranceTime)
            {
                currentEntranceTime = -1;
                currentPresentingTime = 0;
                group.alpha = 1;
            }
        }

        if (currentPresentingTime >= 0)
        {
            currentPresentingTime += Time.deltaTime;


            if (currentPresentingTime >= presentingTime)
            {
                currentPresentingTime = -1;
                GetDisappear();
            }
        }

        if (currentDisappearTime >= 0)
        {
            currentDisappearTime += Time.deltaTime;

            group.alpha = Mathf.Lerp(1, 0, currentDisappearTime / disappearTime);

            if (currentDisappearTime >= disappearTime)
            {
                UI_ScoreIndicator.RemoveTab(this);
                Destroy(this.gameObject);
                currentDisappearTime = -1;
            }
        }
    }

    public void Init()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();
        foreach (Image _img in GetComponentsInChildren<Image>())
        {
            if (_img.gameObject != this.gameObject)
            {
                icon = _img;
            }
            else
            {
                img = _img;
            }
        }
        if (highlightTarget == false) highlightTarget = img;
        text = GetComponentInChildren<TextMeshProUGUI>();

        width = rect.sizeDelta.x;
        height = rect.sizeDelta.y;

        // invisible at begining
        group.alpha = 0;

        isSetup = true;
    }

    public void GetIn()
    {
        currentEntranceTime = 0;
    }
    public void GetDisappear()
    {
        currentDisappearTime = 0;
    }
    public float GetY()
    {
        return rect.anchoredPosition.y;
    }

    public bool IsDisappeared(bool destroyAfterCheckedIfTrue)
    {
        if (isDisappeared)
        {
            UI_ScoreIndicator.RemoveTab(this);
            Destroy(this.gameObject);
        }
        return isDisappeared;
    }

    public float GetWidth()
    {
        return width;
    }
    public float GetHeight()
    {
        return height;
    }

    public void SetHighlight(bool isHighlight)
    {
        if (isSetup == false) Init();

        if (isHighlight)
        {
            img.color = new Color(1, 1, 1, 100f / 255f);
            img.sprite = highlightBackground;
        }
        else
        {
            img.color = new Color(1, 1, 1, 1f / 255f);
            img.sprite = normalBackground;
        }
    }

    public void SetNewPosY(float _y)
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, _y);
    }
    public void SetText(string _text)
    {
        if (isSetup == false)
        {
            Init();
        }
        text.text = _text;
    }
    public void SetIcon(Sprite _icon, Color _color)
    {
        if (isSetup == false) Init();

        if (hasIcon == false) return;

        icon.sprite = _icon;
        icon.color = _color;
    }
}
