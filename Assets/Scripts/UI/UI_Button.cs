using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    public GameObject PointFrame;
    [SerializeField]
    private bool isMouseOn = false;
    [SerializeField]
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouse();
        if (isMouseOn)
        {
            if (PointFrame) PointFrame.SetActive(true);
            FC_AimmingCursor.disableOriginalCursor = false;
        }
        else
        {
            if (PointFrame) PointFrame.SetActive(false);
            FC_AimmingCursor.disableOriginalCursor = true;
        }
    }

    bool CheckMouse()
    {
        float buttonX = rectTransform.position.x * 100f + (float)Screen.width / 2f - rectTransform.sizeDelta.x / 2f;
        float buttonY = rectTransform.position.y * 100f + (float)Screen.height / 2f + rectTransform.sizeDelta.y / 2f;
        //Debug.Log("local x:" + buttonX + " | rect x:" + rectTransform.position.x + " | game x:" + transform.position.x);
        //Debug.Log("Mouse: (" + Input.mousePosition.x + "," + Input.mousePosition.y + ") | button local pos:" + buttonX + "," + buttonY + " | size:" + rectTransform.sizeDelta.x + "," + rectTransform.sizeDelta.y);
        if (Input.mousePosition.x > buttonX
        && Input.mousePosition.x < buttonX + rectTransform.sizeDelta.x
        && Input.mousePosition.y < buttonY
        && Input.mousePosition.y > buttonY - rectTransform.sizeDelta.y)
        {
            isMouseOn = true;
        }
        else
            isMouseOn = false;
        return isMouseOn;
    }
}
