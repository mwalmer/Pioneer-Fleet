using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MatchThree.Utilities;
using MatchThree.Stage;
using TMPro;

public class UI_CanvasInit : MonoBehaviour
{
    private Canvas canvas;
    public static UI_CanvasInit canvasInit;
    public TextMeshProUGUI scoreCountUI;
    private bool goNextScene = false;
    private string nameScene;
   // ActionManager thisActionManager;
    // Start is called before the first frame update
    void Start()
    {
        canvasInit = this;
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
        
        if (goNextScene && UI_Blackscreen.blackscreen.IsFinished())
        {
            SceneManager.LoadScene(canvasInit.nameScene);
            //UpdateScore();
        }
    }
    void UpdateScore()
    {
        scoreCountUI.text = "Score: ";
    }
    public static void EnterNextScene(string _sceneName)
    {
        UI_Blackscreen.blackscreen.OpenBlack();
        canvasInit.nameScene = _sceneName;
        canvasInit.goNextScene = true;
    }
    public Canvas GetCanvas()
    {
        return canvas;
    }

    public static Canvas GetMainCanvas()
    {
        if (canvasInit != null)
        {
            return canvasInit.GetCanvas();
        }
        return null; ;
    }

}
