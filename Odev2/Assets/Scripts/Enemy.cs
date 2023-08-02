using System.Linq;
using UnityEngine; 
using UnityEngine.UI ;
using TMPro;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float health = 1000;
    public GameObject player;
    public Slider slider;
    public TMP_Text sliderValue;
    Animator animator;
    public bool isAlive = true;
    bool stopFlag = false;
    
    private void Start()
    {
        animator=gameObject.GetComponent<Animator>();
         switch (GameManager.Instance.currentDiff)
        {
            case GameData.difficulty.easy:
                moveSpeed = Random.Range(0.1f, 0.5f);
                break;
            case GameData.difficulty.medium:
                moveSpeed = Random.Range(0.1f, 0.1f);
                break;
            case GameData.difficulty.hard:
                moveSpeed = Random.Range(0.1f, 1.5f);
                break; 
            default:
                break;
        } 
        player = GameManager.Instance.PlayerPoint; 
        animator.SetFloat("speed",moveSpeed);
        slider.value = health;
        slider.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    { 
        if(isAlive && !stopFlag)
            MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    { 
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f; // Sadece Y ekseni rotasyonunu koruyoruz
        transform.LookAt(transform.position + direction.normalized);

        // Hareket
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        if(Vector3.Distance(transform.position, player.transform.position) < 30)
            slider.transform.parent.gameObject.SetActive(true);


    }

    public void TakeDamage(float damageAmount, float distance)
    {
        if (isAlive)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                health=0;
                Die(distance);
            }
            slider.value = health;
            sliderValue.text = health.ToString();
        }
    }

    private void Die(float distance)
    {
        isAlive = false;
        GameManager.Instance.totalKill ++;
        GameManager.Instance.distanceArray.Add(distance);
        // Düþmanýn yok edilmesi veya patlama efekti gibi ölüm iþlemleri burada yapýlabilir.
        EnemyManager.Instance.enemyCount--;
        GameManager.Instance.LeftEnemy.text = EnemyManager.Instance.enemyCount.ToString();
        animator.SetBool("Die", true);
        Debug.Log("Enemy died!");
        Destroy(gameObject,10);
        if(EnemyManager.Instance.enemyCount == 0) GameManager.Instance.EndGame();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stopFlag = false;
            animator.SetBool("Idle", true);
        }
    }
}
