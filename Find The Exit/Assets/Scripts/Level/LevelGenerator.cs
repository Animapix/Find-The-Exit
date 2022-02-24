using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] GameObject batteryPrefab;
    [SerializeField] float tileSize = 4.0f;

    [SerializeField] public int chunkColumns = 15;
    [SerializeField] public int chunkRows = 20;

    private List<List<Tile>> tiles = new List<List<Tile>>();

    public int lastRow { get => tiles[0].Count; }

    private void Start()
    {
        for (int column = 0; column < chunkColumns; column++)
        {
            tiles.Add(new List<Tile>());
        }

        CreateChunk();
        CreateChunk();
    }

    public void CreateChunk()
    {

        int rowOffset = tiles[0].Count;
        Maze.CellType[,] maze = Maze.Generate(chunkColumns, chunkRows);


        if (rowOffset != 0)
        {
            List<int> avaibleOutputs = new List<int>();
            for (int column = 1; column < chunkColumns; column += 2)
            {
                bool isWall = tiles[column][rowOffset - 2].isWall;
                if (isWall) avaibleOutputs.Add(column);
            }
            int randomOutput = avaibleOutputs[Random.Range(0, avaibleOutputs.Count)];
            maze[randomOutput, 0] = Maze.CellType.Floor;
        }
        

        for (int row = 0; row < chunkRows; row++)
        {
            for (int column = 0; column < chunkColumns; column++)
            {
                bool isWall = maze[column, row] == Maze.CellType.Wall;
                Tile tile = CreateTile(column, row + rowOffset, isWall);
                tiles[column].Add(tile);
            }
        }

        if (rowOffset != 0)
        {
            StartCoroutine(AutoTile(0.001f, rowOffset - 1));
        }
        else
        {
            StartCoroutine(AutoTile(0.001f, rowOffset));
        }

    }

    private IEnumerator AutoTile(float delayTime, int rowOffset)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);

        for (int row = 0; row < chunkRows; row++)
        {
            for (int column = 0; column < chunkColumns; column++)
            {
                tiles[column][row + rowOffset].Autotile();
            }
        }
    }

    public IEnumerator AutoTile(float delayTime, int column, int row, int columns, int rows)
    {
        yield return new WaitForSeconds(delayTime);
        for (int r = row; r < row + rows; r++)
        {
            for (int c = column; c < column + columns; c++)
            {
                if (c >= 0 && r >= 0 && c < tiles.Count && r < tiles[0].Count)
                {
                    tiles[c][r].Autotile();
                }
            }
        }
    }


    private Tile CreateTile(int column, int row, bool isWall)
    {
        float x  = column * tileSize;
        float z  = row * tileSize;

        Tile tile = Instantiate(tilePrefab, transform).GetComponent<Tile>();
        tile.transform.position = new Vector3(x, 0, z);
        tile.name = $"Tile {column}-{row}";
        
        tile.isWall = isWall;

        int rnd = Random.Range(0, 100);
        if (!isWall && rnd < 10)
        {
            Battery battery = Instantiate(batteryPrefab, transform).GetComponent<Battery>();
            battery.transform.position = new Vector3(x, 2, z);
        }

        return tile;
    }
}
