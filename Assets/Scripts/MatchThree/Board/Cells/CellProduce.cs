using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Board
{
    public static class CellProduce
    {
        public static Cell SpawnCell(Stage.StageInfo stageInfo, int numRow, int numColumn)
        {
            Debug.Assert(stageInfo != null);
            Debug.Assert(numRow < stageInfo.row && numColumn < stageInfo.column);

            return SpawnCell(stageInfo.GetCellType(numRow, numColumn));
        }

        public static Cell SpawnCell(CellType cellType)
        {
            return new Cell(cellType);
        }
    }
}
