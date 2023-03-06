using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WindowsManager : MonoBehaviour
{
    public static UI_WindowController currentWindow;

    // Update is called once per frame
    void Update()
    {

    }

    public static void ChangeCurrentWindow(UI_WindowController _window)
    {
        if (currentWindow && _window != currentWindow)
        {
            currentWindow.FadeBack();
        }
        currentWindow = _window;
    }

    

}
