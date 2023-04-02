using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WindowController : MonoBehaviour
{
    public CanvasGroup uiGroup;
    public Button cancelButton = null;
    public Button confirmButton = null;

    //Fading UI
    public int fadingStatus = 0; // -1 = fading back, 0 = pause, 1 = fading out
    public float fadingTime = 1; // in sec

    // Start is called before the first frame update
    void Start()
    {
        uiGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingStatus != 0)
        {
            Fading();
        }

        if (uiGroup.alpha == 1 && Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO:: required a window manager for solving confliction with multiple windwos
            //FadeOut();
        }
    }

    void Fading()
    {
        if (fadingStatus == -1)
        {
            uiGroup.alpha = uiGroup.alpha + Time.deltaTime / fadingTime;
            if (uiGroup.alpha >= 1) { fadingStatus = 0; }
        }
        else if (fadingStatus == 1)
        {
            uiGroup.alpha = uiGroup.alpha - Time.deltaTime / fadingTime;
            if (uiGroup.alpha <= 0) { fadingStatus = 0; }
        }
    }

    public void OnOffMenu()
    {
        if (uiGroup.alpha == 1)
        {
            FadeOut();
        }
        else if (uiGroup.alpha == 0)
        {
            FadeBack();
        }
    }

    public void FadeOut()
    {
        UI_WindowsManager.RemoveWindow(this);
        fadingStatus = 1;
    }
    public void FadeBack()
    {
        UI_WindowsManager.RequestWindow(this);
        fadingStatus = -1;
    }

    public void CloseWindow()
    {
        if (cancelButton)
        {
            cancelButton.onClick.Invoke();
        }
    }
    public void OpenWindow()
    {
        FadeBack();
    }
}
