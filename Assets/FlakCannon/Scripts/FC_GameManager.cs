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
        // FlakCannon: string gameMode = "Elimination" , "Survival"
        //             int difficulty = 3 //0~7
        //             bool IsBossFight = false// if I have time, I can working on it.

        // FlakCannon Player Status:
        //             CannonFireSpeed = 4; // 1 ~ 8
        //             CannonMagazingNumber = 20; // 10 ~ 60
        //             ShieldSustain = 100; // 50 ~ 400
        //             CannonHP = 7; // 3 ~ 20
        //             EnergyGain = 10; // 1 ~ 100

        //EventData.GetData().difficulty = 7;   // For testing
        //EventData.GetData().gameMode = "Survival";

        if (EventData.GetData() is object)
        {
            GameMode = EventData.GetData().gameMode;
            int difficulty = EventData.GetData().difficulty;

            if (difficulty == 0)
            {
                playerHP = 30;
                if (GameMode == "Survival")
                {
                    timeLimit = 60;
                }
                else if (GameMode == "Elimination")
                {
                    timeLimit = 120;
                    winningCount = 20;
                }
            }
            else
            {
                playerHP = (7 - difficulty) * 2 + 1;
                if (GameMode == "Survival")
                {
                    timeLimit = 40 + 60 * ((float)difficulty / 7);
                }
                else if (GameMode == "Elimination")
                {
                    timeLimit = 80 - 20f * ((float)difficulty / 7f);
                    winningCount = (int)(20 + 20f * ((float)difficulty / 7f));
                }
            }
        }




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
            if (GameMode == "Elimination")
            {
                condiReminder.Clear();
                condiReminder.AddWords("Defeat ");
                condiReminder.AddWords(((winningCount - destroyCount) >= 0 ? (winningCount - destroyCount) : 0).ToString(), ((winningCount - destroyCount) <= 0 ? Color.green : Color.red));
                condiReminder.AddWords(" before run out the time.");
                condiReminder.UpdateSentence();
            }
            else if (GameMode == "Survival")
            {
                condiReminder.Clear();
                condiReminder.AddWords("Please ");
                condiReminder.AddWords("SURVIVAL", Color.red);
                condiReminder.AddWords(" in the remaining time.");
                condiReminder.UpdateSentence();
            }
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
            FC_GameManager.IsGameActive = false;
            FC_EnemyProjecter.DestroyAllEnemy();
        }
    }
    void LosingEvent()
    {
        // TODO:: deal the events when player lost the game
        if (gameEndEvent)
        {
            UI_ScreenEffect.StopScreenBump();
            gameEndEvent.gameObject.SetActive(true);
            gameEndEvent.gameEndNotice.color = Color.red;
            gameEndEvent.gameEndNotice.text = "YOU LOSE!";
            FC_GameManager.IsGameActive = false;
        }
    }


    public static void ChangePlayerHP(int add)
    {
        GameManager.playerHP += add;
    }
    public static void PlayerTakeDamage(int damage)
    {
        GameManager.playerHP -= damage;
        UI_ScreenEffect.ScreenGlassFlash(Color.red, 1f, 0.2f);
        UI_ScreenEffect.ScreenUIBump(0.3f, 0.05f, 10f);
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
