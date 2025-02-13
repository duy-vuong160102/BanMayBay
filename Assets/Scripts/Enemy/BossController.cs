using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 50;                // Máu của boss
    public GameObject explosionEffect;     // Hiệu ứng nổ khi boss chết
    public GameObject bulletPrefab;      // Prefab của viên đạn
    public Transform[] firePoints;       // Các điểm bắn trên boss
    public float fireInterval = 1.5f;      // Khoảng thời gian giữa các lần bắn
    public float bulletSpeed = 7f;       // Tốc độ của viên đạ
    private float fireTimer;             // Bộ đếm thời gian bắn
    public float moveSpeed = 0f;         // Tốc độ di chuyển của boss
    public float leftLimit = -8f;        // Giới hạn trái
    public float rightLimit = 8f;        // Giới hạn phải
    private bool moveRight = true;       // Kiểm tra xem boss di chuyển phải hay trái
    public WinScreenManager winScreenManager; // màn hình chiến thắng

    void Start()
    {
        if (winScreenManager == null)
        {
            winScreenManager = FindObjectOfType<WinScreenManager>(); 
        }
    }
    void Update()
    {
        MoveBoss();
        // Tăng bộ đếm thời gian
        fireTimer += Time.deltaTime;

        // Nếu đủ thời gian, boss bắn đạn
        if (fireTimer >= fireInterval)
        {
            Shoot();
            fireTimer = 0; // Reset bộ đếm
            transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
        }
    }
    void MoveBoss()
    {
        // Di chuyển boss theo trục X
        if (moveRight)
        {
            // Boss di chuyển về bên phải
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            // Boss di chuyển về bên trái
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        // Đổi hướng khi boss chạm đến biên
        if (transform.position.x >= rightLimit)  // Chạm biên phải
        {
            moveRight = false;  // Đổi hướng sang trái
        }
        else if (transform.position.x <= leftLimit)  // Chạm biên trái
        {
            moveRight = true;   // Đổi hướng sang phải
        }
    }

    void Shoot()
    {
        // Bắn đạn từ tất cả các điểm bắn
        foreach (Transform firePoint in firePoints)
        {
            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;  // Giảm máu khi nhận sát thương

        if (health <= 0)
        {
            Die();  // Nếu máu của boss <= 0, boss chết
        }
    }

    void Die()
    {
        // Hiển thị hiệu ứng nổ khi boss chết
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        winScreenManager.EndGame(); // Gọi màn hình chiến thắng
        
        // Hủy boss
        Destroy(gameObject);
    }
}
