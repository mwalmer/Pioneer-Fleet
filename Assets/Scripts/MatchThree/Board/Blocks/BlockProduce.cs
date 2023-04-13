using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Board
{
    public static class BlockProducce
    {
        public static Block spBlock(BlockType blockType)
        {
            Block block = new Block(blockType);

            //Set Breed
            if(blockType == BlockType.BASIC)
                block.breed = (BlockBreed)UnityEngine.Random.Range(0, 6);
            else if(blockType == BlockType.EMPTY)
                block.breed = BlockBreed.NA;

            return block;
        }
    }
}