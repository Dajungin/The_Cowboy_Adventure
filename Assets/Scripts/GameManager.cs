using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // 시작할 때 게임오버 창 숨기기
        gameOverPanel.SetActive(false);

        Time.timeScale = 1;
    }

    public void GameOver()
    {
        // 모든 적 삭제
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        EnemySpawn spawn = FindFirstObjectByType<EnemySpawn>();

        if (spawn != null)
        {
            spawn.StopSpawn();
        }

        // 게임오버 UI 표시
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }


        // 게임 정지
        Time.timeScale = 0;
    }


    public void Retry()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }


}