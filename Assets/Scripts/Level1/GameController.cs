using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject player;

    public static Action<GameObject> onPlayerSpawned;

    private void Awake()
    {
        // player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }


private void Start() // Use Start to ensure everything is ready
{
    GameObject player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    onPlayerSpawned?.Invoke(player); 
}

    private void OnEnable()
    {
        PlayerHealth.onPlayerDie += ResetScene;
    }

    private void OnDisable()
    {
        PlayerHealth.onPlayerDie -= ResetScene;
    }

    private void ResetScene()
    {
        Invoke("ResetSceneDelay", 1f); // 2-second delay before restart
    }

    private void ResetSceneDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}