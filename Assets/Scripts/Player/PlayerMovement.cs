using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioClip dashSFX;
    [SerializeField] private PointEffector2D magnet;

    [Header("Event Channels")]
    [SerializeField] private EventAudioClip OnSFXRequestEvent;
    [SerializeField] private EventFloatFloat OnSpeedBoostEvent;
    private Coroutine speedBoostRoutine;

    [Header("Movement Attributes")]
    [SerializeField] private float walkForce = 5f;
    [SerializeField] private float sprintForce = 8f;
    [SerializeField] private float maxWalkSpeed = 2f;
    [SerializeField] private float maxSprintSpeed = 10f;
    [SerializeField] private float airControlMult = 0.6f;
    private Vector2 moveInput;
    private bool isSprinting;
    private float moveForce;
    private float maxSpeed;

    [Header("Jump Attributes")]
    [SerializeField] private int extraJumps = 0;
    [SerializeField] private float initialJumpForce = 2f;
    [SerializeField] private float continuedJumpForce = 1f;
    [SerializeField] private float coyoteDuration = 0.2f;
    [SerializeField] private float jumpBufferDuration = 0.15f;
    private int extraJumpsLeft;
    private float coyoteTimer;
    private float jumpBufferTimer;

    [Header("Dash Attributes")]
    [SerializeField] private int maxDashCharges = 1;
    [SerializeField] private float dashForce = 20f;
    [SerializeField] private float dashDuration = 0.15f;
    private bool isDashing;
    private bool canDash;
    private int dashCharges;

    public bool IsDashing => isDashing;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    [Header("Wall-Sliding")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.3f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpForceX = 8f;
    [SerializeField] private float wallJumpForceY = 10f;
    [SerializeField] private float wallJumpLockDuration = 0.15f;
    private bool isTouchingWall;
    private bool isWallSliding;
    private int wallDirection; // 0 -> None, 1 -> Right, -1 -> Left
    private float wallJumpLockTimer;

    private void OnEnable()
    {
        OnSpeedBoostEvent.Subscribe(OnSpeedBoost);
    }

    private void OnDisable()
    {
        OnSpeedBoostEvent.Unsubscribe(OnSpeedBoost);
    }

    private void Start()
    {
        //InventoryManager.Instance.player = this.gameObject;
        moveInput = Vector2.zero;
        isSprinting = false;
        moveForce = walkForce;
        maxWalkSpeed = playerData.baseMaxWalkSpeed;
        maxSprintSpeed = playerData.baseMaxSprintSpeed;
        maxSpeed = maxWalkSpeed;

        extraJumpsLeft = extraJumps;
        coyoteTimer = coyoteDuration;
        jumpBufferTimer = 0;
        
        isDashing = false;
        canDash = true;
        dashCharges = maxDashCharges;

        isGrounded = false;

        wallJumpLockTimer = 0f;
    }

    private void Update()
    {
        moveInput = InputSystem.actions["Move"].ReadValue<Vector2>();
        
        if (wallJumpLockTimer <= 0f && !isWallSliding)
        {
            if (moveInput.x < 0) sprite.flipX = true;
            else if (moveInput.x > 0) sprite.flipX = false;
        }
        else if (isWallSliding)
        {
            sprite.flipX = wallDirection > 0;
        }

        if (isGrounded)
        {
            extraJumpsLeft = extraJumps;
            coyoteTimer = coyoteDuration;
            dashCharges = maxDashCharges;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (InputSystem.actions["Jump"].WasPressedThisFrame())
        {
            jumpBufferTimer = jumpBufferDuration;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        if (wallJumpLockTimer > 0f)
        {
            wallJumpLockTimer -= Time.deltaTime;
        }

        if (jumpBufferTimer > 0f)
        {
            if (isWallSliding || (isTouchingWall && !isGrounded))
            {
                WallJump();
                jumpBufferTimer = 0f;
            }
            else if (coyoteTimer > 0f)
            {
                Jump();
                coyoteTimer = 0f;
                jumpBufferTimer = 0f;
            }
            else if (extraJumpsLeft > 0)
            {
                extraJumpsLeft--;
                jumpBufferTimer = 0;
                Jump();
            }
        }

        if (InputSystem.actions["Jump"].IsPressed() && rb.linearVelocityY > 0f)
        {
            rb.AddForceY(continuedJumpForce);
        }

        if (InputSystem.actions["Dash"].WasPressedThisFrame() && !isDashing && dashCharges > 0 && canDash && !playerAttack.IsAttacking)
        {
            dashCharges--;
            canDash = false;
            OnSFXRequestEvent.Raise(dashSFX);
            StartCoroutine(Dash());
        }
        else if (InputSystem.actions["Dash"].IsPressed())
        {
            isSprinting = true;
        }
        else if (InputSystem.actions["Dash"].WasReleasedThisFrame())
        {
            isSprinting = false;
            canDash = true;
        }

        if (rb.linearVelocityY < 0f && !isDashing)
        {
            rb.gravityScale = 3f;
        }
        else if (!isDashing)
        {
            rb.gravityScale = 2f;
        }

        UpdateAnimatorParameters();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        bool touchingRightWall = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, wallLayer);
        bool touchingLeftWall = Physics2D.Raycast(wallCheck.position, Vector2.left, wallCheckDistance, wallLayer);
        isTouchingWall = touchingLeftWall || touchingRightWall;
        wallDirection = touchingRightWall ? 1 : (touchingLeftWall ? -1 : 0);

        isWallSliding = isTouchingWall && !isGrounded && !isDashing && rb.linearVelocityY < 0f && Mathf.Sign(moveInput.x) == wallDirection;

        if (!isDashing && wallJumpLockTimer <= 0f)
        {
            float control = isGrounded ? 1f : airControlMult;
            moveForce = isSprinting ? sprintForce : walkForce;
            maxSpeed = isSprinting ? maxSprintSpeed : maxWalkSpeed;
            rb.AddForce(Vector2.right * moveInput * moveForce * control);

            float clampedX = Mathf.Clamp(rb.linearVelocityX, -maxSpeed, maxSpeed);
            rb.linearVelocity = new Vector2(clampedX, rb.linearVelocityY);
        }

        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, Mathf.Max(rb.linearVelocityY, -wallSlideSpeed));
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
        rb.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
    }

    private void WallJump()
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(-wallDirection * wallJumpForceX, wallJumpForceY), ForceMode2D.Impulse);
        wallJumpLockTimer = wallJumpLockDuration;
        sprite.flipX = wallDirection > 0;
    }

    private System.Collections.IEnumerator Dash()
    {
        rb.gravityScale = 0f;
        isDashing = true;
        animator.SetBool("IsDashing", true);

        float direction = sprite.flipX ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * dashForce, 0f);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        animator.SetBool("IsDashing", false);
        rb.gravityScale = 2f;
    }

    private void OnSpeedBoost(float duration, float multiplier)
    {
        if (speedBoostRoutine != null)
        {
            StopCoroutine(speedBoostRoutine);
        }

        speedBoostRoutine = StartCoroutine(SpeedBoostRoutine(duration, multiplier));
    }

    private System.Collections.IEnumerator SpeedBoostRoutine(float duration, float multiplier)
    {
        maxWalkSpeed = playerData.baseMaxWalkSpeed * multiplier;
        maxSprintSpeed = playerData.baseMaxSprintSpeed * multiplier;
        yield return new WaitForSeconds(duration);
        maxWalkSpeed = playerData.baseMaxWalkSpeed;
        maxSprintSpeed = playerData.baseMaxSprintSpeed;
        speedBoostRoutine = null;
    }

    private void UpdateAnimatorParameters()
    {
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsMoving", Mathf.Abs(moveInput.x) > 0.1f && isGrounded);
        animator.SetBool("IsJumping", !isGrounded && rb.linearVelocityY > 0.1f);
        animator.SetBool("IsFalling", !isGrounded && rb.linearVelocityY < -0.1f);
        animator.SetBool("IsWallSliding", isWallSliding);
    }

    public void EnableItemCollector()
    {
        magnet.enabled = true;
    }

    public void DisableItemCollector()
    {
        magnet.enabled = false;
    }
}
