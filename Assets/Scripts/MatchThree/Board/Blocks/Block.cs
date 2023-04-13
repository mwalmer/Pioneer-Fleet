using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.Quest;

namespace MatchThree.Board
{
    public class Block
    {

        public BlockStatus status;
        public BlockQuestType questType;
        public MatchType match = MatchType.NONE;
        public short matchNum;


        BlockType objectBlockType;
        public BlockType type
        {
            get { return objectBlockType; }
            set { objectBlockType = value; }
        }

        protected BlockBreed thisBreed;  
        public BlockBreed breed
        {
            get { return thisBreed; }
            set
            {
                thisBreed = value;
                thisBlockBehaviour?.UpdateView(true);
            }
        }

        protected BlockBehaviour thisBlockBehaviour;
        public BlockBehaviour blockBehaviour
        {
            get { return thisBlockBehaviour; }
            set
            {
                thisBlockBehaviour = value;
                thisBlockBehaviour.SetBlock(this);
            }
        }

        public Transform blockObj { get { return thisBlockBehaviour?.transform; } }

        Vector2Int dupVec;  
        public int horzDuplicate
        {
            get { return dupVec.x; }
            set { dupVec.x = value; }
        }

        public int vertDuplicate
        {
            get { return dupVec.y; }
            set { dupVec.y = value; }
        }

        int durab;         
        public virtual int durability
        {
            get { return durab; }
            set { durab = value; }
        }

        protected BlockActionBehaviour thisBlockActionBehaviour;

        public bool isMoving
        {
            get
            {
                return blockObj != null && thisBlockActionBehaviour.isMoving;
            }
        }

        public Vector2 dropDistance
        {
            set
            {
                thisBlockActionBehaviour?.MoveDrop(value);
            }
        }

        public Block(BlockType blockType)
        {
            objectBlockType = blockType;

            status = BlockStatus.NORMAL;
            questType = BlockQuestType.CLEAR_SIMPLE;
            match = MatchType.NONE;
            thisBreed = BlockBreed.NA;

            durab = 1;
        }


        internal Block instBlock(GameObject blockPrefab, Transform containerObj)
        {
            if (IsValidate() == false)
                return null;

            GameObject newObj = Object.Instantiate(blockPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            newObj.transform.parent = containerObj;

            this.blockBehaviour = newObj.transform.GetComponent<BlockBehaviour>();
            thisBlockActionBehaviour = newObj.transform.GetComponent<BlockActionBehaviour>();

            return this;
        }

        public bool evalBoard(BoardNumber boardEnumerator, int nRow, int nCol)
        {
            Debug.Assert(boardEnumerator != null, $"({nRow},{nCol})");

            if (!IsEvaluatable())
                return false;

            //Condition of matching is met
            if (status == BlockStatus.MATCH)
            {
                if (questType == BlockQuestType.CLEAR_SIMPLE || boardEnumerator.IsCageTypeCell(nRow, nCol)) //TODO cagetype cell 조건이 필요한가? 
                {
                    durability--;
                }
                else //Block for special use
                {
                    return true;
                }

                if (durab == 0)
                {
                    status = BlockStatus.CLEAR;
                    return false;
                }
            }

            //set it to normal status
            status = BlockStatus.NORMAL;
            match = MatchType.NONE;
            matchNum = 0;

            return false;
        }


        public void updateBlockStatus(MatchType matchType, bool bAccumulate = true)
        {
            this.status = BlockStatus.MATCH;

            if (match == MatchType.NONE)
            {
                this.match = matchType;
            }
            else
            {
                this.match = bAccumulate ? match.Add(matchType) : matchType; //match + matchType
            }

            matchNum = (short)matchType;
        }


        internal void Move(float x, float y)
        {
            blockBehaviour.transform.position = new Vector3(x, y);
        }

        public void MoveTo(Vector3 to, float duration)
        {
            thisBlockBehaviour.StartCoroutine(Utilities.Action2D.MoveTo(blockObj, to, duration));
        }

        public virtual void Destroy()
        {
            blockBehaviour.aClear();
        }

        public bool IsValidate()
        {
            return type != BlockType.EMPTY;
        }

        public void ResetDuplicationInfo()
        {
            dupVec.x = 0;
            dupVec.y = 0;
        }

        public bool IsEqual(Block target)
        {
            if (IsMatchableBlock() && this.breed == target.breed)
                return true;

            return false;
        }


        public bool IsMatchableBlock()
        {
            return !(type == BlockType.EMPTY);
        }

        public bool IsSwipeable(Block baseBlock)
        {
            return true;
        }

        public bool IsEvaluatable()
        {
            if (status == BlockStatus.CLEAR || !IsMatchableBlock())
                return false;

            return true;
        }
    }
}