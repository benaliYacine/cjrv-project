using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marche : MonoBehaviour
{
    // Animation component
    private Animation animations;

    // Movement speeds
    public float walkV = 5f;  // Walking speed
    public float runV = 10f;  // Running speed
    public float jumpForce = 5f;    // Jump force
    public float gravityScale = 2f; // Increased gravity for faster fall

    // Speed boost variables
    private float currentSpeedMultiplier = 1f;
    private float boostEndTime = 0f;
    private bool isBoosted = false;

    // Input keys for movement
    public string inputFront = "w";
    public string inputBack  = "s";
    public string inputLeft  = "a";
    public string inputRight = "d";
    public string inputJump  = "space";

    // Physics components
    private Rigidbody rb;
    private bool isGrounded;

    [Header("Footstep Audio")]
    public AudioClip stepClip;      // assign your footstep loop clip here
    [Tooltip("Overall footstep volume")]
    public float stepVolume = 1f;
    [Tooltip("Pitch multiplier when walking")]
    public float walkPitch = 0.7f;
    [Tooltip("Pitch multiplier when running")]
    public float runPitch = 1f;
    private AudioSource stepSource;

    void Start()
    {
        // Get components
        animations = GetComponent<Animation>();
        rb         = GetComponent<Rigidbody>();

        if (animations == null)
            Debug.LogError("Animation component is missing on " + gameObject.name);
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Rigidbody setup
        rb.useGravity = true;
        rb.mass = 1f;
        rb.linearDamping  = 0f;
        rb.angularDamping = 0.05f;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // AudioSource setup
        stepSource = GetComponent<AudioSource>();
        if (stepSource == null)
            stepSource = gameObject.AddComponent<AudioSource>();

        if (stepClip != null)
        {
            stepSource.clip         = stepClip;
            stepSource.loop         = true;
            stepSource.playOnAwake  = false;
            stepSource.spatialBlend = 0f;       // 2D sound
            stepSource.volume       = stepVolume;
        }
        else
        {
            Debug.LogWarning("Step clip not assigned on " + gameObject.name);
        }
    }

    void Update()
    {
        if (animations == null || rb == null) return;

        // Check if speed boost has expired
        if (isBoosted && Time.time >= boostEndTime)
        {
            currentSpeedMultiplier = 1f;
            isBoosted = false;
        }

        // Determine if sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(inputFront);

        // Handle jumping
        if (Input.GetKeyDown(inputJump) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
        }

        // Custom gravity
        rb.AddForce(Physics.gravity * (gravityScale - 1), ForceMode.Acceleration);

        // Track movement input
        bool movedThisFrame = false;

        // Forward/back/strafe movement
        float currentWalkSpeed = walkV * currentSpeedMultiplier;
        float currentRunSpeed  = runV  * currentSpeedMultiplier;

        if (Input.GetKey(inputFront))
        {
            float speed = isSprinting ? currentRunSpeed : currentWalkSpeed;
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
            animations.Play(isSprinting ? "run" : "walk");
            movedThisFrame = true;
        }
        if (Input.GetKey(inputBack))
        {
            transform.Translate(0, 0, -currentWalkSpeed * Time.deltaTime, Space.Self);
            animations.Play("walk");
            movedThisFrame = true;
        }
        if (Input.GetKey(inputLeft))
        {
            transform.Translate(-currentWalkSpeed * Time.deltaTime, 0, 0, Space.Self);
            animations.Play("walk");
            movedThisFrame = true;
        }
        if (Input.GetKey(inputRight))
        {
            transform.Translate(currentWalkSpeed * Time.deltaTime, 0, 0, Space.Self);
            animations.Play("walk");
            movedThisFrame = true;
        }

        // Stop animation if no movement keys
        if (!movedThisFrame)
            animations.Stop();

        // FOOTSTEP AUDIO: adjust pitch & play/stop
        if (stepSource.clip != null)
        {
            if (movedThisFrame)
            {
                stepSource.pitch = isSprinting ? runPitch : walkPitch;
                if (!stepSource.isPlaying)
                    stepSource.Play();
            }
            else
            {
                if (stepSource.isPlaying)
                    stepSource.Stop();
            }
        }
    }

    // Ground check
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
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
