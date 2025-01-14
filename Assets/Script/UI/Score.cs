using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public float totalScore;
    public int ScoreMultiples;
    public float bonusScore { get; set; }
    public TMP_Text scoreText;

    

    private void OnEnable()
    {
        bonusScore = 0; 
        float multyple = ((GameManager.Instance.gameDifficulty - 1) / 2.0f) + 1;
        ScoreMultiples = (int)(100.0f * multyple); // ���̵� ������ ���� �ٲ�����
    }

    void Update()
    {
        totalScore = bonusScore * ScoreMultiples;
        scoreText.text = "Score : "+ totalScore.ToString("F0");
    }
}
