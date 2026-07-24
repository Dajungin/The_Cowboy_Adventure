using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;

    [Header("Enemy")]
    public float speed = 2f;
    public int hp = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime);
    }

    public void Damage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}