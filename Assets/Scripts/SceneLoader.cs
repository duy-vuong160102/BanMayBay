using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public GameObject startScreen; // Màn hình Start
    public GameObject loadingUI;   // Giao diện Loading
    public GameObject gameContent; // Nội dung game
    public Slider progressBar;     // Thanh tiến trình
    public TextMeshProUGUI progressText; // Text hiển thị phần trăm
    void Start()
    {
    }
    public void OnStartButtonClicked()
    {
        // Ẩn màn hình Start
        startScreen.SetActive(false);

        // Hiển thị giao diện Loading
        loadingUI.SetActive(true);

        // Bắt đầu tiến trình tải
        StartCoroutine(LoadGameData());
    }

    IEnumerator LoadGameData()
    {
        // quá trình tải game
        float loadProgress = 0;

        while (loadProgress < 1f)
        {
            // Tăng tiến trình tải
            loadProgress += Time.deltaTime * 0.5f; // Tốc độ tải 
            progressBar.value = loadProgress;
            progressText.text = (loadProgress * 100).ToString("F0") + "%";

            yield return null;
        }

        
        SceneManager.LoadScene("A");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
