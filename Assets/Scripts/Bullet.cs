using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f;
    public float lifeTime = 3f;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.Damage(1);   // 적 HP 1 감소
            }

            Destroy(gameObject);   // 총알 제거
        }
    }
}