using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScoreIndicator : MonoBehaviour
{
    public static UI_ScoreIndicator scoreIndicator;
    [SerializeField]
    protected List<UI_ScoreTab> scoreTabs;
    protected Queue<UI_ScoreTab> tabQueue;
    public float appearingTime;
    public int maxAppears = 8;
    public UI_ScoreTab scoretabPrefab;
    public float height = 40;
    public float yOffset = 8;
    public float headOffset = 60;
    public float makeSpaceTime = 0.5f;
    private float currentSpaceTime = -1;
    RectTransform scorePanel;
    public Sprite defeatIcon;
    public Sprite blockIcon;
    // Start is called before the first frame update
    void Start()
    {
        scoreIndicator = this;
        scoreTabs = new List<UI_ScoreTab>();
        tabQueue = new Queue<UI_ScoreTab>();
        scorePanel = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tabQueue.Count > 0)
        {
            if (currentSpaceTime < 0)
            {
                currentSpaceTime = 0;
                if (scoreTabs.Count > 0) scoreTabs[scoreTabs.Count - 1].SetHighlight(false);
            }
            // 1. Move a line space for new tab
            MoveALine();

            if (currentSpaceTime >= makeSpaceTime)
            {
                // 2. Move the tab from queue into list
                scoreTabs.Add(tabQueue.Dequeue());

                // 3. Play the visual actions of the new tab
                scoreTabs[scoreTabs.Count - 1].SetNewPosY((headOffset + yOffset) * -1);
                scoreTabs[scoreTabs.Count - 1].GetIn();
                scoreTabs[scoreTabs.Count - 1].SetHighlight(true);

                currentSpaceTime = -1;
            }
        }
    }

    void MoveALine()
    {
        if (scoreTabs.Count == 0)
        {
            currentSpaceTime = makeSpaceTime;
            return;
        }

        //TODO:: Move all curent registered tab down for a tab line space
        currentSpaceTime += Time.deltaTime;
        float oldY = 0;
        float newY = 0;
        /*
            Position calculation: yOffset + (height + yOffset) * n
        */
        for (int i = scoreTabs.Count - 1; i >= 0; --i)
        {
            newY = Mathf.Lerp(-headOffset - yOffset - (height + yOffset) * (scoreTabs.Count - 1 - i),
                               -headOffset - yOffset - (height + yOffset) * (scoreTabs.Count - i),
                               currentSpaceTime / makeSpaceTime);
            scoreTabs[i].SetNewPosY(newY);
        }
    }

    public static void RemoveTab(UI_ScoreTab _tab)
    {
        scoreIndicator.scoreTabs.Remove(_tab);
    }


    UI_ScoreTab GenerateScoreTab()
    {
        UI_ScoreTab newTab = Instantiate(scoretabPrefab, this.transform);
        tabQueue.Enqueue(newTab);

        return newTab;
    }

    public static void DefeatEnemyFighter(string enemyName, float enemyDistance)
    {
        UI_ScoreTab newTab = scoreIndicator.GenerateScoreTab();
        newTab.SetText(enemyName + " - " + enemyDistance + "m");
        newTab.SetIcon(scoreIndicator.defeatIcon, new Color(1, 128f / 255f, 128f / 255f, 1));
    }
    public static void BlockEnemyFighter(string enemyName)
    {
        UI_ScoreTab newTab = scoreIndicator.GenerateScoreTab();
        newTab.SetText(enemyName);
        newTab.SetIcon(scoreIndicator.blockIcon, new Color(96f / 255f, 128f / 255f, 1, 1));
    }

}
