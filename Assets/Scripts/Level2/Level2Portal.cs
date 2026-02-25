using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Portal : MonoBehaviour
{
    [Header("Travel Settings")]
    [Tooltip("Type the exact name of the scene you want to load next (e.g., Level3 or WinScene)")]
    [SerializeField] private string nextLevelName; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Level 2 adjustment: Ensure your Player2 object has the "Player" tag!
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player 2 reached the portal! Moving to: " + nextLevelName);
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogError("Level2Portal: Next Level Name is empty! Set it in the Inspector.");
        }
    }
}