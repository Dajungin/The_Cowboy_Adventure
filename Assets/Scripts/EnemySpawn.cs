using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // 적 스폰시간, 스테이지에 따른 몬스터 종류와 능력 
    //계속 적 종류를 추가할 수 있게 하기
    //스폰을 화면 밖에서 하기
    //
    public GameObject monster;

    public float time = 2f;

    void Start()
    {
        InvokeRepeating("Spawn", 1, time);
    }

    void Spawn()
    {
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

        Instantiate(monster, pos, Quaternion.identity);
    }
}
