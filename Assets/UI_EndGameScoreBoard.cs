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



    private CanvasGroup group;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreRequirement.text = "/ " + FC_GameManager.scoreFor100;
        dynamicNumber.SetValue(FC_ScoreTaker.GetTotalScore());
    }
}
