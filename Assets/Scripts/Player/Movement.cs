using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] Animator Player_Animator;

    [Header("Dashing proprieties")]
    [SerializeField] bool canJump = true;
    [SerializeField] bool canDash = true;
    private bool canTurn = true;
    [SerializeField] bool isDashing;
    [SerializeField] private bool canJumpAfterDash = true;
    public bool isJumping = false;
    public bool AbilitiesDash = false;
    private float dashingCooldown = 0.5f;
    private float JumpingCooldown = 0.1f;
    private float startDashTime = 1f;
    private float tm;
    private IEnumerator coroutine;


    private float horizontal;
    private bool isFacingRight = true;

    [Header("Respawn")]
    public Vector2 respawnPoint;
    public GameObject FallDetector;
    public Checkpoint checkpoint;

    [Header("Movement")]

    private float moveSpeed = 10f;
    [SerializeField] int jumpPower;
    public PlayerInput playerInput;
    private bool wasInAir = false;


    bool isGrounded = false;
    public bool isMooving = true;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    [Header("Ground")]
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundSize;

    [Header("Particule")]
    [SerializeField] ParticleSystem dust;
    [SerializeField] ParticleSystem DashParticle;

    [Header("Sound")]
    public AudioManager audioManager;

    private void Awake()
    {
        isFacingRight = true;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    void Update()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        Fall();
        Up();
    }

    void FixedUpdate()
    {
        Ground_Detection();
        PlayerOneController();
    }

    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        canTurn = false;
        isDashing = true;
        canJumpAfterDash = false;
        Player_Animator.SetBool("BoolDash", true);
        rb.gravityScale = 0f;

        float localDashTime = 0.4f;
        float enhancedDashSpeed = 20f;
        DashParticle.Play();

        while (localDashTime > 0f)
        {
            localDashTime -= Time.deltaTime;
            rb.velocity = direction * enhancedDashSpeed;
            yield return null;
        }

        rb.velocity = new Vector2(0f, 0f);
        Player_Animator.SetBool("BoolDash", false);
        isDashing = false;
    
        rb.gravityScale = 1.7f;
       
        yield return new WaitForSeconds(0.1f);
        canJumpAfterDash = true;
        canTurn = true;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }



    IEnumerator JumpCo()
    {
        canJump = false;
        yield return new WaitForSeconds(JumpingCooldown);
        canJump = true;
    }

    IEnumerator JumpAnim()
    {
        yield return new WaitForSeconds(0.5f);
       Player_Animator.SetBool("BoolJump", false);
    }

    void PlayerOneController()
    {
        if (isMooving == true)
        {
            moveSpeed = 10f;
        }
        if (isMooving == false)
        {
            moveSpeed = 0;
        }


    }

    private void Flip()
    {
        if (canTurn) // Ne permet le retournement que si canTurn est vrai
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Fall()
    {
        if (!isGrounded && rb.velocity.y < 0f)
        {
            Player_Animator.SetBool("BoolFall", true);
        }
        else
        {
            Player_Animator.SetBool("BoolFall", false);
        }
    }

    private void Up()
    {
        if (rb.velocity.y > 0f && isJumping == false)
        {
            Player_Animator.SetBool("BoolJump2", true);
        }
        else
        {
            Player_Animator.SetBool("BoolJump2", false);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (!PauseMenu.gameIsPaused)
        {
            if (isMooving)
            {
                horizontal = context.ReadValue<Vector2>().x;
                Player_Animator.SetBool("BoolRun", horizontal != 0);
            }
            if (context.canceled)
            {
                horizontal = 0;  // Reset horizontal movement
                Player_Animator.SetBool("BoolRun", false);
            }
        }
    }

    public void ResetMovement()
    {
        horizontal = 0;
        Player_Animator.SetBool("BoolRun", false);
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && isMooving && canJump && canJumpAfterDash)
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                Player_Animator.SetBool("BoolJump", true);
                audioManager.PlaySFX(audioManager.jump);
                dust.Play();
                isJumping = true;
                StartCoroutine(JumpCo());
                StartCoroutine(JumpAnim());

                // Déclencher les vibrations du gamepad
                StartVibration(0.01f, 0.1f);  // Intensité à 0.01, durée à 0.01 secondes
            }
        }
    }



    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash && isMooving && AbilitiesDash)
        {
            if (context.performed && isDashing == false)
            {
                Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
                audioManager.PlaySFX(audioManager.dash);
                Player_Animator.SetBool("BoolDash", true);
                StartCoroutine(Dash(direction));
                StartVibration(0.03f, 0.18f);
            }
        }
    }

    private void Ground_Detection()
    {
        bool previouslyGrounded = isGrounded;
        Collider2D[] Wall_Detection = Physics2D.OverlapBoxAll(groundPosition.position, groundSize, 0, groundLayer);
        isGrounded = false;
        foreach (var Object in Wall_Detection)
        {
            if (Object.tag == "Ground")
            {
                isGrounded = true;
                break;
            }
        }

        // Joue la particule si le joueur retouche le sol après avoir été en l'air
        if (!previouslyGrounded && isGrounded && wasInAir)
        {
            dust.Play();
            wasInAir = false; // Réinitialiser l'état
        }

        // Mettre à jour l'état de wasInAir
        if (!isGrounded)
        {
            wasInAir = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            transform.position = respawnPoint;
            rb.velocity = Vector2.zero;
            ResetAllPlatforms(); // Réinitialise toutes les plateformes, actives ou non
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            respawnPoint = transform.position;
        }
    }

    void ResetAllPlatforms()
    {
        PlatformFall[] platforms = FindObjectsOfType<PlatformFall>();
        foreach (PlatformFall platform in platforms)
        {
            platform.ResetPosition(); // Force la réinitialisation même si désactivée
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            Player_Animator.SetBool("BoolJump", false);
            isJumping = false;
        }
    }




    private void StartVibration(float intensity, float duration)
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(intensity, intensity);
            StartCoroutine(StopVibration(gamepad, duration));
        }
    }

    private IEnumerator StopVibration(Gamepad gamepad, float delay)
    {
        yield return new WaitForSeconds(delay);
        gamepad.ResetHaptics();
    }
}
