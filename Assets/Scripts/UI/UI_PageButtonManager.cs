using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PageButtonManager : MonoBehaviour
{
    public List<UI_Button> buttons;
    public UI_Button currentSelected;
    public RectTransform horiLine;
    private UI_Button selectedButton;
    public float replacingTime = 0.75f; // in sec
    [SerializeField]
    private float currentReplacingTime = 0f;

    private float unselectY = 66f; // in rect transform
    private float selectedY = 78f;
    private float unselectXAdder = -330f;
    private float selectedXAdder = -360f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonSelection();
        if (selectedButton) SwappingButtonFocus();

    }

    void SwappingButtonFocus()
    {
        if (currentSelected != selectedButton)
        {
            currentReplacingTime += Time.deltaTime;
            float timeRatio = currentReplacingTime / (replacingTime * 10f); // 10f is visual multiplier since lerp has a very fast result.
            float currentX = -180f;
            for (int i = 0; i < buttons.Count; ++i)
            {
                RectTransform rectT = buttons[i].GetComponent<RectTransform>();
                if (buttons[i] == selectedButton)
                {
                    // Scaling
                    Vector3 newScale = Vector3.Lerp(rectT.localScale, Vector3.one, timeRatio);
                    if (Mathf.Abs(Mathf.Abs(newScale.x) - 1f) < 0.0001f) newScale = Vector3.one;
                    rectT.localScale = newScale;

                    // Positioning
                    if (i == 0)
                    {
                        currentX = currentX - unselectXAdder + selectedXAdder;
                    }
                    else
                    {
                        currentX = currentX + selectedXAdder;
                    }
                    Vector3 newPos = Vector3.Lerp(rectT.localPosition, new Vector3(currentX, selectedY, rectT.position.z), timeRatio);
                    rectT.localPosition = newPos;

                    // Horizontal Line Positioning
                    float newX = Mathf.Lerp(horiLine.localPosition.x, currentX + 10, timeRatio);
                    horiLine.localPosition = new Vector3(newX, horiLine.localPosition.y, horiLine.localPosition.z);

                    currentX += selectedXAdder - unselectXAdder;
                }
                else
                {
                    Vector3 newScale = Vector3.Lerp(rectT.localScale, new Vector3(0.8f, 0.8f, 0.8f), timeRatio);
                    if (Mathf.Abs(Mathf.Abs(newScale.x) - 0.8f) < 0.0001f) new Vector3(0.8f, 0.8f, 0.8f);
                    rectT.localScale = newScale;

                    if (i != 0)
                    {
                        currentX = currentX + unselectXAdder;
                    }
                    Vector3 newPos = Vector3.Lerp(rectT.localPosition, new Vector3(currentX, unselectY, rectT.position.z), timeRatio);
                    rectT.localPosition = newPos;
                }
            }
            if (currentReplacingTime >= replacingTime)
            {
                currentSelected = selectedButton;
                currentReplacingTime = 0f;
                EnableButtons();
            }
        }
        else
        {
            selectedButton = null;
            EnableButtons();
            currentReplacingTime = 0f;
        }
    }
    void Test()
    {
        foreach (UI_Button button in buttons)
        {
            RectTransform _rectT = button.GetComponent<RectTransform>();
            Debug.Log(button.gameObject.name + " : " + _rectT.localPosition);
        }
    }
    void CheckButtonSelection()
    {
        foreach (UI_Button button in buttons)
        {
            if (button.IsButtonPressing())
            {
                if (currentReplacingTime == 0)
                {
                    selectedButton = button;
                    DisableButtons();
                }
                return;
            }
        }
    }
    void DisableButtons()
    {
        foreach (UI_Button button in buttons)
        {
            button.isUsable = false;
        }
    }
    void EnableButtons()
    {
        foreach (UI_Button button in buttons)
        {
            button.isUsable = true;
        }
    }
}
