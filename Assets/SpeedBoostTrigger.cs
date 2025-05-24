using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    [Tooltip("How much to increase the speed (multiplier)")]
    public float speedBoostMultiplier = 1.5f;
    
    [Tooltip("How long the speed boost lasts in seconds")]
    public float boostDuration = 3f;

    // reference the AudioSource on this same object
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogWarning("No AudioSource found on SpeedBoostTrigger!");
    }

    private void OnTriggerEnter(Collider other)
    {
        // only trigger on objects tagged "Boost"
        if (!CompareTag("Boost"))
            return;

        // play pickup sound
        if (audioSource != null)
            audioSource.PlayOneShot(audioSource.clip);

        // apply boost to walking
        var walk = other.GetComponent<marche>();
        if (walk != null)
            walk.ApplySpeedBoost(speedBoostMultiplier, boostDuration);

        // apply boost to swimming
        var swim = other.GetComponent<movement_swim>();
        if (swim != null)
            swim.ApplySpeedBoost(speedBoostMultiplier, boostDuration);

        // (optional) destroy or disable the boost object so it can only be picked once
        // Destroy(gameObject);
    }
}
