using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHealth2D : MonoBehaviour
{
    public int health = 1; // Số lần bắn cần để phá hủy thiên thạch
    public GameObject explosionEffect; // Hiệu ứng nổ
    public float explosionDuration = 1f; // Thời gian hiệu ứng nổ tồn tại (giây)

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với viên đạn
        if (collision.CompareTag("Bullet"))
        {
            // Giảm máu
            health--;

            // Hủy viên đạn sau khi va chạm
            Destroy(collision.gameObject);

            // Nếu máu <= 0, phá hủy thiên thạch
            if (health <= 0)
            {
                Explode(); // Hiệu ứng nổ
            }
        }
    }

    void Explode()
    {
        // Tạo hiệu ứng nổ tại vị trí thiên thạch
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Hủy hiệu ứng nổ sau thời gian định trước
            Destroy(explosion, explosionDuration);
        }

        // Phá hủy thiên thạch
        Destroy(gameObject);
    }
}
