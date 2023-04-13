using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Board
{
    using BlockVectorKV = KeyValuePair<Block, Vector2Int>;

    public class BoardShuffler
    {
        Board objectBoard;
        bool loadingMode;
        
        SortedList<int, BlockVectorKV> OrgBlocks = new SortedList<int, BlockVectorKV>();
        IEnumerator<KeyValuePair<int, BlockVectorKV>> it;
        Queue<BlockVectorKV> UnusedBlocks = new Queue<BlockVectorKV>();
        bool bListComplete;

        public BoardShuffler(Board board, bool bLoadingMode)
        {
            objectBoard = board;
            loadingMode = bLoadingMode;
        }

        public void Shuffle(bool bAnimation = false)
        {
            //update matching information before shuffle
            PrepareDuplicationDatas();
            //store blocks that need to be shuffled
            PrepareShuffleBlocks();
            RunShuffle(bAnimation);
        }

        BlockVectorKV NextBlock(bool bUseQueue)
        {
            if (bUseQueue && UnusedBlocks.Count > 0)
                return UnusedBlocks.Dequeue();

            if (!bListComplete && it.MoveNext())
                return it.Current.Value;

            bListComplete = true;

            return new BlockVectorKV(null, Vector2Int.zero);
        }
        
        void PrepareDuplicationDatas()
        {
            for (int numRow = 0; numRow < objectBoard.maxRow; numRow++)
                for (int numColumn = 0; numColumn < objectBoard.maxCol; numColumn++)
                {
                    Block block = objectBoard.blocks[numRow, numColumn];

                    if (block == null)
                        continue;

                    if (objectBoard.CanShuffle(numRow, numColumn, loadingMode))
                        block.ResetDuplicationInfo();
                    else
                    {
                        block.horzDuplicate = 1;
                        block.vertDuplicate = 1;
                        if (numColumn > 0 && !objectBoard.CanShuffle(numRow, numColumn - 1, loadingMode) && objectBoard.blocks[numRow, numColumn - 1].IsSafeEqual(block))
                        {
                            block.horzDuplicate = 2;
                            objectBoard.blocks[numRow, numColumn - 1].horzDuplicate = 2;
                        }

                        if (numRow > 0 && !objectBoard.CanShuffle(numRow - 1, numColumn, loadingMode) && objectBoard.blocks[numRow - 1, numColumn].IsSafeEqual(block))
                        {
                            block.vertDuplicate = 2;
                            objectBoard.blocks[numRow - 1, numColumn].vertDuplicate = 2;
                        }
                    }
                }
        }

        void PrepareShuffleBlocks()
        {
            for (int numRow = 0; numRow < objectBoard.maxRow; numRow++)
                for (int numColumn = 0; numColumn < objectBoard.maxCol; numColumn++)
                {
                    if (!objectBoard.CanShuffle(numRow, numColumn, loadingMode))
                        continue;
                    while (true)
                    {
                        int nRandom = UnityEngine.Random.Range(0, 10000);
                        //detect key duplication
                        if (OrgBlocks.ContainsKey(nRandom))
                            continue;

                        OrgBlocks.Add(nRandom, new BlockVectorKV(objectBoard.blocks[numRow, numColumn], new Vector2Int(numColumn, numRow)));
                        break;
                    }
                }

            it = OrgBlocks.GetEnumerator();
        }


        //Shuffle all the blocks
        void RunShuffle(bool bAnimation)
        {
            for (int numRow = 0; numRow < objectBoard.maxRow; numRow++)
            {
                for (int numColumn = 0; numColumn < objectBoard.maxCol; numColumn++)
                {
                    if (!objectBoard.CanShuffle(numRow, numColumn, loadingMode))
                        continue;

                    objectBoard.blocks[numRow, numColumn] = GetShuffledBlock(numRow, numColumn);
                }
            } 
        } 
        
        Block GetShuffledBlock(int numRow, int numColumn)
        {
            BlockBreed prevBreed = BlockBreed.NA;  
            Block firstBlock = null;               

            bool bUseQueue = true;  
            while (true)
            {
                BlockVectorKV blockInfo = NextBlock(bUseQueue);
                Block block = blockInfo.Key;
                if (block == null)
                {
                    blockInfo = NextBlock(true);
                    block = blockInfo.Key;
                }

                //Debug.Assert(block != null, $"block can't be null : queue  count -> {UnusedBlocks.Count}");

                if (prevBreed == BlockBreed.NA) 
                    prevBreed = block.breed;

                if (bListComplete)
                {
                    if (firstBlock == null)
                    {
                        firstBlock = block;  
                    }
                    else if (System.Object.ReferenceEquals(firstBlock, block))
                    {
                        objectBoard.ChangeBlock(block, prevBreed);
                    }
                }
                Vector2Int vtDup = CalcDuplications(numRow, numColumn, block);

                if (vtDup.x > 2 || vtDup.y > 2)
                {
                    UnusedBlocks.Enqueue(blockInfo);
                    bUseQueue = bListComplete || !bUseQueue;

                    continue;
                }

                block.vertDuplicate = vtDup.y;
                block.horzDuplicate = vtDup.x;
                if (block.blockObj != null)
                {
                    float initX = objectBoard.CalcInitX(Constants.Multiplier.blockOrg);
                    float initY = objectBoard.CalcInitY(Constants.Multiplier.blockOrg);
                    block.Move(initX + numColumn, initY + numRow);
                }

                //return block found
                return block;
            }
        }
        
        Vector2Int CalcDuplications(int numRow, int numColumn, Block block)
        {
            int dupColumn = 1, dupRow = 1;

            if (numColumn > 0 && objectBoard.blocks[numRow, numColumn - 1].IsSafeEqual(block))
                dupColumn += objectBoard.blocks[numRow, numColumn - 1].horzDuplicate;

            if (numRow > 0 && objectBoard.blocks[numRow - 1, numColumn].IsSafeEqual(block))
                dupRow += objectBoard.blocks[numRow - 1, numColumn].vertDuplicate;

            if (numColumn < objectBoard.maxCol - 1 && objectBoard.blocks[numRow, numColumn + 1].IsSafeEqual(block))
            {
                Block rightBlock = objectBoard.blocks[numRow, numColumn + 1];
                dupColumn += rightBlock.horzDuplicate;

                if (rightBlock.horzDuplicate == 1)
                    rightBlock.horzDuplicate = 2;
            }

            if (numRow < objectBoard.maxRow - 1 && objectBoard.blocks[numRow + 1, numColumn].IsSafeEqual(block))
            {
                Block upperBlock = objectBoard.blocks[numRow + 1, numColumn];
                dupRow += upperBlock.vertDuplicate;

                if (upperBlock.vertDuplicate == 1)
                    upperBlock.vertDuplicate = 2;
            }

            return new Vector2Int(dupColumn, dupRow);
        }
    }
}
