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
    private bool shouldGoNextScene = false;
    private bool isEventEnabled = false;
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
        cGroup.blocksRaycasts = false;
        cGroup.interactable = false;
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

                cGroup.alpha = 1;
                cGroup.blocksRaycasts = false;
                cGroup.interactable = false;

                isEventEnabled = true;
            }
        }

        if (isGameActived == false && fadingCurrent < 0)
        {
            Cursor.visible = true;
            scoreBoard.ShowScoreBoard();
            if (scoreBoard.IsFinishedRecording() && Input.anyKey)
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
                        finalScore = Mathf.RoundToInt((float)FC_ScoreTaker.GetTotalScore() / 200f * 100);
                        finalScore = (finalScore <= 100 ? (finalScore >= 0 ? finalScore : 0) : 100);

                        playerData.GetComponent<PlayerData>().setMinigameInfo(1, finalScore);
                    }
                    else if (currentMiniGame == "FlakCannon")
                    {
                        finalScore = Mathf.RoundToInt((float)FC_ScoreTaker.GetTotalScore() / (float)FC_GameManager.scoreFor100 * 100);
                        finalScore = (finalScore <= 100 ? (finalScore >= 0 ? finalScore : 0) : 100);

                        playerData.GetComponent<PlayerData>().setMinigameInfo(2, finalScore);
                    }
                    else if (currentMiniGame == "ArmamentsAlign")
                    {
                        //TODO:: sent final score;
                        finalScore = Mathf.RoundToInt((float)FC_ScoreTaker.GetTotalScore() / 100f * 100);
                        finalScore = (finalScore <= 100 ? (finalScore >= 0 ? finalScore : 0) : 100);

                        playerData.GetComponent<PlayerData>().setMinigameInfo(3, finalScore);
                    }
                }
            }
        }

        shouldGoNextScene = true;
        UI_Blackscreen.CallBlackscreen(9, 1);

    }


    public static void EnableEndGameEvent()
    {
        if (endGameEvent.isEventEnabled == false)
        {
            // Game states
            endGameEvent.isGameActived = false;
            if (endGameEvent.currentMiniGame == "FlakCannon")
            {
                FC_GameManager.IsGameActive = false;
                endGameEvent.scoreBoard.SetScoreRequirement(FC_GameManager.scoreFor100);
            }
            else if (endGameEvent.currentMiniGame == "Starfigther")
            {

            }
            else if (endGameEvent.currentMiniGame == "ArmamentsAlign")
            {

                endGameEvent.scoreBoard.SetScoreRequirement(100);
            }

            // EndGameEvent Window
            if (endGameEvent.fadingCurrent < 0) endGameEvent.fadingCurrent = 0;
            endGameEvent.gameObject.SetActive(true);
        }
    }

    public static void SetTitle(string title, Color color)
    {
        endGameEvent.gameEndNotice.text = title;
        endGameEvent.gameEndNotice.color = color;
    }
}
