using System;
using UnityEngine;
using MatchThree.Board;

namespace MatchThree.Stage
{
    public class StageBuilder
    {
        StageInfo thisStageInfo;
        int thisStage;

        public StageBuilder(int nStage)
        {
            thisStage = nStage;
        }


        //Make stage and cell and block
        public Stage ComposeStage()
        {
            Debug.Assert(thisStage > 0, $"error : {thisStage}");

            //Load stage
            thisStageInfo = LoadStage(thisStage);

            //make stage object
            Stage stage = new Stage(this, thisStageInfo.row, thisStageInfo.column);

            //Initialize Cell,Block
            for (int numRow = 0; numRow < thisStageInfo.row; numRow++)
            {
                for (int numColumn = 0; numColumn < thisStageInfo.column; numColumn++)
                {
                    stage.blocks[numRow, numColumn] = blockForStage(numRow, numColumn);
                    stage.cells[numRow, numColumn] = cellForStage(numRow, numColumn);
                }
            }

            return stage;
        }

        public StageInfo LoadStage(int nStage)
        {
            StageInfo stageInfo = StageReader.LoadStage(nStage);
            if (stageInfo != null)
            {
                Debug.Log(stageInfo.ToString());
            }

            return stageInfo;
        }

        Block blockForStage(int numRow, int numColumn)
        {
            if (thisStageInfo.GetCellType(numRow, numColumn) == CellType.EMPTY)
                return spEmptyBlock();

            return spBlock();
        }

        Cell cellForStage(int numRow, int numColumn)
        {
            Debug.Assert(thisStageInfo != null);
            Debug.Assert(numRow < thisStageInfo.row && numColumn < thisStageInfo.column);

            return CellProduce.SpawnCell(thisStageInfo, numRow, numColumn);
        }

        public static Stage BuildStage(int nStage)
        {
            StageBuilder stageBuilder = new StageBuilder(nStage);
            Stage stage = stageBuilder.ComposeStage();

            return stage;
        }

        public Block spBlock()
        {
            return BlockProducce.spBlock(BlockType.BASIC);
        }
        
        public Block spEmptyBlock()
        {
            Block newBlock = BlockProducce.spBlock(BlockType.EMPTY);

            return newBlock;
        }
    }
}