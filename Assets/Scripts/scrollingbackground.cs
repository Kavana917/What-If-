using UnityEngine;

public class scrollingbackground : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed = 0.1f; 
    
    private Transform camTransform;
    private Vector3 lastCameraPosition;
    private Material mat;

    void Start()
    {
        // Get the material from the MeshRenderer
        mat = GetComponent<Renderer>().material;
        
        // Follow the Camera instead of the Player for smoother movement
        camTransform = Camera.main.transform;
        lastCameraPosition = camTransform.position;
    }

    void Update()
    {
        // Calculate camera movement
        float movement = camTransform.position.x - lastCameraPosition.x;

        // Offset the texture based on camera movement
        Vector2 offset = mat.mainTextureOffset;
        offset.x += movement * parallaxSpeed;
        mat.mainTextureOffset = offset;

        // Keep the Quad physically moving with the camera
        transform.position = new Vector3(camTransform.position.x, transform.position.y, transform.position.z);

        lastCameraPosition = camTransform.position;
    }
}