using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class VideoSceneManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Enter the exact name of the scene to load after the video ends or is skipped.")]
    [SerializeField] private string nextSceneName = "Level1";

    private VideoPlayer vp;

    void Awake()
    {
        vp = GetComponent<VideoPlayer>();

        if (vp != null)
        {
            // 1. Ensure 'Play On Awake' is OFF to allow manual control via events
            vp.playOnAwake = false;

            // 2. Subscribe to the "Preparation Finished" and "Video Finished" events
            vp.prepareCompleted += OnVideoPrepared;
            vp.loopPointReached += FinishReached;
            
        }
        else
        {
            Debug.LogError("VideoSceneManager: No VideoPlayer component found on this GameObject!");
        }
    }

    // This runs automatically once Unity has preloaded the video
    void OnVideoPrepared(VideoPlayer source)
    {
        Debug.Log($"Video '{source.clip.name}' fully loaded. Playing now!");
        source.Play();
    }

    void FinishReached(VideoPlayer source)
    {
        LoadNextScene();
    }

    void Update()
    {
        // Skip logic using the New Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || 
                Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                LoadNextScene();
            }
        }
    }

    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("VideoSceneManager: Next Scene Name is empty! Please set it in the Inspector.");
        }
    }

    // Clean up events when the object is destroyed
    void OnDestroy()
    {
        if (vp != null)
        {
            vp.prepareCompleted -= OnVideoPrepared;
            vp.loopPointReached -= FinishReached;
        }
    }
}