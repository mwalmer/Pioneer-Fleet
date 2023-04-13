using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchThree.BConfig;

namespace MatchThree.Board
{
    public class BlockBehaviour : MonoBehaviour
    {
        Block thisBlock;
        SpriteRenderer thisSpriteRenderer;
        [SerializeField] BlockConfig thisBlockConfig;

        void Start()
        {
            thisSpriteRenderer = GetComponent<SpriteRenderer>();

            UpdateView(false);
        }

        internal void SetBlock(Block block)
        {
            thisBlock = block;
        }

        public void UpdateView(bool bValueChanged)
        {
            if (thisBlock.type == BlockType.EMPTY)
            {
                thisSpriteRenderer.sprite = null;
            }
            else if(thisBlock.type == BlockType.BASIC)
            {
                thisSpriteRenderer.sprite = thisBlockConfig.basicBlockSprites[(int)thisBlock.breed];
            }
        }

        public void aClear()
        {
            StartCoroutine(sExplosion(true));
        }


        IEnumerator sExplosion(bool bDestroy = true)
        {
            yield return Utilities.Action2D.Scale(transform, Constants.Multiplier.blockScale, 4f);

            GameObject explosionObj = thisBlockConfig.GetExplosionObject(BlockQuestType.CLEAR_SIMPLE);
            ParticleSystem.MainModule newModule = explosionObj.GetComponent<ParticleSystem>().main;
            newModule.startColor = thisBlockConfig.GetBlockColor(thisBlock.breed);

            explosionObj.SetActive(true);
            explosionObj.transform.position = this.transform.position;

            yield return new WaitForSeconds(0.1f);

            if (bDestroy)
                Destroy(gameObject);
            else
            {
                Debug.Assert(false, "error");
            }
        }

    }
}