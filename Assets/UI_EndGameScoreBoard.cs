using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EndGameScoreBoard : MonoBehaviour
{
    public UI_ScoreTab scoreTabPrefab;
    public UI_DynamicNumber dynamicNumber;
    public TextMeshProUGUI scoreRequirement;
    public UI_CanvasGroupControl continueNotice;

    public float headOffset;
    public float yOffset;
    public float height;

    private Queue<UI_ScoreTab> tabQueue;
    private List<UI_ScoreTab> scoreTabs;

    public CanvasGroup subGroup;
    private CanvasGroup group;
    private float appearingTime = 1f;
    private float currentAppearingTime = -1;
    bool isRecording = false;
    bool isFinishedRecording = false;
    private int recordedScore = 0;
    private bool isAppeared = false;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        tabQueue = new Queue<UI_ScoreTab>();
        scoreTabs = new List<UI_ScoreTab>();
        group.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAppearingTime >= 0)
        {
            currentAppearingTime += Time.deltaTime;

            group.alpha = Mathf.Lerp(0, 1, currentAppearingTime / appearingTime);
            if (subGroup) subGroup.alpha = group.alpha;

            if (currentAppearingTime >= appearingTime)
            {
                isAppeared = true;
                currentAppearingTime = -1;
                group.alpha = 1;
                subGroup.alpha = 1;
                StartToRecord();
            }
        }

        if (group.alpha != 1) return;

        if (tabQueue.Count > 0)
        {
            if (tabQueue.Peek().IsFinishedGetIn())
            {
                recordedScore += FC_ScoreTaker.GetScore(tabQueue.Peek().name);
                dynamicNumber.SetValue(recordedScore);
                scoreTabs.Add(tabQueue.Dequeue());
                if (tabQueue.Count > 0)
                {
                    tabQueue.Peek().GetIn();
                    tabQueue.Peek().SetNewPosY(-headOffset - yOffset - (yOffset + height) * scoreTabs.Count);
                }
            }
        }
        else
        {
            isFinishedRecording = true;
            if (continueNotice) continueNotice.Show();
        }

        scoreRequirement.text = "/ " + FC_GameManager.scoreFor100;
        float scoreMargin = (float)recordedScore / (float)FC_GameManager.scoreFor100;
        if (scoreMargin > 1) scoreMargin = 1;
        else if (scoreMargin < 0) scoreMargin = 0;
        if (scoreMargin >= 0.9f) dynamicNumber.SetColor(Color.green);
        else if (scoreMargin >= 0.5f) dynamicNumber.SetColor(Color.yellow);
        else dynamicNumber.SetColor(Color.red);
    }

    public void ShowScoreBoard()
    {
        if (isAppeared == false && currentAppearingTime == -1) currentAppearingTime = 0;
    }
    public void StartToRecord()
    {
        UI_ScoreTab scoreTab;
        foreach (KeyValuePair<string, int> score in FC_ScoreTaker.GetScores())
        {
            scoreTab = Instantiate(scoreTabPrefab, this.transform);
            scoreTab.name = score.Key;
            scoreTab.Init();
            scoreTab.frontText.text = score.Key;
            scoreTab.tailText.text = (score.Value < 0 ? "" : "+") + score.Value;
            scoreTab.tailText.color = (score.Value < 0 ? Color.red : Color.white);
            scoreTab.rightToLeft = true;
            scoreTab.presentingTime = 9999f;
            tabQueue.Enqueue(scoreTab);
        }

        if (tabQueue.Count > 0)
        {

            tabQueue.Peek().GetIn();
            tabQueue.Peek().SetNewPosY(-headOffset - yOffset - (yOffset + height) * scoreTabs.Count);
        }
    }
    public bool IsFinishedRecording()
    {
        return isFinishedRecording;
    }
}
