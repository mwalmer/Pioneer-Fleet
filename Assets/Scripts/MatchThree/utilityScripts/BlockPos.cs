using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Utilities
{
    public struct BlockPos
    {
        public int row { get; set; }
        public int column { get; set; }

        public BlockPos(int numRow = 0, int numColumn = 0)
        {
            row = numRow;
            column = numColumn;
        }

        public override bool Equals(object obj)
        {
            return obj is BlockPos pos && row == pos.row && column == pos.row;
        }

        public override int GetHashCode()
        {
            var hashCode = -928284752;
            hashCode = hashCode * -1521134295 + row.GetHashCode();
            hashCode = hashCode * -1521134295 + column.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"(row = {row}, col = {column})";
        }
    }
}