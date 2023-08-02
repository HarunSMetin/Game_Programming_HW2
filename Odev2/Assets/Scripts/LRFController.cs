using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRFController : MonoBehaviour
{

    public float radarRange = 30f; // Radar�n g�r�� mesafesi
    public float scanAngle = 60f; // Tarayacak a�� (60 derece) 
    public GameObject radarMarkerPrefab; // Radar i�aretleme i�in prefab  
    private List<Transform> enemiesInRange = new List<Transform>(); // G�r�� mesafesindeki d��manlar� tutmak i�in liste
    private List<Transform> LRFobjects = new List<Transform>(); // radar nokatalrini  tutmak i�in liste 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ScanForEnemies()
    {
        // Mevcut d��manlar� temizleyelim (her frame'de yeniden hesaplama yapmak i�in)
        enemiesInRange.Clear();

        // T�m d��manlar�n listesini alal�m (�rn. tag'e g�re) 

        // Her bir d��man�n oyuncudan uzakl���n� ve bak�� a��s�na g�re kontrol edelim
        foreach (GameObject enemy in EnemyManager.Instance.createdEnemies)
        {
            Vector3 enemyDirection = enemy.transform.position - GameManager.Instance.PlayerPoint.transform.position;
            float angleToEnemy = Vector3.Angle(GameManager.Instance.PlayerPoint.transform.forward, enemyDirection);

            if (enemyDirection.magnitude <= radarRange && angleToEnemy <= scanAngle / 2f)
            {
                enemiesInRange.Add(enemy.transform); // 60 derecelik alandaysa listeye ekleyelim
            }
        } 
        UpdateRadarMarkers();
    }

    private void UpdateRadarMarkers()
    { 

    }
}
