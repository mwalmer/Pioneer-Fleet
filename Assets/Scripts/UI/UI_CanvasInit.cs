using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CanvasInit : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        if (canvas)
        {
            UI_Blackscreen.mainCanvas = canvas;
            UI_Blackscreen.CallBlackscreen(9, -1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
