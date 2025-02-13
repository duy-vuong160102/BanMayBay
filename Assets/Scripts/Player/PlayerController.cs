using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public int health = 10;  // Máu của người chơi
    public int score = 0;    // Điểm của người chơi
    public TextMeshProUGUI scoreText;   // Hiển thị điểm
    public TextMeshProUGUI healthText;
    public GameObject explosionEffect;  // Hiệu ứng nổ khi player bị tiêu diệt
    public float moveSpeed = 5f;
    public Vector2 minBounds;    // Giới hạn thấp nhất (Xmin, Ymin)
    public Vector2 maxBounds;    // Giới hạn cao nhất (Xmax, Ymax)

    private Rigidbody rb;

    public Vector3 moveInput;

    public int unlockScore = 300; // Điểm để mở khóa chế độ bắn mới
    private PlayerShooting shootingController;
    public LoseScreenManager loseScreenManager;

    public SpriteRenderer playerRenderer;
    void Start()
    {
        UpdateHealthUI();  // Cập nhật UI máu khi bắt đầu game
        UpdateScoreUI();
        shootingController = GetComponent<PlayerShooting>();

        if (shootingController == null)
        {
            Debug.LogError("ShootingController is missing!");
        }
        if (loseScreenManager == null)
        {
            loseScreenManager = FindObjectOfType<LoseScreenManager>(); // Tìm script quản lý màn hình chiến thắng
        }
    }
    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        Vector3 currentPosition = transform.position;

        // Giới hạn trục X và Y
        currentPosition.x = Mathf.Clamp(currentPosition.x, minBounds.x, maxBounds.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minBounds.y, maxBounds.y);

        // Cập nhật lại vị trí của máy bay
        transform.position = currentPosition;

        if (health <= 0)
        {
            Die();  // Nếu máu bằng 0, người chơi chết
        }
    }
    // Hàm nhận sát thương khi va chạm với đạn hoặc máy bay địch
    public void TakeDamage(int damage)
    {
        health -= damage;  // Giảm máu
        UpdateHealthUI();  // Cập nhật UI máu sau khi nhận sát thương

        if (health <= 0)
        {
            Die();
        }

    }
    public void AddScore(int points)
    {
        score += points;  // Cộng thêm điểm

        UpdateScoreUI();  // Cập nhật giao diện điểm
        // Kiểm tra nếu đạt điểm mở khóa
        if (score >= unlockScore && shootingController != null)
        {
            shootingController.SetSpecialMode(true); // Mở khóa chế độ bắn đặc biệt
            Debug.Log("Special shooting mode unlocked!");
        }
    }
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score;
        }
    }

    // Hàm xử lý khi người chơi chết
    void Die()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);  // Hiệu ứng nổ
        }
        // Hủy người chơi sau khi chết
        playerRenderer.enabled = false;
        loseScreenManager.GameOver();
        // lose
    }

    // Hàm va chạm với đạn hoặc máy bay địch
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Meteor"))
        {
            TakeDamage(1);  // Trừ 1 máu khi va chạm với đạn hoặc máy bay địch
            Destroy(other.gameObject);  // Hủy đạn hoặc máy bay địch sau khi va chạm
        }
    }
    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "" + health;  // Cập nhật giá trị máu hiện tại
        } 
    }

}
