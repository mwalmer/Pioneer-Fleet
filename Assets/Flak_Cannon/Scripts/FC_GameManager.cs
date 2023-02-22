using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FC_GameManager : MonoBehaviour
{
    public TextMeshProUGUI hpUI;
    public TextMeshProUGUI destroyCountUI;
    public TextMeshProUGUI gameEndNoticeUI;
    public UI_Description condiReminder;
    public UI_Timer gameTimer;

    //
    public static float playerHP = 2;
    public static int destroyCount = 0;
    public int winningCount = 10;
    public float timeLimit = 60;


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
            hpUI.text = "HP: " + playerHP;
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
        if (destroyCount > winningCount)
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
        gameEndNoticeUI.color = Color.green;
        gameEndNoticeUI.text = "YOU WIN!";
    }
    void losingEvent()
    {
        // TODO:: deal the events when player lost the game
        gameEndNoticeUI.color = Color.red;
        gameEndNoticeUI.text = "YOU LOSE!";
    }


    public static void ChangePlayerHP(int add)
    {
        playerHP += add;
    }
    public static void CountDestroy()
    {
        destroyCount++;
    }
}
