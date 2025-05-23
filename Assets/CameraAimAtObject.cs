using UnityEngine;

public class CameraAimAtObject : MonoBehaviour
{
    public Transform target; // The object the camera will aim at

    void Update()
    {
        if (target != null)
        {
            // Make the camera look at the target
            transform.LookAt(target);
        }
        else
        {
            Debug.LogWarning("No target assigned for the camera to aim at.");
        }
    }
}