using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.Board;

namespace MatchThree.Board
{
    public class BoardNumber
    {
        MatchThree.Board.Board m_Board;

        public BoardNumber(MatchThree.Board.Board board)
        {
            this.m_Board = board;
        }

        public bool IsCageTypeCell(int nRow, int nCol)
        {
            return false;
        }
    }
}