using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreenManager : MonoBehaviour
{
    public GameObject game;
    public GameObject gameScreen;
    public GameObject gameOverPanel;
    public GameObject loadingUI;   // Giao diện Loading
    public Slider progressBar;     // Thanh tiến trình
    public TextMeshProUGUI progressText; // Text hiển thị phần trăm
    public void OnHomeButtonClicked()
    {
        // Ẩn màn hình 
        gameScreen.SetActive(false);
        gameOverPanel.SetActive(false);

        // Hiển thị giao diện Loading
        loadingUI.SetActive(true);

        // Bắt đầu tiến trình tải
        StartCoroutine(LoadGameData());
    }

    IEnumerator LoadGameData()
    {
        // Giả lập quá trình tải game
        float loadProgress = 0;

        while (loadProgress < 1f)
        {
            // Tăng tiến trình tải
            loadProgress += Time.deltaTime * 0.5f; // Tốc độ tải giả lập
            progressBar.value = loadProgress;
            progressText.text = (loadProgress * 100).ToString("F0") + "%";

            yield return null;
        }

        // Khi tải xong, ẩn giao diện Loading và hiển thị nội dung game
        SceneManager.LoadScene("Menu");
        SceneManager.LoadScene("Menu");
    }
    void Start()
    {
        gameOverPanel.SetActive(false); // Ẩn màn hình
        loadingUI.SetActive(false);
    }
    public void GameOver()
    {
        Invoke("ShowResultScreen", 0f); // Gọi hàm sau 0 giây
        game.SetActive(false);
    }

    void ShowResultScreen()
    {
        gameOverPanel.SetActive(true); // Hiển thị màn hình kết quả
    }
}
