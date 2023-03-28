using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanSpawn : MonoBehaviour
{
    public GameObject[] enemies; // farklı düşmanlarınızın prefabları
    public float spawnDelay = 1f; // her düşmanın spawn aralığı
    public Transform[] spawnPoints; // düşmanların spawn edileceği konumlar

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true) // sonsuz döngü
        {
            yield return new WaitForSeconds(spawnDelay);

            int enemyIndex = Random.Range(0, enemies.Length); // rastgele düşman seçimi

            int spawnPointIndex = Random.Range(0, spawnPoints.Length); // rastgele spawn konumu seçimi
            Transform spawnPoint = spawnPoints[spawnPointIndex];

            Instantiate(enemies[enemyIndex], spawnPoint.position, spawnPoint.rotation); // düşmanın spawn edilmesi
        }
    }
}
