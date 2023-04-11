using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ParticalAutoSizing : MonoBehaviour
{
    RectTransform parent;
    RectTransform rect;
    Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        parent = rect.transform.parent.GetComponent<RectTransform>(); ;
        initialScale = rect.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (parent)
        {
            rect.localScale = new Vector3(initialScale.x * parent.localScale.x * 0.01f, initialScale.y * parent.localScale.y * 0.01f, initialScale.z * parent.localScale.z * 0.01f);
        }
    }
}
