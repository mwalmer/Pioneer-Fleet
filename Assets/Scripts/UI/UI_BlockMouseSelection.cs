using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BlockMouseSelection : MonoBehaviour
{

    public SpriteRenderer block;
    private SpriteRenderer cell;
    private bool isPressed = false;
    private bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
        cell = GetComponent<SpriteRenderer>();
        GetCellPos();
    }

    // Update is called once per frame
    void Update()
    {
        IsMouseOn();
        PressEvent();
    }

    public Vector2 GetCellPos()
    {
        Debug.Log(cell.size);
        float newX = transform.position.x - cell.size.x / 2;
        float newY = transform.position.y + cell.size.y / 2;

        return new Vector2(newX, newY);
    }

    public bool IsMouseOn()
    {
        Vector2 cellPos = GetCellPos();
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (isPressed == false)
        {
            if (mousePos.x > cellPos.x && mousePos.x < cellPos.x + cell.size.x && mousePos.y < cellPos.y && mousePos.y > cellPos.y - cell.size.y)
            {
                block.color = new Color(1, 1, 1, 1);
                isOn = true;
            }
            else
            {
                block.color = new Color(1, 1, 1, 0.001f);
                isOn = false;
            }
        }

        return false;
    }
    public void PressEvent()
    {
        if (isOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isPressed = true;
                block.color = new Color(64f / 255f, 128f / 255f, 1, 1);
            }
        }
        if (isPressed)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                isPressed = false;
                block.color = new Color(1, 1, 1, 1);
            }
        }
    }

}
