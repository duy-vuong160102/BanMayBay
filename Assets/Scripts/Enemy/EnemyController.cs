using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 2;
    public GameObject explosionEffect;  // Hiệu ứng nổ khi enemy bị tiêu diệt
    public float moveSpeed = 2f;          // Tốc độ di chuyển
    public GameObject bulletPrefab;      // Prefab của viên đạn địch
    public Transform firePoint;          // Vị trí bắn đạn
    public float fireInterval = 1f;      // Khoảng thời gian giữa các lần bắn
    private float fireTimer;             // Bộ đếm thời gian cho việc bắn
    public float explosionDuration = 1f; // Thời gian hiệu ứng nổ tồn tại
    public int scoreValue = 10;  // Điểm khi tiêu diệt máy bay

    public AudioSource explosionAudio;    // AudioSource để phát âm thanh nổ
    public AudioClip explosionClip;       // Âm thanh nổ

    void Start()
    {
        if (explosionAudio == null)
        {
            explosionAudio = GetComponent<AudioSource>(); // Tìm AudioSource trên kẻ địch
        }
    }
    void Update()
    {
        // Di chuyển máy bay địch từ từ về phía trước (trục Y âm)
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        // Kiểm tra thời gian để bắn đạn
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            Shoot(); // Bắn đạn
            fireTimer = 0; // Reset bộ đếm
        }
    }

    void Shoot()
    {
        // Tạo viên đạn tại vị trí bắn 
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    void OnBecameInvisible()
    {
        // Hủy máy bay địch khi ra khỏi màn hình
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;  // Giảm máu khi bị tấn công

        if (health <= 0)
        {
            Die();  // Nếu máu <= 0, enemy sẽ chết
        }
    }

    void Die()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.AddScore(scoreValue);  // Ghi điểm
        }
        // Hiển thị hiệu ứng nổ (nếu có)
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Hủy hiệu ứng nổ sau khi nó kết thúc (sử dụng thời gian sống của particle system)
            Destroy(explosion, explosionDuration);
        }
        PlayExplosionSound();

        // Hủy enemy sau khi chết
        Destroy(gameObject);

    }
    void PlayExplosionSound()
    {
        if (explosionAudio != null && explosionClip != null)
        {
            explosionAudio.PlayOneShot(explosionClip); // Phát âm thanh một lần
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi hàm TakeDamage trên PlayerController
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);  // Trừ máu của người chơi
            }

            // Phát nổ và hủy máy bay địch
            Explode();
        }
    }

    void Explode()
    {
        // Tạo hiệu ứng nổ
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Hủy hiệu ứng nổ sau khi nó kết thúc (sử dụng thời gian sống của particle system)
            Destroy(explosion, explosionDuration);
        }
        Destroy(gameObject);
    }
}
