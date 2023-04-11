using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FC_GameManager : MonoBehaviour
{
    [Header("Game Conditions")]
    public static string GameMode = "Elimination"; // Survival, Elimination.
    public static int GameDifficulty = 1; // 0~7
    [Space]
    [Header("UI Handlers")]
    public UI_IconBar hpUI;
    public TextMeshProUGUI destroyCountUI;
    public FC_EndGameEvent gameEndEvent;
    public TextMeshProUGUI gameEndNoticeUI;
    public UI_Description condiReminder;
    public UI_Timer gameTimer;

    public int playerHP = 10;
    public int playerEnergy = 5;
    public int destroyCount = 0;
    public int winningCount = 10;
    public float timeLimit = 60;

    public static bool IsGameActive = true;
    public static FC_GameManager GameManager;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager = this;
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FC_GameManager.ReloadScene();
        }
    }
    private void FixedUpdate()
    {
        CheckGameConditions();
    }


    void InitGame()
    {
        //TODO::


        if (gameTimer)
        {
            gameTimer.RunCountDown(timeLimit);
        }
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }





    void UpdatePlayerInfo()
    {
        if (hpUI != null)
        {
            hpUI.ChangeBarValue(playerHP);
        }
        if (destroyCountUI != null)
        {
            destroyCountUI.text = "Destroyed: " + destroyCount + "/" + winningCount;
        }

        // WinningCondition reminder
        if (condiReminder)
        {
            condiReminder.Clear();
            condiReminder.AddWords("Defeat ");
            condiReminder.AddWords(((winningCount - destroyCount) >= 0 ? (winningCount - destroyCount) : 0).ToString(), ((winningCount - destroyCount) <= 0 ? Color.green : Color.red));
            condiReminder.AddWords(" before run out the time.");
            condiReminder.UpdateSentence();
        }
    }


    void CheckGameConditions()
    {
        if (GameMode == "Elimination")
        {
            if (destroyCount >= winningCount)
            {
                WinningEvent();
            }
            if (gameTimer.IsTime())
            {
                LosingEvent();
            }
        }
        else if (GameMode == "Survival")
        {
            if (gameTimer.IsTime())
            {
                WinningEvent();
            }
        }

        //All cases
        if (playerHP <= 0)
        {
            LosingEvent();
        }
    }
    void WinningEvent()
    {
        // TODO:: deal the events when player won the game
        if (gameEndEvent)
        {
            gameEndEvent.gameObject.SetActive(true);
            gameEndEvent.gameEndNotice.color = Color.green;
            gameEndEvent.gameEndNotice.text = "YOU WIN!";
        }
    }
    void LosingEvent()
    {
        // TODO:: deal the events when player lost the game
        if (gameEndEvent)
        {
            gameEndEvent.gameObject.SetActive(true);
            gameEndEvent.gameEndNotice.color = Color.red;
            gameEndEvent.gameEndNotice.text = "YOU LOSE!";
        }
    }


    public static void ChangePlayerHP(int add)
    {
        GameManager.playerHP += add;
    }
    public static void CountDestroy()
    {
        GameManager.destroyCount++;
    }
    public static void ResetFlakCannonGameSettings()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
    }
}
