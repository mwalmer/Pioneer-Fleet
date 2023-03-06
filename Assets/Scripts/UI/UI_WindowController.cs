using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WindowController : MonoBehaviour
{
    public CanvasGroup uiGroup;


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
    }

    void Fading()
    {
        Debug.Log("is fading");
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


    public void FadeOut()
    {
        fadingStatus = 1;

    }
    public void FadeBack()
    {
        fadingStatus = -1;

    }
}
