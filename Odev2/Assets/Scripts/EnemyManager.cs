using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = GameObject.Find("/Enemy Manager");
                if (go == null)
                {
                    return null;
                }

                instance = go.GetComponent<EnemyManager>();
                if (instance == null)
                {
                    return null;
                }
            }
            return instance;
        }
    }

    public GameObject[] enemyPrefabs; // X, Y ve Z tipi düþman prefab'leri
    public int enemyCount = 10; // Düþman sayýsý
    public float spawnRadius = 7f; // Oyuncu etrafýnda yaratýlmamasý için 0,0,0 noktasýna olan uzaklýk
    public float rangeOfPlayground = 100f;  
    public HashSet<GameObject> createdEnemies = new HashSet<GameObject>(); // Oyunda bulunan düþmanlar

    float healthParam = 0.1f;
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
            createdEnemies.Add(CreatedEnemy);
            Enemy e = CreatedEnemy.GetComponent<Enemy>();
            e.isAlive = true;
            switch (randomEnemyPair.Value)
            {
                case 0:
                    e.health = 1000* healthParam;
                    e.slider.maxValue = 1000 * healthParam; 
                    break;
                case 1:
                    e.health = 1500 * healthParam;
                    e.slider.maxValue = 1500 * healthParam;
                    break;
                case 2:
                    e.health = 2000 * healthParam;
                    e.slider.maxValue = 2000 * healthParam; 
                    break;
            }

            e.slider.minValue = 0;
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
