using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour
{
    public GameObject PointFrame;
    public KeyCode shortcut;
    public bool isUsable = true;
    [SerializeField]
    private bool isMouseOn = false;
    [SerializeField]
    private bool isPressing = false;
    [SerializeField]
    private RectTransform rectTransform;
    private CanvasGroup cGroup;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        cGroup = GetComponentInParent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        Shortcut();

        CheckMousePosition();

        if (isMouseOn)
        {
            if (isUsable == false) return;
            if (PointFrame) PointFrame.SetActive(true);
            FC_AimmingCursor.disableOriginalCursor = false;

            if (Input.GetMouseButtonDown(0))
            {
                isPressing = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isPressing = false;
            }
        }
        else
        {
            if (PointFrame) PointFrame.SetActive(false);
            FC_AimmingCursor.disableOriginalCursor = true;
            isPressing = false;
        }
    }

    bool CheckMousePosition()
    {
        isMouseOn = false;
        if (cGroup.alpha == 0) return false;
        //float buttonX = rectTransform.position.x * 100f + (float)Screen.width / 2f - rectTransform.sizeDelta.x / 2f;
        float buttonX = canvas.worldCamera.WorldToScreenPoint(rectTransform.position).x - rectTransform.sizeDelta.x / 2f;
        float buttonY = canvas.worldCamera.WorldToScreenPoint(rectTransform.position).y + rectTransform.sizeDelta.y / 2f;
        //Debug.Log("local x:" + buttonX + " | rect x:" + rectTransform.position.x + " | game x:" + transform.position.x);
        //Debug.Log("Mouse: (" + Input.mousePosition.x + "," + Input.mousePosition.y + ") | button local pos:" + buttonX + "," + buttonY + " | size:" + rectTransform.sizeDelta.x + "," + rectTransform.sizeDelta.y);
        if (Input.mousePosition.x > buttonX
        && Input.mousePosition.x < buttonX + rectTransform.sizeDelta.x
        && Input.mousePosition.y < buttonY
        && Input.mousePosition.y > buttonY - rectTransform.sizeDelta.y)
        {
            isMouseOn = true;
        }
        return isMouseOn;
    }
    public bool IsButtonPressing()
    {
        return isPressing;
    }
    private void Shortcut()
    {
        if (Input.GetKeyDown(shortcut))
        {
            gameObject.GetComponentInChildren<Button>().onClick.Invoke();
        }
    }
}
