using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BlockMouseSelection : MonoBehaviour
{

    public SpriteRenderer block;
    private SpriteRenderer cell;
    // Start is called before the first frame update
    void Start()
    {
        cell = GetComponent<SpriteRenderer>();
        GetCellPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetCellPos()
    {
        Debug.Log(cell.size);
        float newX = transform.position.x - cell.size.x / 2;
        float newY = transform.position.y + cell.size.y / 2;
        Debug.Log(cell.size + " | " + newX + ", " + newY);

        return new Vector2(newX, newY);
    }

    public bool IsMouseOn()
    {
        Vector2 cellPos = GetCellPos();
        if (Input.mousePosition.x > cellPos.x && Input.mousePosition.x < cellPos.x + cell.size.x)
        {

        }


        return false;
    }

}
