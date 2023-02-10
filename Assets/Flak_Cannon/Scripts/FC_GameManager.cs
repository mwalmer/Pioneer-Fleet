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

    //
    public static float playerHP = 2;
    public static int destroyCount = 0;
    public int winningCount = 10;


    // Start is called before the first frame update
    void Start()
    {

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
    }


    void CheckGameConditions()
    {
        if (playerHP <= 0)
        {
            losingEvent();
        }
        if (destroyCount > winningCount)
        {
            WinningEvent();
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
