using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // X, Y ve Z tipi d��man prefab'leri
    public int enemyCount = 10; // D��man say�s�
    public float spawnRadius = 7f; // Oyuncu etraf�nda yarat�lmamas� i�in 0,0,0 noktas�na olan uzakl�k
    public float rangeOfPlayground = 100f;  
    private void Start()
    {
        PlaceEnemies();
    }
    private void PlaceEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            KeyValuePair<GameObject, int> randomEnemyPair = GetRandomEnemyPrefab(); 
            GameObject randomEnemyPrefab = randomEnemyPair.Key;
           
            GameObject CreatedEnemy = Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);

            Enemy e = CreatedEnemy.GetComponent<Enemy>();
            switch (randomEnemyPair.Value)
            {
                case 0:
                    e.health = 100;
                    break;
                case 1:
                    e.health = 150;
                    break;
                case 2:
                    e.health = 200;
                    break;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-rangeOfPlayground, rangeOfPlayground), 0f, Random.Range(-rangeOfPlayground, rangeOfPlayground));
        while (randomPosition.magnitude < spawnRadius)
        {
            randomPosition = new Vector3(Random.Range(-rangeOfPlayground, rangeOfPlayground), 0f, Random.Range(-rangeOfPlayground, rangeOfPlayground));
        }
        return randomPosition;
    }

    private KeyValuePair<GameObject,int> GetRandomEnemyPrefab()
    {
        int index = (int)Random.Range(0, enemyPrefabs.Length);
        return new KeyValuePair<GameObject, int>(enemyPrefabs[index],index) ;
    }
}
