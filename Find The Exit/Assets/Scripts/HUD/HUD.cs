using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text stageText;


    public void setScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void setStage(int stage)
    {
        stageText.text = stage.ToString();
    }
}
