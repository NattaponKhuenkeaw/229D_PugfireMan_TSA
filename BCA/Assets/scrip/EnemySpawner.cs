using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;          // Prefab ของศัตรู
    public Transform[] spawnPoints;         // ตำแหน่งที่ศัตรูจะเกิด (ตั้งไว้หลายจุดได้)
    public float spawnInterval = 3f;        // เวลาเกิดศัตรูทุกๆ กี่วินาที

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        // สุ่มตำแหน่ง spawn
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // สร้างศัตรู
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.Euler(0f,180f,0f));
    }
}

