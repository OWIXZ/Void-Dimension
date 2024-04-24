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
    public bool AbilitiesDash = false;
    private float dashSpeed = 15f;
    private float dashingTime = 0.4f;
    private float dashingCooldown = 1f;
    private float JumpingCooldown = 1.1f;
    private float tm;
    private IEnumerator coroutine;

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
    [SerializeField] ParticleSystem DashParticleR;
    [SerializeField] ParticleSystem DashParticleL;

    [Header("Sound")]
    public AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       respawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        Ground_Detection();
        PlayerOneController();
        Jump();
        DashAction();
    }

    IEnumerator Dash()
    {
        //isMooving = false;
        canDash = false;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.left * dashSpeed;
            DashParticleL.Play();
        }
        else
        {
            rb.velocity = Vector2.right * dashSpeed;
            DashParticleR.Play();
        }
        
        yield return new WaitForSeconds(dashingTime);
        // Animator_player.SetBool("BoolDash", false);
        rb.gravityScale = originalGravity;
        //isMooving = true;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
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


    void PlayerOneController()
    {
        if (isMooving == true)
        {
            dashSpeed = 15f;
            moveSpeed = 10f;
        }
        if (isMooving == false)
        {
            dashSpeed = 0f;
            moveSpeed = 0;
        }

     

            //-----------------Deplacement -----------------                                                                                                  //when I press the chosen keys, I can move around and launch the corresponding animation


            if (Input.GetKey(KeyCode.D) && isMooving)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = false;                                                                          //flip the direction of the animation 
        }

        else if (Input.GetKey(KeyCode.A) && isMooving)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = true;                                                                            //flip the direction of the animation
        }
        else                                                                                                        //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolRun", false);
        }



        if (Input.GetKeyDown(KeyCode.W) && isMooving && canSwitch == true)
        {
            audioManager.PlaySFX(audioManager.portal);
            ShockWave.Play();
            StartCoroutine(Switch());
        }
           
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && isMooving && canJump)
        {
            audioManager.PlaySFX(audioManager.jump);
            dust.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            Player_Animator.SetBool("BoolJump", true);                                                             //play jump animation
            StartCoroutine(JumpCo());
        }

        else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolJump", false);
        }
    }

    private void DashAction()
    {
        //-----------------Dash-----------------  
        if (Input.GetKey(KeyCode.LeftShift) && canDash && isMooving && AbilitiesDash)
        {
            audioManager.PlaySFX(audioManager.dash);
            StartCoroutine(Dash());
            Player_Animator.SetBool("BoolDash", true);
        }

        //-----------------Animation-----------------

        else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolDash", false);
        }
    }


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
       
    }
}


