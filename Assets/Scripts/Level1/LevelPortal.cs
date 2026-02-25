using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private string nextLevelName; // Type "Level2" or your next scene name

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object entering the portal is the Player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the portal!");
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        // You can add a small delay or a fade-out here later
        SceneManager.LoadScene(nextLevelName);
    }
}