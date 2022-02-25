using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] Text scoreText;
    [SerializeField] Text StageText;
    [SerializeField] Text collectedText;

    public void SetGameData(int score , int stage, int collected)
    {
        scoreText.text = score.ToString();
        StageText.text = stage.ToString();
        collectedText.text = collected.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
