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
    [SerializeField] public GameOverUI gameOverUI;

    [SerializeField] float stageRows;
    [SerializeField] float speedUpAmount = 0.3f;

    private int score = 0;
    private int stage = 0;
    private float totalDistance = 0f;
    private float distanceBetweenStart = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        hud.setScore(score);
        hud.setStage(stage);

        hud.GetComponent<FadeCanvas>().FadeInUI();
    }


    private void Update()
    {
        if (robot.coordinates.row > level.lastRow - level.chunkRows - 10)
        {
            level.CreateChunk();
        }

        if ((robot.coordinates.row - stage * stageRows) / stageRows > 1)
        {
            stage++;
            hud.setStage(stage);
            scrolling.speed += speedUpAmount;
        }

    }

    public void AddToScore(int amount)
    {
        score += amount;
        hud.setScore(score);
    }
    public void GameOver()
    {
        gameOverUI.GetComponent<FadeCanvas>().FadeInUI();
        hud.GetComponent<FadeCanvas>().FadeOutUI();

        robot.GetComponent<RobotMovement>().enabled = false;

        gameOverUI.SetGameData(score * stage, stage, score);

    }
}
