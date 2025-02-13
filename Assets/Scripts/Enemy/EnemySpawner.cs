using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] enemyPrefabs; // Mảng chứa các loại máy bay địch
        public GameObject bossPrefab;    // Prefab của máy bay boss
        public float spawnInterval = 1f; // Thời gian giữa các lần spawn
        public Vector2 spawnRangeX;      // Phạm vi xuất hiện theo trục X
        public Vector2 spawnRangeY;      // Phạm vi xuất hiện theo trục Y
        public float bossSpawnTime = 120f; // Thời gian (giây) để boss xuất hiện

        private bool bossSpawned = false; // Kiểm tra xem boss đã xuất hiện hay chưa

        void Start()
        {
            // Bắt đầu spawn địch thường
            InvokeRepeating("SpawnRandomEnemy", spawnInterval, spawnInterval);

            // Spawn boss sau thời gian chờ
            Invoke("SpawnBoss", bossSpawnTime);
        }

        void SpawnRandomEnemy()
        {
            // Nếu boss đã xuất hiện thì không spawn địch thường nữa
            if (bossSpawned) return;

            // Chọn ngẫu nhiên 1 loại máy bay địch
            int randomIndex = Random.Range(0, enemyPrefabs.Length);

            // Tạo vị trí ngẫu nhiên trong phạm vi
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randomY = Random.Range(spawnRangeY.x, spawnRangeY.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Tạo máy bay địch
            Instantiate(enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }

        void SpawnBoss()
        {
            // Tạo boss tại một vị trí cố định hoặc ngẫu nhiên
            float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            Vector3 bossPosition = new Vector3(randomX, spawnRangeY.y, 0);

            Instantiate(bossPrefab, bossPosition, Quaternion.identity);

            // Đánh dấu boss đã xuất hiện
            bossSpawned = true;

            // Ngừng việc spawn máy bay thường
            CancelInvoke("SpawnRandomEnemy");
        }
    }