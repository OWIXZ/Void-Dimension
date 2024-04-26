using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public CheckPoint checkpoint;

    //-----------------ANIM-----------------
    [Header("Animator")]
    [SerializeField] SpriteRenderer sprite_renderer;                                           //I enter the differents variables
    [SerializeField] Animator Player_Animator;                                                 //these bool variables allow me to bridge the gap between animation and code

    //-----------------MOVEMENT-----------------
    [Header("Dashing proprieties")]
    [SerializeField] bool canJump = true;
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] private bool canJumpAfterDash = true;
    public bool isJumping = false;
    public bool AbilitiesDash = false;
    private float dashingCooldown = 0.5f;
    private float JumpingCooldown = 0.5f;
    private float startDashTime = 1f;
    private float tm;
    private IEnumerator coroutine;


    private float horizontal;
    private bool isFacingRight = true;

    [Header("Respawn")]
    public Vector2 respawnPoint;
    public GameObject FallDetector;

    [Header("Movement")]
    private float moveSpeed = 10f;
    [SerializeField] int jumpPower;
    public bool canSwitch = false;
    [SerializeField] float SwitchingCooldown = 1;

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
    [SerializeField] ParticleSystem ShockWave;
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
    }


    void FixedUpdate()
    {
        Ground_Detection();
        PlayerOneController();
    }



    IEnumerator Dash(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        canJumpAfterDash = false;  // Désactiver la capacité de sauter immédiatement après le dash
        Player_Animator.SetBool("BoolDash", true);

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

        yield return new WaitForSeconds(0.1f); // Attendre 0.1 seconde avant de permettre de nouveau le saut
        canJumpAfterDash = true;  // Réactiver la capacité de sauter

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }






    IEnumerator Switch()
    {
        canSwitch = false;
        tm = Time.time;
        yield return new WaitForSeconds(SwitchingCooldown);
        canSwitch = true;
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

     




        if (Input.GetKeyDown(KeyCode.W) && isMooving && canSwitch == true)
        {
            audioManager.PlaySFX(audioManager.portal);
            ShockWave.Play();
            StartCoroutine(Switch());
        }
           
    }




    //-----------------FLIP-----------------


    private void Flip()                                                      // ============== FLIP [NEW]
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }


    private void FALL()
    {
        if (isGrounded == false && isJumping == true)
        {
            Player_Animator.SetBool("BoolFall", true);
        }
        else
        {
            Player_Animator.SetBool("BoolFall", false);
        }
    }

    //-----------------MOVEMENT-----------------


    public void Move(InputAction.CallbackContext context)
    {
        if (isMooving)
        {
            horizontal = context.ReadValue<Vector2>().x;
            Player_Animator.SetBool("BoolRun", true);
        }

        if (context.canceled)
        {
            Player_Animator.SetBool("BoolRun", false);
        }
    }






    //-----------------JUMP-----------------




    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && isMooving && canJump && canJumpAfterDash)  // Vérifier également canJumpAfterDash
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
            }
        }
    }







    //-----------------DASH-----------------

    public void Dash(InputAction.CallbackContext context)                  // ============== NEW DASHING SYSTEM
    {
        if (canDash && isMooving && AbilitiesDash)
        {
            if (context.performed && canDash == true && isDashing == false)
            {
                if (isFacingRight && canDash == true && isDashing == false)
                {
                    audioManager.PlaySFX(audioManager.dash);
                    Player_Animator.SetBool("BoolDash", true);
                    StartCoroutine(Dash(Vector2.right));
                }
                else if (!isFacingRight && canDash == true && isDashing == false)
                {
                    audioManager.PlaySFX(audioManager.dash);
                    Player_Animator.SetBool("BoolDash", true);
                    StartCoroutine(Dash(Vector2.left));
                }
            }
        }
    }


    //-----------------GROUND-----------------



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundPosition.position, groundSize);
    }

    private void Ground_Detection()
    {

        Collider2D[] Wall_Detection = Physics2D.OverlapBoxAll(groundPosition.position, groundSize, groundLayer);

        isGrounded = false;

        foreach (var Object in Wall_Detection)
        {

            if (Object.tag == "Ground")
            {
                isGrounded = true;
            }
        }
    }






    //-----------------COLLIDER-----------------




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            audioManager.PlaySFX(audioManager.death);
            transform.position = respawnPoint;
        }


        if (collision.tag == "Checkpoint" && checkpoint.CheckPointON == true)
        {
            checkpoint.checkPointPass();
            respawnPoint = transform.position;
        }

        if (collision.tag == "Checkpoint" && checkpoint.CheckPointON == false)
        {
            respawnPoint = transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);  // Ajouter pour tester
        if (collision.gameObject.CompareTag("Ground") && isJumping)
        {
            Player_Animator.SetBool("BoolJump", false);
            isJumping = false;
        }
    }
}


