using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float health = 1000;
    public GameObject player;
    Animator animator;
    private void Start()
    {
        animator=gameObject.GetComponent<Animator>();
        moveSpeed = Random.Range(0.1f, 1.5f);
        player = GameManager.Instance.PlayerPoint; 
        animator.SetFloat("speed",moveSpeed);
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    { 
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f; // Sadece Y ekseni rotasyonunu koruyoruz
        transform.LookAt(transform.position + direction.normalized);

        // Hareket
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
         

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Düþmanýn yok edilmesi veya patlama efekti gibi ölüm iþlemleri burada yapýlabilir.
        Debug.Log("Enemy died!");
        DestroyImmediate(gameObject);
    }
}
