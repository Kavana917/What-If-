using UnityEngine;
using Unity.Cinemachine; 

public class Setcamera2follow : MonoBehaviour
{
    private CinemachineCamera vcam; 

    private void Awake()
    {
        // Get the Cinemachine Camera component attached to this GameObject
        vcam = GetComponent<CinemachineCamera>(); 
    }

    private void OnEnable()
    {
        // UPDATED: Listen for the Level 2 specific spawn event
        Game2Controller.onPlayer2Spawned += HandlePlayerSpawned; 
    }

    private void OnDisable()
    {
        // UPDATED: Unsubscribe to prevent errors when the scene closes
        Game2Controller.onPlayer2Spawned -= HandlePlayerSpawned; 
    }

    private void HandlePlayerSpawned(GameObject player)
    {
        if (vcam != null && player != null)
        {
            // Cinemachine v3: Access the TrackingTarget through the Target property
            vcam.Target.TrackingTarget = player.transform; 
            
            // This forces the camera to jump to the player instantly so it doesn't "slide" from (0,0,0)
            vcam.ForceCameraPosition(player.transform.position, Quaternion.identity); 
            
            Debug.Log("Cinemachine v3: Level 2 Camera successfully linked to " + player.name);
        }
    }
}