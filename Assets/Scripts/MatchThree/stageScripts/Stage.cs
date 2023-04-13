using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.Board;
using MatchThree.Utilities;
using System;

namespace MatchThree.Stage
{
    public class Stage
    {
        public int maxRow { get { return thisBoard.maxRow; } }
        public int maxCol { get { return thisBoard.maxCol; } }
        MatchThree.Board.Board thisBoard;
        public MatchThree.Board.Board board { get { return thisBoard; } }
        StageBuilder thisStageBuilder;
        public Block[,] blocks { get { return thisBoard.blocks; } }
        public Cell[,] cells { get { return thisBoard.cells; } }
        public static int matchScore { get; internal set; }
        /// <summary>
        /// Make Board

        public Stage(StageBuilder stageBuilder, int numRow, int numColumn)
        {
            thisStageBuilder = stageBuilder;
            thisBoard = new MatchThree.Board.Board(numRow, numColumn);
        }

        /// Make board with Cell/Block Prefab, container

        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container)
        {
            thisBoard.ComposeStage(cellPrefab, blockPrefab, container, thisStageBuilder);
        }

        //Swipe checks if the swipe leads to match or not
        public IEnumerator swipeAction(int numRow, int numColumn, Swipe swipeDir, Returnable<bool> actionResult)
        {
            actionResult.value = false; 
            int numSwipeRow = numRow, numSwipeColumn = numColumn;
            numSwipeRow += swipeDir.getRow(); //Right : +1, LEFT : -1
            numSwipeColumn += swipeDir.getColumn(); //UP : +1, DOWN : -1
            //check if swipable
            if (thisBoard.checkSwipe(numSwipeRow, numSwipeColumn))
            {
                //save the position of blocks that will be swiped
                Block targetBlock = blocks[numSwipeRow, numSwipeColumn];
                Block baseBlock = blocks[numRow, numColumn];
                Debug.Assert(baseBlock != null && targetBlock != null);

                Vector3 basePos = baseBlock.blockObj.transform.position;
                Vector3 targetPos = targetBlock.blockObj.transform.position;

                //Swipe action
                if (targetBlock.IsSwipeable(baseBlock))
                {
                    //Revert back after swipe is unsuccessful
                    baseBlock.MoveTo(targetPos, Constants.Multiplier.swipeTime);
                    targetBlock.MoveTo(basePos, Constants.Multiplier.swipeTime);

                    yield return new WaitForSeconds(Constants.Multiplier.swipeTime);

                    //exchange block
                    blocks[numRow, numColumn] = targetBlock;
                    blocks[numSwipeRow, numSwipeColumn] = baseBlock;

                    actionResult.value = true;
                }
            }

            yield break;
        }

        public IEnumerator Evaluate(Returnable<bool> matchResult)
        {
            yield return thisBoard.Evaluate(matchResult);
        }

        //Logic to handle matched blocks
        public IEnumerator PostprocessAfterEvaluate()
        {
            List<KeyValuePair<int, int>> unfilledBlocks = new List<KeyValuePair<int, int>>();
            List<Block> movingBlocks = new List<Block>();
            yield return thisBoard.ArrangeBlocksAfterClean(unfilledBlocks, movingBlocks);
            yield return thisBoard.SpawnBlocksAfterClean(movingBlocks);
            yield return waitDrop(movingBlocks);
        }

        public IEnumerator waitDrop(List<Block> movingBlocks)
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(0.05f); 

            while (true)
            {
                bool bContinue = false;
                for (int i = 0; i < movingBlocks.Count; i++)
                {
                    if (movingBlocks[i].isMoving)
                    {
                        bContinue = true;
                        break;
                    }
                }

                if (!bContinue)
                    break;

                yield return waitForSecond;
            }

            movingBlocks.Clear();
            yield break;
        }

        #region Simple Methods

        public bool IsInsideBoard(Vector2 ptOrg)
        {
            Vector2 point = new Vector2(ptOrg.x + (maxCol / 2.0f), ptOrg.y + (maxRow / 2.0f));
            if (point.y < 0 || point.x < 0 || point.y > maxRow || point.x > maxCol)
                return false;
            return true;
        }


        public bool validBlock(Vector2 point, out BlockPos blockPos)
        {
            //Converting coordinates into pos
            Vector2 pos = new Vector2(point.x + (maxCol/ 2.0f), point.y + (maxRow / 2.0f));
            int numRow = (int)pos.y;
            int numColumn = (int)pos.x;
            //Blocks to return
            blockPos = new BlockPos(numRow, numColumn);
            //Check swipeability
            return board.checkSwipe(numRow, numColumn);
        }


        /**
         * Check if swipe is valid at current pos
         */
        public bool validSwipe(int numRow, int numColumn, Swipe swipeDir)
        {
            switch (swipeDir)
            {
                case Swipe.DOWN: return numRow > 0; ;
                case Swipe.UP: return numRow < maxRow - 1;
                case Swipe.LEFT: return numColumn > 0;
                case Swipe.RIGHT: return numColumn < maxCol - 1;
                default:
                    return false;
            }
        }
        #endregion

        public void PrintAll()
        {
            System.Text.StringBuilder strCells = new System.Text.StringBuilder();
            System.Text.StringBuilder strBlocks = new System.Text.StringBuilder();

            for (int numRow = maxRow -1; numRow >= 0; numRow--)
            {
                for (int numColumn = 0; numColumn < maxCol; numColumn++)
                {
                    strCells.Append($"{cells[numRow, numColumn].type}, ");
                    strBlocks.Append($"{blocks[numRow, numColumn].breed}, ");
                }

                strCells.Append("\n");
                strBlocks.Append("\n");
            }
        }
    }
}