using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Số viên đạn cần để phá hủy máy bay địch
    public GameObject explosionEffect; // Hiệu ứng nổ khi máy bay bị phá hủy (tùy chọn)

    // Hàm xử lý khi bị trúng đạn
    public void TakeDamage(int damage)
    {
        health -= damage; // Giảm máu của máy bay
        if (health <= 0)
        {
            Die();
        }
    }

    // Hàm phá hủy máy bay
    void Die()
    {
        // Hiệu ứng nổ (tùy chọn)
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Hủy máy bay
        Destroy(gameObject);
    }

}
