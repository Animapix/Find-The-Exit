using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    [SerializeField] public LevelGenerator level;
    [SerializeField] public RobotController robot;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (robot.coordinates.row > level.lastRow - level.chunkRows - 10)
        {
            level.CreateChunk();
        }
    }
}
