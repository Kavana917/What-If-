using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Game2Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance;

    // Updated action naming for Level 2 consistency
    public static Action<GameObject> onPlayer2Spawned;

    private void Start() 
    {
        // Spawns the Level 2 Player (Player2)
        playerInstance = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        
        // Notify other scripts (like HUD2 or Camera2) that the player is ready
        onPlayer2Spawned?.Invoke(playerInstance); 
    }

    private void OnEnable()
    {
        // Subscribe to Player2Health events instead of PlayerHealth
        Player2Health.onPlayerDie += ResetLevel2;
    }

    private void OnDisable()
    {
        // Unsubscribe to prevent memory leaks or errors when changing scenes
        Player2Health.onPlayerDie -= ResetLevel2;
    }

    private void ResetLevel2()
    {
        // Short delay to allow death animations or sound effects to play
        Invoke("ResetSceneDelay", 1.5f); 
    }

    private void ResetSceneDelay()
    {
        // Reloads the current active scene (Level 2)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}