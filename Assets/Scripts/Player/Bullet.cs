using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;  // Sát thương của đạn

    public float speed = 5f; // Tốc độ di chuyển của đạn

    void Update()
    {
        // Di chuyển đạn theo hướng Y dương
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Xóa đạn nếu đi ra khỏi màn hình
        if (transform.position.y > Camera.main.orthographicSize + 1)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra va chạm với boss hoặc enemy
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Gọi hàm nhận sát thương của enemy
            }
            Destroy(gameObject);  // Hủy viên đạn sau khi va chạm
        }
        else if (other.CompareTag("Boss"))
        {
            BossController boss = other.GetComponent<BossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);  // Gọi hàm nhận sát thương của boss
            }
            Destroy(gameObject);  // Hủy viên đạn sau khi va chạm
        }
    }
}
