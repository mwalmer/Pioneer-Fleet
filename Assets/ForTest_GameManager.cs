using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTest_GameManager : MonoBehaviour
{
    public Canvas mainCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        if (!UI_Blackscreen.blackscreen)
        {
            UI_Blackscreen.mainCanvas = mainCanvas;
            UI_Blackscreen.CallBlackscreen(9, -1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
