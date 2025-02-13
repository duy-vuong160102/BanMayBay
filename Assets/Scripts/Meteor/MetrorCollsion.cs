using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MetrorCollsion : MonoBehaviour
{
    public GameObject explosionEffect; // Hiệu ứng nổ
    public float explosionDuration = 1f; // Thời gian tồn tại của hiệu ứng nổ
    public int meteorPoints = 3; // Điểm cho mỗi thiên thạch

    public AudioSource explosionAudio;    // AudioSource để phát âm thanh nổ
    public AudioClip explosionClip;       // Âm thanh nổ
    private ScoreManager scoreManager; // Tham chiếu  ScoreManager

    void Start()
    {
        if (explosionAudio == null)
        {
            explosionAudio = GetComponent<AudioSource>(); // Tìm AudioSource trên kẻ địch
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với người chơi
        if (collision.CompareTag("Player"))
        {

            // Gọi hàm tạo hiệu ứng nổ
            Explode();
            PlayExplosionSound();

            // Phá hủy thiên thạch
            Destroy(gameObject);


        }
    }

    void Explode()
    {
        // Tạo hiệu ứng nổ tại vị trí thiên thạch
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(explosion, explosionDuration); // Hủy hiệu ứng nổ sau thời gian
        }
    }
    void PlayExplosionSound()
    {
        if (explosionAudio != null && explosionClip != null)
        {
            explosionAudio.PlayOneShot(explosionClip); // Phát âm thanh một lần
        }
    }
}