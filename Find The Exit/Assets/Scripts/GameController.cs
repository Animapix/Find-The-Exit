using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController instance;

    [SerializeField] public LevelGenerator level;
    [SerializeField] public RobotController robot;
    [SerializeField] public Scrolling scrolling;

    [SerializeField] public HUD hud;

    [SerializeField] public float stageRows;

    private int score = 0;
    private int stage = 0;
    private float totalDistance = 0f;
    private float distanceBetweenStart = 0f;

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

    private void Start()
    {
        hud.setScore(score);
        hud.setStage(stage);
    }


    private void Update()
    {
        if (robot.coordinates.row > level.lastRow - level.chunkRows - 10)
        {
            level.CreateChunk();
        }

        if ((robot.coordinates.row - stage * stageRows) / stageRows > 1.0f)
        {
            stage++;
            hud.setStage(stage);
            scrolling.speed += 0.1f;
        }

    }

    public void AddToScore(int amount)
    {
        score += amount;
        hud.setScore(score);
    }
}
