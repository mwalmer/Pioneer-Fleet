using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.Utilities;
using UnityEngine.UI;
using TMPro;


namespace MatchThree.Stage
{
    public class StageController : MonoBehaviour
    {

        bool thisbInit;
        Stage thisStage;
        StageController thisstageCtr;
        public TextMeshProUGUI scoreCountUI;
        public int matchScore = 0;
        public int finalScore = 0;
        //public float timeLimit = 60;
        InputManager thisInputManager;
        ActionManager thisActionManager;

        //Event Members
        bool thisbTouchDown;         
        BlockPos thisBlockDownPos;   
        Vector3 thisClickPos;        

        [SerializeField] Transform thisContainer;
        [SerializeField] GameObject thisCellPrefab;
        [SerializeField] GameObject thisBlockPrefab;

        void Start()
        {
            InitStage();
        }

        private void Update()
        {
            updateScore();
            if (!thisbInit)
                return;

            OnInputHandler();
            
        }

        void InitStage()
        {
            if (thisbInit)
                return;

            thisbInit = true;
            thisInputManager = new InputManager(thisContainer);

            BuildStage();
            /*
            if (gameTimer)
            {
                gameTimer.RunCountDown(timeLimit);
            }
            */

        }


        //Make a stage
        void BuildStage()
        {
            //make stage
            thisStage = StageBuilder.BuildStage(nStage : 1);
            thisActionManager = new ActionManager(thisContainer, thisStage);

            //make scene
            thisStage.ComposeStage(thisCellPrefab, thisBlockPrefab, thisContainer);
        }
        void AwardPoints()
        {
            matchScore += 10;
        }
        public int returnScore()
        {
            return thisActionManager.score;
        }
        void updateScore()
        {
            scoreCountUI.text = "" + thisActionManager.score;
        }
        void OnInputHandler()
        {
            if (!thisbTouchDown && thisInputManager.isTouchDown)
            {
                //get coordiniates of board.
                Vector2 point = thisInputManager.touch2BoardPosition;

                //Check if clicked correctly
                if (!thisStage.IsInsideBoard(point))
                    return;

                BlockPos blockPos;
                if (thisStage.validBlock(point, out blockPos))
                {
                    thisbTouchDown = true;       
                    thisBlockDownPos = blockPos;  
                    thisClickPos = point;        
                }
            }
            
            else if (thisbTouchDown && thisInputManager.isTouchUp)
            {
                Vector2 point = thisInputManager.touch2BoardPosition;
                //Get direction of swipe
                Swipe swipeDir = thisInputManager.evalSwapDirection(thisClickPos, point);

                if (swipeDir != Swipe.NA)
                    thisActionManager.DoSwipeAction(thisBlockDownPos.row, thisBlockDownPos.column, swipeDir);

                thisbTouchDown = false;   
            }
        }

    }
}
