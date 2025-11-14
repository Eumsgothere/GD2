using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public PlayerSc player; // Assign in Inspector

    [Header("Gameplay")]
    public int targetCount = 0; // tracks remaining targets

    private bool hasWon = false;

    private void Update()
    {
        // Automatically check for win
        if (targetCount <= 0 && !hasWon)
        {
            Debug.Log("YOU WIN!");
            hasWon = true;

            if (player != null)
                StartCoroutine(TriggerWinCoroutine());
        }
    }

    private IEnumerator TriggerWinCoroutine()
    {
        yield return null; // wait one frame to ensure Animator is ready
        player.Win();
    }
    public void CheckWin()
    {
        if (targetCount <= 0 && !hasWon)
        {
            Debug.Log("YOU WIN!");
            hasWon = true;

            if (player != null)
                StartCoroutine(TriggerWinCoroutine());
        }
    }

    private void OnGUI()
    {
        if (hasWon)
        {
            int width = 200;
            int height = 50;

            int x = (Screen.width - width) / 2;
            int y = (Screen.height - height) / 2;

            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 40;
            style.normal.textColor = Color.yellow;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(new Rect(x, y, width, height), "YOU WIN!", style);
        }
    }
}
