using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_ScoreTaker : MonoBehaviour
{
    public static FC_ScoreTaker scoreTaker;
    private Dictionary<string, int> scores;

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

    public void RegisterScore(string scoreType, int score)
    {
        //It will overwrite
        scores.Add(scoreType, score);
    }
    public void AddScore(string scoreType, int score)
    {
        if (scores.ContainsKey(scoreType))
        {
            scores[scoreType] += score;
        }
        else
        {
            scores.Add(scoreType, score);
        }
    }

    public Dictionary<string, int> GetScores()
    {
        return scores;
    }

    public int GetTotalScore()
    {
        int totalScore = 0;
        foreach (KeyValuePair<string, int> score in scores)
        {
            totalScore += score.Value;
        }
        return totalScore;
    }
}
