using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Board
{
    public enum CellType
    {
        EMPTY = 0,      
        BASIC = 1,      
        FIXTURE = 2,   
        JELLY = 3,      
    }

    static class CellTypeMethod
    {
        //check if block can come in
        public static bool isBlockableCell(this CellType cellType)
        {
            return !(cellType == CellType.EMPTY);
        }

        //check if block is movable 
        public static bool isMovableCell(this CellType cellType)
        {
            return !(cellType == CellType.EMPTY);
        }
    }
}