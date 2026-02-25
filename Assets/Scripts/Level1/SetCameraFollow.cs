using UnityEngine;
using Unity.Cinemachine; //

public class SetCameraFollow : MonoBehaviour
{
    private CinemachineCamera vcam; //

    private void Awake()
    {
        vcam = GetComponent<CinemachineCamera>(); //
    }

    private void OnEnable()
    {
        GameController.onPlayerSpawned += HandlePlayerSpawned; //
    }

    private void OnDisable()
    {
        GameController.onPlayerSpawned -= HandlePlayerSpawned; //
    }

    private void HandlePlayerSpawned(GameObject player)
    {
        if (vcam != null && player != null)
        {
            // In v3, we access the TrackingTarget through the Target property
            vcam.Target.TrackingTarget = player.transform; //
            
            // This forces the camera to jump to the player instantly
            vcam.ForceCameraPosition(player.transform.position, Quaternion.identity); 
            
            Debug.Log("Cinemachine v3: Successfully linked to " + player.name);
        }
    }
}