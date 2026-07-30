using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageManager : MonoBehaviour
{
    public GameObject nextStageObject;
    public Transform spawnPosition;

    public float surviveTime = 60f;

    [Header("UI")]
    public TMP_Text timerText;

    private float currentTime;
    private bool stageClear = false;


    void Start()
    {
        nextStageObject.SetActive(false);

        currentTime = surviveTime;

        UpdateTimerUI();
    }


    void Update()
    {
        if (stageClear)
            return;


        currentTime -= Time.deltaTime;


        if (currentTime <= 0)
        {
            currentTime = 0;

            StageClear();
        }


        UpdateTimerUI();
    }


    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text =
                "생존 시간 : " + Mathf.Ceil(currentTime) + "초";
        }
    }


    void StageClear()
    {
        stageClear = true;


        // 몬스터 제거
        RemoveAllEnemy();


        // 몬스터 생성 중지
        StopEnemySpawn();


        // 이동 오브젝트 생성
        CreateNextObject();
    }


    // 모든 적 삭제
    void RemoveAllEnemy()
    {
        Enemy[] enemies =
            FindObjectsByType<Enemy>(FindObjectsSortMode.None);


        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }


    // 적 스폰 중지
    void StopEnemySpawn()
    {
        EnemySpawn spawn =
            FindFirstObjectByType<EnemySpawn>();


        if (spawn != null)
        {
            spawn.StopSpawn();
        }
    }


    void CreateNextObject()
    {
        nextStageObject.SetActive(true);


        if (spawnPosition != null)
        {
            nextStageObject.transform.position =
                spawnPosition.position;
        }
    }


    public void NextStage()
    {
        Time.timeScale = 1;

        int nextScene =
            SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextScene);
    }
}