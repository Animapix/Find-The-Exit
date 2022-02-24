
using System;
using System.Collections.Generic;

public class Maze
{
    public enum CellType
    {
        Floor,
        Wall,
    }

    private enum Direction
    {
        West,
        North,
        Est,
        South
    }

    private static Random random = new Random();
    private static CellType[,] _cells;
    private static int _columns;
    private static int _rows;

    public static CellType[,] Generate(int columns, int rows)
    {
        _columns = columns;
        _rows = rows;
        _cells = new CellType[_columns, _rows];

        // Fill cells array with walls
        for (int column = 0; column < _columns; column++)
            for (int row = 0; row < _rows; row++)
                _cells[column, row] = CellType.Wall;

        DrillMaze(1, 1);

        return _cells;
    }

    private static void DrillMaze(int column, int row, int cellsCounter = 0)
    {
        _cells[column, row] = CellType.Floor;
        cellsCounter++;

        while (cellsCounter < (_columns * _rows) / 2)
        {
            List<Direction> avaibleDirections = GetAvaibleDirections(column,row);
            if (avaibleDirections.Count > 0)
            {
                int randomDirectionId = random.Next(avaibleDirections.Count);
                Direction randomDirection = avaibleDirections[randomDirectionId];
                switch (randomDirection)
                {
                    case Direction.West:
                        _cells[column - 1, row] = CellType.Floor;
                        DrillMaze(column - 2, row, cellsCounter);
                        break;
                    case Direction.North:
                        _cells[column, row - 1] = CellType.Floor;
                        DrillMaze(column, row - 2, cellsCounter);
                        break;
                    case Direction.Est:
                        _cells[column + 1, row] = CellType.Floor;
                        DrillMaze(column + 2, row, cellsCounter);
                        break;
                    case Direction.South:
                        _cells[column, row + 1] = CellType.Floor;
                        DrillMaze(column, row + 2, cellsCounter);
                        break;
                }
            }
            else
            {
                break;
            }
        }
    }

    private static List<Direction> GetAvaibleDirections(int column,int row)
    {
        List <Direction> avaibleDirections = new List<Direction>();

        if (IsInBounds(column - 2, row))
            if (_cells[column - 2, row] == CellType.Wall) avaibleDirections.Add(Direction.West);
        if (IsInBounds(column + 2, row))
            if (_cells[column + 2, row] == CellType.Wall) avaibleDirections.Add(Direction.Est);
        if (IsInBounds(column, row - 2))
            if (_cells[column, row - 2] == CellType.Wall) avaibleDirections.Add(Direction.North);
        if (IsInBounds(column, row + 2))
            if (_cells[column, row + 2] == CellType.Wall) avaibleDirections.Add(Direction.South);

        return avaibleDirections;
    }

    private static bool IsInBounds(int column, int row)
    {
        if (column < 0) return false;
        if (row < 0) return false;
        if (column >= _columns) return false;
        if (row >= _rows) return false;
        return true;
    }

}
