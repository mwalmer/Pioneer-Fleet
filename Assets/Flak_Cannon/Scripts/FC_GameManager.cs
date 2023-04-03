using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FC_GameManager : MonoBehaviour
{
    public UI_IconBar hpUI;
    public TextMeshProUGUI destroyCountUI;
    public FC_EndGameEvent gameEndEvent;
    public TextMeshProUGUI gameEndNoticeUI;
    public UI_Description condiReminder;
    public UI_Timer gameTimer;

    public static int playerHP = 10;
    public static int destroyCount = 0;
    public int winningCount = 10;
    public float timeLimit = 60;

    public static bool IsGameActive = true;

    // Start is called before the first frame update
    void Start()
    {
        if (gameTimer)
        {
            gameTimer.RunCountDown(timeLimit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerInfo();
    }
    private void FixedUpdate()
    {
        CheckGameConditions();
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

        if (gameTimer)
        {
            if (gameTimer.IsTime())
            {
                losingEvent();
            }
        }

        // WinningCondition
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
        if (destroyCount >= winningCount)
        {
            WinningEvent();
        }
        if (playerHP <= 0)
        {
            losingEvent();
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
    void losingEvent()
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
        playerHP += add;
    }
    public static void CountDestroy()
    {
        destroyCount++;
    }
    public static void ResetFlakCannonGameSettings()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
    }
}
