using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Board
{
    public enum BlockType
    {
        EMPTY = 0,
        BASIC = 1
    }

    public enum BlockBreed
    {
        NA      = -1,   
        BREED_0 = 0,
        BREED_1 = 1,
        BREED_2 = 2,
        BREED_3 = 3,
        BREED_4 = 4,
        BREED_5 = 5,
    }

    public enum BlockStatus
    {
        NORMAL,                
        MATCH,                
        CLEAR                   
    }

    public enum BlockQuestType 
    {
        NONE = -1,
        CLEAR_SIMPLE = 0,       
        CLEAR_HORZ = 1,        
        CLEAR_VERT = 2,         
        CLEAR_CIRCLE = 3,       
        CLEAR_LAZER = 4,        
        CLEAR_HORZ_BUFF = 5,   
        CLEAR_VERT_BUFF = 6,      
        CLEAR_CIRCLE_BUFF = 7, 
        CLEAR_LAZER_BUFF = 8   
    }


    static class BlockMethod
    {
        public static bool IsSafeEqual(this Block block, Block targetBlock)
        {
            if (block == null)
                return false;

            return block.IsEqual(targetBlock);
        }
    }
}