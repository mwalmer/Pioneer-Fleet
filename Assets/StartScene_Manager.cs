using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene_Manager : MonoBehaviour
{
    public string firstSceneName = "NodeMap";
    private int operation = 0; // -1 = exit, 1 = start
    // Start is called before the first frame update
    void Start()
    {
        UI_Blackscreen.mainCanvas = this.GetComponent<Canvas>();
        UI_Blackscreen.CallBlackscreen(9, -1);
    }

    void Update()
    {
        if (operation == 1 && UI_Blackscreen.blackscreen.IsFinished())
        {
            SceneManager.LoadScene(firstSceneName);
        }
        else if (operation == -1 && UI_Blackscreen.blackscreen.IsFinished())
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE
            Application.Quit();
#endif
        }
    }

    public void StartGame()
    {
        operation = 1;
        UI_Blackscreen.blackscreen.OpenBlack();
    }
    public void ExitGame()
    {
        operation = -1;
        UI_Blackscreen.blackscreen.OpenBlack();
    }
}
