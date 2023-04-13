using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.Board;

namespace MatchThree.Stage
{
    [System.Serializable]
    public class StageInfo
    {
        public int row;
        public int column;
        public int[] cells;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }


        public CellType GetCellType(int numRow, int numColumn)
        {
            int revisedRow = (row - 1) - numRow;
            if (cells.Length > revisedRow * column + numColumn)
                return (CellType)cells[revisedRow * column + numColumn];
            return CellType.EMPTY;
        }

        public bool DoValidation()
        {
            if (cells.Length != row * column)
                return false;
            return true;
        }
    }
}
