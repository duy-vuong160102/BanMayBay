using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 3f;  // Tốc độ của viên đạn

    void Update()
    {
        // Di chuyển đạn theo trục Y âm
        transform.Translate(Vector3.down * bulletSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm với người chơi
        if (other.CompareTag("Player"))
        {
            // Gọi hàm thua cuộc của người chơi
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                //player.GameOver();
            }

            // Hủy viên đạn sau khi va chạm
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Hủy viên đạn khi ra khỏi màn hình
        Destroy(gameObject);
    }
}
