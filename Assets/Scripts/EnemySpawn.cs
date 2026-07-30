using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] monsters;   // 여러 종류의 몬스터

    [Header("Spawn")]
    public float time = 2f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, time);
    }

    void Spawn()
    {
        // 등록된 몬스터가 없으면 생성하지 않음
        if (monsters.Length == 0)
            return;

        Vector2 pos;

        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0:
                pos = new Vector2(Random.Range(-10f, 10f), 6f);
                break;

            case 1:
                pos = new Vector2(Random.Range(-10f, 10f), -6f);
                break;

            case 2:
                pos = new Vector2(-10f, Random.Range(-5f, 5f));
                break;

            default:
                pos = new Vector2(10f, Random.Range(-5f, 5f));
                break;
        }

        // 랜덤 몬스터 선택
        int randomIndex = Random.Range(0, monsters.Length);

        Instantiate(monsters[randomIndex], pos, Quaternion.identity);
    }
}