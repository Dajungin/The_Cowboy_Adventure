using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // 씬 이름으로 이동
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    // 다음 씬으로 이동
    public void NextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextScene);
    }


    // 현재 씬 다시 시작
    public void RestartScene()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }


    // 게임 종료
    public void QuitGame()
    {
        Application.Quit();
    }
}