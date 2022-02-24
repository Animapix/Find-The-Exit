using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] LevelGenerator level;
    [SerializeField] RobotController robot;

    private void Update()
    {
        if (robot.coordinates.row > level.lastRow - level.chunkRows - 10)
        {
            level.CreateChunk();
        }
    }
}
