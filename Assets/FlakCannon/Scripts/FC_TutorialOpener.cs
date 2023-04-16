using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_TutorialOpener : MonoBehaviour
{
    private CanvasGroup group;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        if (group) group.alpha = 0;

        if (!EventData.FC_firstTimePlay)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            FC_GameManager.IsGameActive = false;
            group.alpha = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CloseTutorial()
    {
        if (group)
        {
            group.alpha = 0;
        }
        this.gameObject.SetActive(false);
        FC_GameManager.IsGameActive = true;
    }
    public void OpenTutorial()
    {
        this.gameObject.SetActive(true);
        if (group)
        {
            group.alpha = 1;
        }
        FC_GameManager.IsGameActive = false;
    }
}
