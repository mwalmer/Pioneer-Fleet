using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public int GridDimension = 8;
    public float Distance = 1.0f;
    private GameObject[,] Grid;
    void InitGrid()
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0); // 1
        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++) // 2
            {
                GameObject newTile = Instantiate(TilePrefab); // 3
                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>(); // 4
                renderer.sprite = Sprites[Random.Range(0, Sprites.Count)]; // 5
                newTile.transform.parent = transform; // 6
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset; // 7

                Grid[column, row] = newTile; // 8
            }
    }
    void Start()
    {
        Grid = new GameObject[GridDimension, GridDimension];
        InitGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
