using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marche : MonoBehaviour
{
    // Animation component
    Animation animations;

    // Movement speeds
    public float walkV = 5f; // Walking speed
    public float runV = 10f; // Running speed
    public float jumpForce = 5f; // Jump force
    public float gravityScale = 2f; // Increased gravity for faster fall

    // Input keys for movement
    public string inputFront = "w"; // Key for moving forward
    public string inputBack = "s"; // Key for moving backward
    public string inputLeft = "a"; // Key for strafing left
    public string inputRight = "d"; // Key for strafing right
    public string inputJump = "space"; // Key for jumping

    // Physics components
    private Rigidbody rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Get components
        animations = gameObject.GetComponent<Animation>();
        rb = gameObject.GetComponent<Rigidbody>();

        // Check if components are attached
        if (animations == null)
        {
            Debug.LogError("Animation component is missing on " + gameObject.name);
        }
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
            rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        // Optimize Rigidbody for realistic physics
        rb.useGravity = true;
        rb.mass = 1f;
        rb.linearDamping = 0f;
        rb.angularDamping = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (animations == null || rb == null)
        {
            return; // Exit if components are missing
        }

        // Check if sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(inputFront);

        // Handle jumping
        if (Input.GetKeyDown(inputJump) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
            Debug.Log("Jump triggered!");
        }

        // Apply custom gravity scale for faster fall
        rb.AddForce(Physics.gravity * (gravityScale - 1), ForceMode.Acceleration);

        // Move forward
        if (Input.GetKey(inputFront))
        {
            if (isSprinting)
            {
                transform.Translate(0, 0, runV * Time.deltaTime);
                if (!animations.IsPlaying("run"))
                {
                    animations.Stop();
                    animations.Play("run");
                }
            }
            else
            {
                transform.Translate(0, 0, walkV * Time.deltaTime);
                if (!animations.IsPlaying("walk"))
                {
                    animations.Stop();
                    animations.Play("walk");
                }
            }
        }

        // Move backward
        if (Input.GetKey(inputBack))
        {
            transform.Translate(0, 0, -walkV * Time.deltaTime);
            if (!animations.IsPlaying("walk"))
            {
                animations.Stop();
                animations.Play("walk");
            }
        }

        // Strafe left
        if (Input.GetKey(inputLeft))
        {
            transform.Translate(-walkV * Time.deltaTime, 0, 0);
            if (!animations.IsPlaying("walk"))
            {
                animations.Stop();
                animations.Play("walk");
            }
        }

        // Strafe right
        if (Input.GetKey(inputRight))
        {
            transform.Translate(walkV * Time.deltaTime, 0, 0);
            if (!animations.IsPlaying("walk"))
            {
                animations.Stop();
                animations.Play("walk");
            }
        }

        // Stop animations when no keys are pressed
        if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack) &&
            !Input.GetKey(inputLeft) && !Input.GetKey(inputRight))
        {
            animations.Stop();
        }
    }

    // Check if character is grounded
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}