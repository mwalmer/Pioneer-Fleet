using System.Collections;
using System.Collections.Generic;
using MatchThree.Board;
using UnityEngine;

namespace MatchThree.Board
{
    public class CellBehaviour : MonoBehaviour
    {
        Cell thisCell;
        SpriteRenderer thisSpriteRenderer;

        // Start is called before the first frame update
        void Start()
        {
            thisSpriteRenderer = GetComponent<SpriteRenderer>();

            UpdateView(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCell(Cell cell)
        {
            thisCell = cell;
        }

        public void UpdateView(bool bValueChanged)
        {
            if (thisCell.type == CellType.EMPTY)
            {
                thisSpriteRenderer.sprite = null;
            }
        }

    }
}