using UnityEngine;

public class Portal : MonoBehaviour
{
    public StageManager stageManager;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            stageManager.NextStage();
        }
    }
}