using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA_GameManager : MonoBehaviour
{
    protected static AA_GameManager gameManager;
    public UI_Timer gameTimer;
    public FC_EndGameEvent gameEndEvent;
    public MatchThree.Stage.StageController gameStage;
    float previousScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer.IsTime())
        {
            // Time out event
            gameEndEvent.gameObject.SetActive(true);
        }

        ReportScore();
    }

    void Init()
    {
        if (gameTimer)
        {
            gameTimer.time = EventData.GetData().MT_timeGiven;
            gameTimer.isCountingDown = true;
            gameTimer.Run();
        }
    }

    public float ReportScore()
    {
        FC_ScoreTaker.RegisterScore("ArmamentsAlign's Score", gameStage.returnScore());
        return gameStage.returnScore();
    }
    public static void RecordScore()
    {
        float scoreDiff = gameManager.gameStage.returnScore() - gameManager.previousScore;
        
    }
}
