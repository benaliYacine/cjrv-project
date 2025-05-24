using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_swim : MonoBehaviour
{
    [Header("Movement Speeds")]
    public float swimSpeed = 5f;
    public float sprintSpeed = 10f;

    [Header("Speed Boost")]
    private float currentSpeedMultiplier = 1f;
    private float boostEndTime = 0f;
    private bool isBoosted = false;

    [Header("Input Keys (string names)")]
    public string inputFront = "w";
    public string inputBack  = "s";
    public string inputLeft  = "a";
    public string inputRight = "d";
    public string inputUp    = "space";     // Swim upward
    public string inputDown  = "left shift"; // Swim downward

    private Rigidbody rb;

    [Header("Swim Audio")]
    public AudioClip swimClip;    // assign your swimming loop clip here
    [Tooltip("You can push this above 1.0 to make it louder")]
    public float swimVolume = 2f;
    private AudioSource swimSource;

    void Start()
    {
        // Rigidbody setup
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // AudioSource setup
        swimSource = GetComponent<AudioSource>();
        if (swimSource == null)
        {
            Debug.LogWarning("No AudioSource on " + gameObject.name + "; adding one automatically.");
            swimSource = gameObject.AddComponent<AudioSource>();
        }

        if (swimClip != null)
        {
            swimSource.clip = swimClip;
            swimSource.loop = true;
            swimSource.playOnAwake = false;
            swimSource.spatialBlend = 0f;      // 2D sound
            swimSource.volume = swimVolume;    // set volume (can exceed 1.0 for extra loudness)
        }
        else
        {
            Debug.LogWarning("Swim clip not assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        if (rb == null) return;

        // Check boost expiration
        if (isBoosted && Time.time >= boostEndTime)
        {
            currentSpeedMultiplier = 1f;
            isBoosted = false;
        }

        // Determine current speed
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(inputFront);
        float currentSpeed = (isSprinting ? sprintSpeed : swimSpeed) * currentSpeedMultiplier;

        // Build movement vector
        Vector3 moveDir = Vector3.zero;
        if (Input.GetKey(inputFront)) moveDir += transform.forward;
        if (Input.GetKey(inputBack))  moveDir -= transform.forward;
        if (Input.GetKey(inputLeft))  moveDir -= transform.right;
        if (Input.GetKey(inputRight)) moveDir += transform.right;
        if (Input.GetKey(inputUp))    moveDir += transform.up;
        if (Input.GetKey(inputDown))  moveDir -= transform.up;

        // Apply movement
        if (moveDir != Vector3.zero)
        {
            transform.Translate(moveDir.normalized * currentSpeed * Time.deltaTime, Space.World);
        }

        // Play or stop swim audio based on movement
        bool isMoving = moveDir != Vector3.zero;
        if (isMoving)
        {
            if (!swimSource.isPlaying)
                swimSource.Play();
        }
        else
        {
            if (swimSource.isPlaying)
                swimSource.Stop();
        }
    }

    // Called by SpeedBoostTrigger
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        currentSpeedMultiplier = multiplier;
        boostEndTime = Time.time + duration;
        isBoosted = true;
    }
}
