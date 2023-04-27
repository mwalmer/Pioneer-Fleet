using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_TutorialOpener : MonoBehaviour
{
    public bool shouldWaitBlackscreen = false;
    private CanvasGroup group;
    private bool tutorialLock = true;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        if (group) group.alpha = 0;
        if (shouldWaitBlackscreen == false)
        {
            if (!EventData.FC_firstTimePlay)
            {
                CloseTutorial();
            }
            else
            {
                OpenTutorial();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldWaitBlackscreen && tutorialLock)
        {
            if (UI_Blackscreen.blackscreen.IsFinished())
            {
                if (!EventData.FC_firstTimePlay)
                {
                    CloseTutorial();
                }
                else
                {
                    OpenTutorial();
                }
            }
        }

    }


    public void CloseTutorial()
    {
        if (group)
        {
            group.alpha = 0;
        }
        this.gameObject.SetActive(false);
        FC_EndGameEvent.SetGameActivity(true);
        EventData.FC_firstTimePlay = false;
        if (shouldWaitBlackscreen) Time.timeScale = 1;
    }
    public void OpenTutorial()
    {
        this.gameObject.SetActive(true);
        if (group)
        {
            group.alpha = 1;
        }
        FC_EndGameEvent.SetGameActivity(false);
        if (shouldWaitBlackscreen) Time.timeScale = 0;
    }
}
