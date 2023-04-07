using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_SelectButtonListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string contents;
    public bool isSelected;
    private TextMeshProUGUI text;

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
    }


    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            Debug.Log(gameObject.name + " was pressed!!!");
        }
    }
}
