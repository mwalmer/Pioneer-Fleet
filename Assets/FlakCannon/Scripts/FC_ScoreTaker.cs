using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_ScoreTaker : MonoBehaviour
{
    public static FC_ScoreTaker scoreTaker;
    public UI_DynamicNumber dynamicNumber;
    protected Dictionary<string, int> scores;

    // Start is called before the first frame update
    void Start()
    {
        scores = new Dictionary<string, int>();
        scoreTaker = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void RegisterScore(string scoreType, int score)
    {
        //It will overwrite
        GetScores().Add(scoreType, score);
    }
    public static void AddScore(string scoreType, int score)
    {
        if (GetScores().ContainsKey(scoreType))
        {
            GetScores()[scoreType] += score;
        }
        else
        {
            GetScores().Add(scoreType, score);
        }

        if (scoreTaker.dynamicNumber)
        {
            scoreTaker.dynamicNumber.SetValue(GetTotalScore());
        }
    }

    public static Dictionary<string, int> GetScores()
    {
        return scoreTaker.scores;
    }

    public static int GetTotalScore()
    {
        int totalScore = 0;
        foreach (KeyValuePair<string, int> score in GetScores())
        {
            totalScore += score.Value;
        }
        return totalScore;
    }
}
