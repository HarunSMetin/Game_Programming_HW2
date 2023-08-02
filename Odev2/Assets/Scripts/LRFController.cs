using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRFController : MonoBehaviour
{

    public float radarRange = 30f; // Radarýn görüþ mesafesi
    public float scanAngle = 60f; // Tarayacak açý (60 derece) 
    public GameObject radarMarkerPrefab; // Radar iþaretleme için prefab  
    private List<Transform> enemiesInRange = new List<Transform>(); // Görüþ mesafesindeki düþmanlarý tutmak için liste
    private List<Transform> LRFobjects = new List<Transform>(); // radar nokatalrini  tutmak için liste 
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
        // Mevcut düþmanlarý temizleyelim (her frame'de yeniden hesaplama yapmak için)
        enemiesInRange.Clear();

        // Tüm düþmanlarýn listesini alalým (örn. tag'e göre) 

        // Her bir düþmanýn oyuncudan uzaklýðýný ve bakýþ açýsýna göre kontrol edelim
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
