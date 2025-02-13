using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] meteorPrefabs; // Mảng chứa 3 loại thiên thạch
    public float spawnRate = 1f; // Tốc độ sinh thiên thạch
    public float spawnRangeX = 8f; // Phạm vi sinh trên trục X
    public float spawnHeight = 8f; // Vị trí Y để sinh thiên thạch

    private float spawnTimer;

    void Update()
    {
        // Tăng bộ đếm thời gian
        spawnTimer += Time.deltaTime;

        // Nếu đủ thời gian, tạo thiên thạch
        if (spawnTimer >= spawnRate)
        {
            SpawnMeteor();
            spawnTimer = 0; // Reset bộ đếm
        }
    }

    void SpawnMeteor()
    {
        // Chọn thiên thạch ngẫu nhiên
        int randomIndex = Random.Range(0, meteorPrefabs.Length);

        // Tạo vị trí ngẫu nhiên trên trục 
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0);

        // Sinh thiên thạch tại vị trí
        Instantiate(meteorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
