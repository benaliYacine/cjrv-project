using UnityEngine;

public class WinAudioOnStart : MonoBehaviour
{
    [Header("Win Sound Settings")]
    [Tooltip("Your win sound clip")]
    public AudioClip winClip;
    [Tooltip("Playback volume (0–1)")]
    [Range(0f, 1f)]
    public float volume = 1f;

    private AudioSource audioSource;

    void Awake()
    {
        // Grab or add an AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Configure it
        audioSource.clip        = winClip;
        audioSource.playOnAwake = false;
        audioSource.loop        = false;
        audioSource.volume      = volume;
        audioSource.spatialBlend = 0f;  // 2D
    }

    void Start()
    {
        // Play immediately when the scene loads
        if (winClip != null)
            audioSource.Play();
    }
}
