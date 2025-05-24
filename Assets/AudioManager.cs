using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioClip clickClip;
    AudioSource src;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            src = GetComponent<AudioSource>();
        }
        else Destroy(gameObject);
    }

    public void PlayClick() => src.PlayOneShot(clickClip);
}
