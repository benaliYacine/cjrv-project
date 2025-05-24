using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_swim : MonoBehaviour
{
    // Movement speeds
    public float swimSpeed = 5f;
    public float sprintSpeed = 10f;

    // Speed boost variables
    private float currentSpeedMultiplier = 1f;
    private float boostEndTime = 0f;
    private bool isBoosted = false;

    // Input keys
    public string inputFront = "w";
    public string inputBack = "s";
    public string inputLeft = "a";
    public string inputRight = "d";
    public string inputUp = "space"; // Swim upward
    public string inputDown = "left shift"; // Swim downward

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing on " + gameObject.name);
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Disable gravity for swimming
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (rb == null) return;

        // Check if speed boost has expired
        if (isBoosted && Time.time >= boostEndTime)
        {
            currentSpeedMultiplier = 1f;
            isBoosted = false;
        }

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && Input.GetKey(inputFront);
        float currentSpeed = isSprinting ? sprintSpeed : swimSpeed;
        currentSpeed *= currentSpeedMultiplier; // Apply speed boost multiplier

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(inputFront)) moveDir += transform.forward;
        if (Input.GetKey(inputBack)) moveDir -= transform.forward;
        if (Input.GetKey(inputLeft)) moveDir -= transform.right;
        if (Input.GetKey(inputRight)) moveDir += transform.right;
        if (Input.GetKey(inputUp)) moveDir += transform.up;
        if (Input.GetKey(inputDown)) moveDir -= transform.up;

        if (moveDir != Vector3.zero)
        {
            transform.Translate(moveDir.normalized * currentSpeed * Time.deltaTime, Space.World);
        }
    }

    // Add method to handle speed boosts
    public void ApplySpeedBoost(float multiplier, float duration)
    {
        currentSpeedMultiplier = multiplier;
        boostEndTime = Time.time + duration;
        isBoosted = true;
    }
}
