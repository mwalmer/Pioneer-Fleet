using System.Collections;
using System.Collections.Generic;
using MatchThree.Utilities;
using UnityEngine;
using MatchThree.Stage;
using TMPro;

namespace MatchThree.Stage
{
    public class ActionManager 
    {
        Transform thisContainer;          //Board GameObject
        Stage thisStage;
        StageController thisstageCtr;
        MonoBehaviour thisMonoBehaviour;  
        public TextMeshProUGUI scoreCountUI;
        public int score = 0;

        bool bRunning;                //action in play mode
        
        public ActionManager(Transform container, Stage stage)
        {
            thisContainer = container;
            thisStage = stage;
            //thisstageCtr = stageCtr;

            thisMonoBehaviour = container.gameObject.GetComponent<MonoBehaviour>();
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return thisMonoBehaviour.StartCoroutine(routine);
        }

        public int ReturnScore()
        {
            return score;
        }

        public void DoSwipeAction(int nRow, int numColumn, Swipe swipeDirection)
        {
            if (thisStage.validSwipe(nRow, numColumn, swipeDirection))
            {
                StartCoroutine(swipeAction(nRow, numColumn, swipeDirection));
            }
        }


        //swipe routing
        IEnumerator swipeAction(int numRow, int numColumn, Swipe swipeDir)
        {
            if (!bRunning) 
            {
                bRunning = true;  

                //SoundManager.instance.PlayOneShot(Clip.clear);

                //swipe 
                Returnable<bool> bSwipedBlock = new Returnable<bool>(false);
                yield return thisStage.swipeAction(numRow, numColumn, swipeDir, bSwipedBlock);
                //if swipe successful, evaluate board
                if (bSwipedBlock.value)
                {
                    Returnable<bool> bMatchBlock = new Returnable<bool>(false);
                    yield return EvaluateBoard(bMatchBlock);
                    //Revert back if no match
                    if (!bMatchBlock.value)
                    {
                        yield return thisStage.swipeAction(numRow, numColumn, swipeDir, bSwipedBlock);
                    }
                }
                bRunning = false; 
            }
            yield break;
        }

        IEnumerator EvaluateBoard(Returnable<bool> matchResult)
        {
            while (true)    //keep going if there were matched block
            {
                //Remove Matched Block
                Returnable<bool> bBlockMatched = new Returnable<bool>(false);
                yield return StartCoroutine(thisStage.Evaluate(bBlockMatched));

                //There is match three
                if (bBlockMatched.value)
                {
                    score += 5;
                    matchResult.value = true;

                    SoundManager.instance.PlayOneShot(Clip.shot);

                    // produce block if blocks removed as a result of match
                    yield return StartCoroutine(thisStage.PostprocessAfterEvaluate());
                }
                //no more match, then stop
                else
                    break;  
            }

            yield break;
        }

    }
}