using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText; 
    private static int totalScore = 0; // Điểm tổng
    public GameObject gameWinPanel;
    public TextMeshProUGUI finalScoreText;

    void Awake() { instance = this; }
    void Start()
    {
        UpdateScoreUI(); // Cập nhật điểm khi bắt đầu
    }

    // Thêm điểm vào tổng điểm
    public void AddScore(int points)
    {
        totalScore += points;
        UpdateScoreUI(); // Cập nhật điểm trên UI
    }

    // Cập nhật điểm trên màn hình
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + totalScore; // Hiển thị điểm
        }
    }

    public void GameWin()
    {
        gameWinPanel.SetActive(true);
        finalScoreText.text = "" + scoreText.text;
    }
}
