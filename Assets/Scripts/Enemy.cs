using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.InputSystem;
using System.Collections;

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

            if (ItemManager.Instance != null)
            {
                if (Random.Range(0, 100) < 20)
                {
                    ItemManager.Instance.DropItem(transform.position);
                }
            }

            Destroy(gameObject);
        }
    }
    public void StopEnemy(float time)
    {
        StartCoroutine(StopCoroutine(time));
    }


    IEnumerator StopCoroutine(float time)
    {
        float oldSpeed = speed;

        speed = 0;


        yield return new WaitForSeconds(time);


        speed = oldSpeed;
    }
}