using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static Tile selected;  
    private SpriteRenderer Renderer; 
    public Vector2Int Position;

    private void Start() 
    {
        //Debug.Log("Hello World!");
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        Renderer.color = Color.grey;
    }

    public void Unselect() 
    {
        Renderer.color = Color.white;
    }



    private void OnMouseDown()
    {
        if (selected != null)
        {
            Debug.Log("Hello World!1");
            if (selected == this)
                return;
            selected.Unselect();
            if (Vector2Int.Distance(selected.Position, Position) == 0)
            {
                Debug.Log("Hello World! Distance 0");
            }
                if (Vector2Int.Distance(selected.Position, Position) == 1)
            {
                Debug.Log("Hello World!2");
                GridManager.Instance.SwapTiles(Position, selected.Position);
                selected = null;
            }
            else
            {
                Debug.Log("Hello World!3");
                selected = this;
                Select();
            }
        }
        else
        {
            selected = this;
            Select();
        }
    }

}
