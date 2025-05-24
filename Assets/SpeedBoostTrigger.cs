using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    [Tooltip("How much to increase the speed (multiplier)")]
    public float speedBoostMultiplier = 1.5f;
    
    [Tooltip("How long the speed boost lasts in seconds")]
    public float boostDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has the marche component and if this object is tagged as Boost
        if (gameObject.CompareTag("Boost"))
        {
            marche playerMovement = other.GetComponent<marche>();
            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(speedBoostMultiplier, boostDuration);
            }
        }
    }
} 