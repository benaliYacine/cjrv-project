using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player (character) transform
    public float mouseSensitivity = 100f; // Mouse sensitivity for rotation
    public float minPitch = -80f; // Minimum pitch angle (looking down)
    public float maxPitch = 80f; // Maximum pitch angle (looking up)

    private float pitch = 0f; // Current pitch angle

    void Start()
    {
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Ensure player reference is set
        if (player == null)
        {
            Debug.LogError("Player reference is not set in CameraController!");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Debug.Log($"Mouse X: {mouseX}, Mouse Y: {mouseY}"); // Debug input

        // Rotate player (yaw) based on horizontal mouse movement
        player.Rotate(0, mouseX, 0);

        // Update pitch (vertical rotation) for the camera
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Apply pitch rotation to the camera (local rotation)
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}