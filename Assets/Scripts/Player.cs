using UnityEngine;
using System.Collections;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float maxSpeed;            // Maximum horizontal speed
    [SerializeField] private float accelerationRate;    // Rate at which the player accelerates (units per second^2)
    [SerializeField] private float decelerationRate;    // Rate at which the player decelerates (units per second^2)
    [SerializeField] private float jumpForce;           // Jump force applied when jumping
    [SerializeField] private Transform freeLookCamera;  // Assign the FreeLook Camera's Transform in the Inspector

    // Dash parameters
    [SerializeField] private float dashDistance;        // Distance covered during dash
    [SerializeField] private float dashDuration;        // Duration of the dash in seconds
    [SerializeField] private float dashCooldown;        // Cooldown time after dash (e.g., 5 seconds)

    private Rigidbody rb;
    private Vector3 movementInput; // Raw movement input from the InputManager

    private bool isGrounded = false;      // Is player on the ground
    private bool doubleJumpUsed = false;  // Is double jump used

    // Dash variables
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 dashDirection = Vector3.zero;
    private float dashSpeed = 0f; // Calculated as dashDistance / dashDuration

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Adding MovePlayer as a listener to OnMove event
        inputManager.OnMove.AddListener(ReceiveMovementInput);
        inputManager.OnSpacePressed.AddListener(Jump);
        inputManager.OnDash.AddListener(Dash);
        rb = GetComponent<Rigidbody>();
    }
    private void ReceiveMovementInput(Vector3 input)
    {
        if (isDashing)
            return;
        movementInput = input;
    }
    void FixedUpdate()
    {
        if (isDashing)
        {
            // Override horizontal velocity with dash velocity
            Vector3 newHorizontalVelocity = dashDirection * dashSpeed;
            rb.linearVelocity = new Vector3(newHorizontalVelocity.x, rb.linearVelocity.y, newHorizontalVelocity.z);

            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
                // Clear horizontal velocity when dash ends
                rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            }
            rb.MoveRotation(Quaternion.Euler(0, freeLookCamera.eulerAngles.y, 0));
            return;
        }
        else
        {
            // Update dash cooldown timer
            if (dashCooldownTimer > 0f)
            {
                dashCooldownTimer -= Time.fixedDeltaTime;
            }

            // Calculate the camera's forward and right directions on the horizontal plane
            Vector3 camForward = freeLookCamera.forward;
            camForward.y = 0;
            camForward.Normalize();
            Vector3 camRight = freeLookCamera.right;
            camRight.y = 0;
            camRight.Normalize();

            // Compute the desired horizontal velocity based on input and camera orientation
            Vector3 desiredDirection = (movementInput.x * camRight + movementInput.z * camForward);
            // Normalize to prevent faster diagonal movement; if no input, desiredDirection is zero
            desiredDirection = desiredDirection.magnitude > 0 ? desiredDirection.normalized : Vector3.zero;
            Vector3 desiredVelocity = desiredDirection * maxSpeed;

            // Extract the current horizontal velocity (ignoring the vertical component)
            Vector3 currentVelocity = rb.linearVelocity;
            Vector3 currentHorizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);

            // Choose the appropriate rate: accelerate if input exists; otherwise, decelerate
            float rate = (movementInput.magnitude > 0) ? accelerationRate : decelerationRate;

            // Gradually change the horizontal velocity toward the desired velocity
            Vector3 newHorizontalVelocity = Vector3.MoveTowards(currentHorizontalVelocity, desiredVelocity, rate * Time.fixedDeltaTime);

            // Apply the updated horizontal velocity while preserving the current vertical velocity
            rb.linearVelocity = new Vector3(newHorizontalVelocity.x, rb.linearVelocity.y, newHorizontalVelocity.z);
            // Update player's rotation to match the free look camera's horizontal orientation
            rb.MoveRotation(Quaternion.Euler(0, freeLookCamera.eulerAngles.y, 0));
        }
    }

    private void Jump()
    {
        // If the player is grounded, perform the first jump
        if (isGrounded)
        {
            // Reset vertical velocity before applying jump force
            Vector3 vel = rb.linearVelocity;
            vel.y = 0;
            rb.linearVelocity = vel;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            // Start coroutine to disable grounded state after a short delay
            StartCoroutine(DisableGrounded());
        }
        else if (!doubleJumpUsed)
        {
            // Reset vertical velocity before applying jump force
            Vector3 vel = rb.linearVelocity;
            vel.y = 0;
            rb.linearVelocity = vel;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            doubleJumpUsed = true;
        }
    }

    private IEnumerator DisableGrounded()
    {
        // Wait for a short duration to ensure the jump has been initiated
        yield return new WaitForSeconds(0.1f);
        isGrounded = false;
    }

    // Initiates the dash if cooldown allows
    private void Dash()
    {
        // Check if dash is off cooldown and not currently dashing
        if (dashCooldownTimer > 0f || isDashing)
        {
            return;
        }

        Vector3 dashDir;
        // Determine dash direction based on movement input
        if (movementInput.magnitude > 0)
        {
            // Calculate dash direction relative to the camera's orientation
            Vector3 camForward = freeLookCamera.forward;
            camForward.y = 0;
            camForward.Normalize();
            Vector3 camRight = freeLookCamera.right;
            camRight.y = 0;
            camRight.Normalize();

            Vector3 desiredDirection = (movementInput.x * camRight + movementInput.z * camForward);
            dashDir = desiredDirection.magnitude > 0 ? desiredDirection.normalized : camForward;
        }
        else
        {
            // If no movement input, dash in the direction the camera is facing
            dashDir = freeLookCamera.forward;
            dashDir.y = 0;
            dashDir.Normalize();
        }

        dashDirection = dashDir;
        dashSpeed = dashDistance / dashDuration;
        isDashing = true;
        dashTimer = dashDuration;
    }

    // Returns true if the player can dash (not dashing and dash cooldown expired)
    public bool IsDashAvailable
    {
        get { return !isDashing && dashCooldownTimer <= 0f; }
    }


    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJumpUsed = false;
        }
    }

}
