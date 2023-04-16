using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FC_EndGameEvent : MonoBehaviour
{
    protected static FC_EndGameEvent endGameEvent;
    public UI_EndGameScoreBoard scoreBoard;
    public TextMeshProUGUI gameEndNotice;
    public CanvasGroup cGroup;
    public string currentMiniGame = "FlakCannon";
    public string nextScene;
    public bool isGameActived = true;
    private float fadingTime = 0.5f;
    private float fadingCurrent = -1;
    private float inputLockInterval = 0.25f;
    private float inputLockCurrent = 0;
    private bool shouldGoNextScene = false;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        endGameEvent = this;

        if (!gameEndNotice)
            gameEndNotice = this.GetComponentInChildren<TextMeshProUGUI>();
        if (!cGroup)
            cGroup = this.GetComponent<CanvasGroup>();

        nextScene = EventData.GetData().lastScene;
        isGameActived = true;

        cGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingCurrent >= 0)
        {
            fadingCurrent += Time.deltaTime;
            cGroup.alpha = Mathf.Lerp(0, 1, fadingCurrent / fadingTime);

            if (fadingCurrent >= fadingTime)
            {
                fadingCurrent = -1;
            }
        }

        if (isGameActived == false && fadingCurrent < 0)
        {
            Cursor.visible = true;
            scoreBoard.ShowScoreBoard();
            if (scoreBoard.IsFinishedRecording() && Input.GetKeyDown(KeyCode.Mouse0))
            {
                GoNextScene();
            }
            if (shouldGoNextScene && UI_Blackscreen.blackscreen.IsFinished())
            {
                SceneManager.LoadScene(nextScene);
            }
            return;
        }
    }
    public void GoNextScene()
    {
        FC_GameManager.IsGameActive = false;
        isGameActived = false;
        FC_GameManager.ResetFlakCannonGameSettings();

        if (EventData.GetData().lastScene == "BattleBridge")
        {
            if (currentMiniGame == "FlakCannon")
            {
                GameObject playerData = GameObject.Find("PlayerData");
                int finalScore = 0;
                if (playerData)
                {
                    if (currentMiniGame == "Starfighter")
                    {
                        //TODO:: sent final score;



                        playerData.GetComponent<PlayerData>().setMinigameInfo(0, finalScore);
                    }
                    else if (currentMiniGame == "FlakCannon")
                    {
                        finalScore = Mathf.RoundToInt((float)FC_ScoreTaker.GetTotalScore() / (float)FC_GameManager.scoreFor100 * 100);
                        if (finalScore < 0) finalScore = 0;
                        else if (finalScore > 100) finalScore = 100;

                        playerData.GetComponent<PlayerData>().setMinigameInfo(1, finalScore);
                    }
                    else if (currentMiniGame == "ArmamentsAlign")
                    {
                        //TODO:: sent final score;


                        Debug.Log(FC_ScoreTaker.GetTotalScore());
                        playerData.GetComponent<PlayerData>().setMinigameInfo(2, finalScore);
                    }
                }
            }

            shouldGoNextScene = true;
            UI_Blackscreen.CallBlackscreen(9, 1);
        }
    }


    public static void EnableEndGameEvent()
    {
        endGameEvent.fadingCurrent = 0;
        FC_EndGameEvent.endGameEvent.gameObject.SetActive(true);
        FC_EndGameEvent.endGameEvent.isGameActived = false;
    }
}
