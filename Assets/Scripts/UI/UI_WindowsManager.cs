using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WindowsManager : MonoBehaviour
{
    public static List<Object> windows = new List<Object>();
    public UI_SettingMenu settingMenu;
    public static UI_WindowController currentWindow;

    // Update is called once per frame
    void Update()
    {
        EscapeKeyEvent();
    }

    void EscapeKeyEvent()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (windows.Count > 0)
            {
                Object temp = windows[windows.Count - 1];
                if (temp is UI_WindowController)
                {
                    ((UI_WindowController)temp).CloseWindow();
                }
                else if (temp is UI_SettingMenu)
                {
                    ((UI_SettingMenu)temp).OnOffMenu();
                }
                windows.Remove(temp);
            }
            else
            {
                // It will pause the game as well.
                settingMenu.OnOffMenu();
            }
        }
    }


    public static void ChangeCurrentWindow(UI_WindowController _window)
    {
        if (currentWindow && _window != currentWindow)
        {
            currentWindow.FadeBack();
        }
        currentWindow = _window;
    }

    public static void RequestWindow(Object uiWindow)
    {
        if (windows.Contains(uiWindow) == false)
        {
            Debug.Log("Requested a window");
            windows.Add(uiWindow);
        }
    }
    public static void RemoveWindow(Object uiWindow)
    {
        if (windows.Contains(uiWindow))
        {
            Debug.Log("removed a window");
            windows.Remove(uiWindow);
        }
    }

}
