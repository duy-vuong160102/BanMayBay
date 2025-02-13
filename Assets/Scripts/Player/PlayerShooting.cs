using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab của viên đạn
    public GameObject specialBulletPrefab;   // Prefab đạn đặc biệt 
    public Transform firePoint;     // Vị trí bắn 
    public float bulletSpeed = 10f; // Tốc độ bay của viên đạn
    public bool isSpecialMode = false;       // Chế độ bắn đặc biệt
    public AudioSource shootAudio;      // Âm thanh khi bắn
    public AudioClip shootClip;         // File âm thanh bắn

    void Start()
    {
        if (shootAudio == null)
        {
            shootAudio = GetComponent<AudioSource>(); // Tìm AudioSource trên Player
        }
    }
    void Update()
    {
        // Kiểm tra nếu chuột trái được nhấn
        if (Input.GetMouseButtonDown(0)) // chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (isSpecialMode && specialBulletPrefab != null) // Bắn chế độ đặc biệt
        {
            FireSpecialBullets();
        }
        else if (bulletPrefab != null) // Bắn chế độ thường
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        PlayShootSound();
    }
    void PlayShootSound()
    {
        if (shootAudio != null && shootClip != null)
        {
            shootAudio.PlayOneShot(shootClip); // Phát âm thanh một lần
        }
    }
    void FireSpecialBullets()
    {
        // Bắn 3 viên đạn theo hình nón
        Instantiate(specialBulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(specialBulletPrefab, firePoint.position, Quaternion.Euler(0, 0, 15) * firePoint.rotation);
        Instantiate(specialBulletPrefab, firePoint.position, Quaternion.Euler(0, 0, -15) * firePoint.rotation);
    }

    public void SetSpecialMode(bool isActive)
    {
        isSpecialMode = isActive; // Bật hoặc tắt chế độ bắn đặc biệt
    }
}
