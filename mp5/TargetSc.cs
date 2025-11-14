using UnityEngine;

public class TargetSc : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
            gameManager.targetCount++;
        else
            Debug.LogError("GameManager not found in the scene! Please add it.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.targetCount--;
            gameManager.CheckWin();
        }
    }
}
