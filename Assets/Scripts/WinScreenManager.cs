using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreenManager : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject winScreen;  // Màn hình chiến thắng
    public TextMeshProUGUI diem;     // Text hiển thị điểm số
    public GameObject loadingUI;   // Giao diện Loading
    public Slider progressBar;     // Thanh tiến trình
    public TextMeshProUGUI progressText; // Text hiển thị phần trăm
    public ScoreManager scoreManager;

    void Start()
    {
        winScreen.SetActive(false);
        loadingUI.SetActive(false);
    }
    public void OnHomeButtonClicked()
    {
        // Ẩn màn hình 
        winScreen.SetActive(false);
        gameScreen.SetActive(false);
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
            loadProgress += Time.deltaTime * 0.5f; // Tốc độ tải 
            progressBar.value = loadProgress;
            progressText.text = (loadProgress * 100).ToString("F0") + "%";

            yield return null;
        }

        // Khi tải xong, ẩn giao diện Loading và hiển thị nội dung game
        SceneManager.LoadScene("Menu");


    }

    public void EndGame()
    {
        Invoke("ShowResultScreen", 1f); // Gọi hàm sau 1 giây
    }

    void ShowResultScreen()
    {
        ScoreManager.instance.GameWin();

    }

    public void HomeGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
