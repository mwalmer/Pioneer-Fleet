using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FC_EndGameEvent : MonoBehaviour
{
    public UI_EndGameScoreBoard scoreBoard;
    public TextMeshProUGUI gameEndNotice;
    public CanvasGroup cGroup;
    public string currentMiniGame = "FlakCannon";
    public string nextScene;
    private float fadingTime = 2f;
    private float fadingCurrent = 0f;
    private float inputLockInterval = 1f;
    private float inputLockCurrent = 0;
    private bool shouldGoNextScene = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!gameEndNotice)
            gameEndNotice = this.GetComponentInChildren<TextMeshProUGUI>();
        if (!cGroup)
            cGroup = this.GetComponent<CanvasGroup>();

        nextScene = EventData.GetData().lastScene;
    }

    // Update is called once per frame
    void Update()
    {
        if (FC_GameManager.IsGameActive == false)
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

        if (cGroup.alpha < 1)
        {
            fadingCurrent += Time.deltaTime;
            cGroup.alpha = Mathf.Lerp(cGroup.alpha, 1, fadingCurrent / fadingTime);
        }
        else
        {
            inputLockCurrent += Time.deltaTime;
            Time.timeScale = 0;
        }
    }
    public void GoNextScene()
    {
        FC_GameManager.IsGameActive = false;
        FC_GameManager.ResetFlakCannonGameSettings();

        if (EventData.GetData().lastScene == "BattleBridge")
        {
            int finalScore = Mathf.RoundToInt((float)FC_ScoreTaker.GetTotalScore() / (float)FC_GameManager.scoreFor100 * 100);
            if (finalScore < 0) finalScore = 0;
            else if (finalScore > 100) finalScore = 100;
            Debug.Log("Final Score: " + finalScore);
            GameObject playerData = GameObject.Find("PlayerData");
            if (playerData)
            {
                if (currentMiniGame == "FlakCannon")
                {
                    playerData.GetComponent<PlayerData>().setMinigameInfo(1, finalScore);
                }
                else if (currentMiniGame == "ArmamentsAlign")
                {
                    //TODO:: sent final score;
                }
            }
        }

        Debug.Log("Time is back: " + Time.timeScale);
        shouldGoNextScene = true;
        UI_Blackscreen.CallBlackscreen(9, 1);
    }
}
