using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchThree.Board
{
    public class Cell
    {
        // Member variable, Property
        protected CellType m_CellType;
        public CellType type
        {
            get { return m_CellType; }
            set { m_CellType = value; }
        }

        protected CellBehaviour m_CellBehaviour;
        public CellBehaviour cellBehaviour
        {
            get { return m_CellBehaviour; }
            set
            {
                m_CellBehaviour = value;
                m_CellBehaviour.SetCell(this);
            }
        }

        // Constructor
        public Cell(CellType cellType)
        {
            m_CellType = cellType;
        }

        public Cell InstantiateCellObj(GameObject cellPrefab, Transform containerObj)
        {
            //make Cell object
            GameObject newObj = Object.Instantiate(cellPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //include cell in container
            newObj.transform.parent = containerObj;

            //store cellbehviour
            this.cellBehaviour = newObj.transform.GetComponent<CellBehaviour>();

            return this;
        }


        public void Move(float x, float y)
        {
            cellBehaviour.transform.position = new Vector3(x, y);
        }

        public bool isCell()
        {
            return type == CellType.EMPTY;
        }
    }
}

