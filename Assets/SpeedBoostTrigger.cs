using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    [Tooltip("How much to increase the speed (multiplier)")]
    public float speedBoostMultiplier = 1.5f;
    
    [Tooltip("How long the speed boost lasts in seconds")]
    public float boostDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has either marche or movement_swim component
        if (gameObject.CompareTag("Boost"))
        {
            // Try to get marche component
            marche walkingMovement = other.GetComponent<marche>();
            if (walkingMovement != null)
            {
                walkingMovement.ApplySpeedBoost(speedBoostMultiplier, boostDuration);
            }

            // Try to get movement_swim component
            movement_swim swimmingMovement = other.GetComponent<movement_swim>();
            if (swimmingMovement != null)
            {
                swimmingMovement.ApplySpeedBoost(speedBoostMultiplier, boostDuration);
            }
        }
    }
}