using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] float tileSize = 4.0f;

    private int chunkColumns = 11;
    private int chunkRows = 20;

    private List<List<Tile>> tiles = new List<List<Tile>>();

    private void Start()
    {
        for (int column = 0; column < chunkColumns; column++)
        {
            tiles.Add(new List<Tile>());
        }

        CreateChunk();

    }

    private void CreateChunk()
    {
        Maze.CellType[,] maze = Maze.Generate(chunkColumns, chunkRows);

        for (int row = 0; row < chunkRows; row++)
        {
            for (int column = 0; column < chunkColumns; column++)
            {
                Tile tile = CreateTile(column, row);
                tiles[column].Add(tile);
                if (maze[column, row] == Maze.CellType.Wall) tile.isWall = true;
            }
        }

        for (int row = 0; row < chunkRows; row++)
        {
            for (int column = 0; column < chunkColumns; column++)
            {
                Tile tile = tiles[column][row];
                tile.Autotile();
            }
        }

    }

    private Tile CreateTile(int column, int row)
    {
        float x  = column * tileSize;
        float z  = row * tileSize;

        Tile tile = Instantiate(tilePrefab, transform).GetComponent<Tile>();
        tile.transform.position = new Vector3(x, 0, z);
        tile.name = $"Tile {column}-{row}";

        return tile;
    }
}
