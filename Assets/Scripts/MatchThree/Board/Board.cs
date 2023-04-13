using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.Quest;
using MatchThree.Utilities;
using MatchThree.Stage;

namespace MatchThree.Board
{
    using IntIntKV = KeyValuePair<int, int>;

    public class Board
    {
        int thisNumRow;
        int thisNumColumn;

        public int maxRow { get { return thisNumRow; } }
        public int maxCol { get { return thisNumColumn; } }

        Cell[,] thisCells;
        public Cell[,] cells { get { return thisCells; } }

        Block[,] thisBlocks;
        public Block[,] blocks { get { return thisBlocks; } }

        Transform thisContainer;
        GameObject thisCellPrefab;
        GameObject thisBlockPrefab;
        StageBuilder thisStageBuilder;

        BoardNumber m_Enumerator;

        public Board(int numRow, int numColumn)
        {
            thisNumRow = numRow;
            thisNumColumn = numColumn;

            thisCells = new Cell[numRow, numColumn];
            thisBlocks = new Block[numRow, numColumn];

            m_Enumerator = new BoardNumber(this);
        }

        internal void ComposeStage(GameObject cellPrefab, GameObject blockPrefab, Transform container, StageBuilder stageBuilder)
        {
            //Save Cell,Block, Container(Board) to make a stage. 
            thisCellPrefab = cellPrefab;
            thisBlockPrefab = blockPrefab;
            thisContainer = container;
            thisStageBuilder = stageBuilder;

            //Mix blocks with no match three  
            BoardShuffler shuffler = new BoardShuffler(this, true);
            shuffler.Shuffle();

            //Add Cell/Block GameObject to Board using Cell, Block Prefab
            float initX = CalcInitX(0.5f);
            float initY = CalcInitY(0.5f);
            for (int numRow = 0; numRow < thisNumRow; numRow++)
                for (int numColumn = 0; numColumn < thisNumColumn; numColumn++)
                {
                    //Make cell GameObject, otherwise return null
                    Cell cell = thisCells[numRow, numColumn]?.InstantiateCellObj(cellPrefab, container);
                    cell?.Move(initX + numColumn, initY + numRow);

                    //Make block gameObject, otherwise return null
                    Block block = thisBlocks[numRow, numColumn]?.instBlock(blockPrefab, container);
                    block?.Move(initX + numColumn, initY + numRow);
                }
        }

        //Remove Block matched
        public IEnumerator Evaluate(Returnable<bool> matchResult)
        {
            //If there is match three, return true
            bool bMatchedBlockFound = updateMatch();

            //If there is no match three blocks
            if(bMatchedBlockFound == false)
            {
                matchResult.value = false;
                yield break;
            }

            //If there are match three blocks

            for (int numRow = 0; numRow < thisNumRow; numRow++)
                for (int numColumn = 0; numColumn < thisNumColumn; numColumn++)
                {
                    Block block = thisBlocks[numRow, numColumn];

                    block?.evalBoard(m_Enumerator, numRow, numColumn);
                }
                

            List<Block> clearBlocks = new List<Block>();

            for (int numRow = 0; numRow < thisNumRow; numRow++)
            {
                for (int numColumn = 0; numColumn < thisNumColumn; numColumn++)
                {
                    Block block = thisBlocks[numRow, numColumn];

                    if (block != null)
                    {
                        if (block.status == BlockStatus.CLEAR)
                        {
                            clearBlocks.Add(block);

                            thisBlocks[numRow, numColumn] = null;   
                        }
                    }
                }
            }

            //Remove block matched
            clearBlocks.ForEach((block) => block.Destroy());
            yield return new WaitForSeconds(0.2f);

            //if there are block matched, return true  
            matchResult.value = true;

            yield break;
        }

        public bool updateMatch()
        {
            List<Block> matchedBlockList = new List<Block>();
            int nCount = 0;
            for (int numRow = 0; numRow < thisNumRow; numRow++)
            {
                for (int numColumn = 0; numColumn < thisNumColumn; numColumn++)
                {
                    if (EvalBlocksIfMatched(numRow, numColumn, matchedBlockList))
                    {
                        nCount++;
                    }
                }
            }
            return nCount > 0;
        }

        public bool EvalBlocksIfMatched(int numRow, int numColumn, List<Block> matchedBlockList)
        {
            bool bFound = false;

            Block baseBlock = thisBlocks[numRow, numColumn];
            if (baseBlock == null)
                return false;

            if (baseBlock.match != MatchThree.Quest.MatchType.NONE || !baseBlock.IsValidate() || thisCells[numRow, numColumn].isCell())
                return false;

            matchedBlockList.Add(baseBlock);
            Block block;

            //Check if right block is matched
            for (int i = numColumn + 1; i < thisNumColumn; i++)
            {
                block = thisBlocks[numRow, i];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Add(block);
            }

            //Check if left block is matched
            for (int i = numColumn - 1; i >= 0; i--)
            {
                block = thisBlocks[numRow, i];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Insert(0, block);
            }

            if (matchedBlockList.Count >= 3)
            {
                SetBlockStatusMatched(matchedBlockList, true);
                bFound = true;
            }
            matchedBlockList.Clear();

            //Check vertical
            matchedBlockList.Add(baseBlock);

            //Check if upper block is matched
            for (int i = numRow + 1; i < thisNumRow; i++)
            {
                block = thisBlocks[i, numColumn];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Add(block);
            }

            //Check if lower block is matched
            for (int i = numRow - 1; i >= 0; i--)
            {
                block = thisBlocks[i, numColumn];
                if (!block.IsSafeEqual(baseBlock))
                    break;

                matchedBlockList.Insert(0, block);
            }
            //check if matched
            if (matchedBlockList.Count >= 3)
            {
                SetBlockStatusMatched(matchedBlockList, false);
                bFound = true;
            }
            matchedBlockList.Clear();
            return bFound;
        }


        void SetBlockStatusMatched(List<Block> blockList, bool bHorz)
        {
            int nMatchCount = blockList.Count;
            blockList.ForEach(block => block.updateBlockStatus((MatchType)nMatchCount));
        }

        public IEnumerator ArrangeBlocksAfterClean(List<IntIntKV> unfilledBlocks, List<Block> movingBlocks)
        {
            SortedList<int, int> emptyBlocks = new SortedList<int, int>();
            List<IntIntKV> emptyRemainBlocks = new List<IntIntKV>();

            for (int numColumn = 0; numColumn < thisNumColumn; numColumn++)
            {
                emptyBlocks.Clear();

                for (int numRow = 0; numRow < thisNumRow; numRow++)
                {
                    if (CanBlockBeAllocatable(numRow, numColumn))
                        emptyBlocks.Add(numRow, numRow);
                }
                if (emptyBlocks.Count == 0)
                    continue;
                IntIntKV first = emptyBlocks.First();
                for (int numRow = first.Value + 1; numRow < thisNumRow; numRow++)
                {
                    Block block = thisBlocks[numRow, numColumn];

                    if (block == null || thisCells[numRow, numColumn].type == CellType.EMPTY)
                        continue;

                    //Blocks that need to move
                    block.dropDistance = new Vector2(0, numRow - first.Value);    
                    movingBlocks.Add(block);

                    //Move to empty space
                    Debug.Assert(thisCells[first.Value, numColumn].isCell() == false, $"{thisCells[first.Value, numColumn]}");
                    thisBlocks[first.Value, numColumn] = block;

                    //Empty current space 
                    thisBlocks[numRow, numColumn] = null;
                    emptyBlocks.RemoveAt(0);
                    emptyBlocks.Add(numRow, numRow);
                    first = emptyBlocks.First();
                    numRow = first.Value; 
                }
            }

            yield return null;

            if (emptyRemainBlocks.Count > 0)
            {
                unfilledBlocks.AddRange(emptyRemainBlocks);
            }

            yield break;
        }

        public IEnumerator SpawnBlocksAfterClean(List<Block> movingBlocks)
        {
            for (int numColumn = 0; numColumn < thisNumColumn; numColumn++)
            {
                for (int numRow = 0; numRow < thisNumRow; numRow++)
                {
                    if (thisBlocks[numRow, numColumn] == null)
                    {
                        int nTopRow = numRow;
                        int nSpawnBaseY = 0;

                        for (int y = nTopRow; y < thisNumRow; y++)
                        {
                            if (thisBlocks[y, numColumn] != null || !CanBlockBeAllocatable(y, numColumn))
                                continue;

                            Block block = SpawnBlockWithDrop(y, numColumn, nSpawnBaseY, numColumn);
                            if (block != null)
                                movingBlocks.Add(block);

                            nSpawnBaseY++;
                        }

                        break;
                    }
                }
            }

            yield return null;
        }

        Block SpawnBlockWithDrop(int numRow, int numColumn, int nSpawnedRow, int nSpawnedCol)
        {
            float fInitX = CalcInitX(Constants.Multiplier.blockOrg);
            float fInitY = CalcInitY(Constants.Multiplier.blockOrg) + thisNumRow;

            Block block = thisStageBuilder.spBlock().instBlock(thisBlockPrefab, thisContainer);
            if (block != null)
            {
                thisBlocks[numRow, numColumn] = block;
                block.Move(fInitX + (float)nSpawnedCol, fInitY + (float)(nSpawnedRow));
                block.dropDistance = new Vector2(nSpawnedCol - numColumn, thisNumRow + (nSpawnedRow - numRow));
            }

            return block;
        }

        public float CalcInitX(float offset = 0)
        {
            return -thisNumColumn / 2.0f + offset;   
        }

        public float CalcInitY(float offset = 0)
        {
            return -thisNumRow / 2.0f + offset;
        }

        //Check if the place can shuffle
        public bool CanShuffle(int numRow, int numColumn, bool bLoading)
        {
            if (!thisCells[numRow, numColumn].type.isMovableCell())
                return false;

            return true;
        }


        public void ChangeBlock(Block block, BlockBreed notAllowedBreed)
        {
            BlockBreed genBreed;

            while (true)
            {
                genBreed = (BlockBreed)UnityEngine.Random.Range(0, 6); 

                if (notAllowedBreed == genBreed)
                    continue;

                break;
            }

            block.breed = genBreed;
        }

        public bool checkSwipe(int numRow, int numColumn)
        {
            return thisCells[numRow, numColumn].type.isMovableCell();
        }

        //Check if block can come in
        bool CanBlockBeAllocatable(int numRow, int numColumn)
        {
            if (!thisCells[numRow, numColumn].type.isBlockableCell())
                return false;

            return thisBlocks[numRow, numColumn] == null;
        }
    }
}