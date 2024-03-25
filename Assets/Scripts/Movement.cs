using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //-----------------ANIM-----------------
    
    [SerializeField] SpriteRenderer sprite_renderer;                                           //I enter the differents variables
    [SerializeField] Animator Player_Animator;                                                                     //these bool variables allow me to bridge the gap between animation and code
    //private bool BoolJump;
    //private bool BoolDeath;
    //private bool BoolAttack;

    //-----------------MOVEMENT-----------------
    [Header("Dashing proprieties")]
    [SerializeField] bool canDash = true;
    
    [SerializeField] bool isDashing;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float tm;
    private IEnumerator coroutine;

    [Header("Respawn")]
    private Vector2 respawnPoint;
    public GameObject FallDetector;

    [Header("Movement")]
    private float moveSpeed = 10f;
    [SerializeField] int jumpPower;

    bool isGrounded;
    public bool isMooving = true;


    //[SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    //-----------------SOUND-----------------

    //public AudioSource footstepsSound;

    //-----------------Particule Systeme-----------------

    /*
    [Header("Particule")]
    [SerializeField] ParticleSystem psJump;
    [SerializeField] ParticleSystem psRun;
    [SerializeField] ParticleSystem Particle_VFX;
    */
    //Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    //Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate()
    {
        PlayerOneController();
        Jump();
        DashAction();
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (spriteRenderer.flipX == true)
        {
            rb.velocity = Vector2.left * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.right * dashSpeed;
        }

        yield return new WaitForSeconds(dashingTime);
        // Animator_player.SetBool("Bool_Dash", false);
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
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
      

        if (Input.GetKey(KeyCode.D) && isMooving == true)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);                                                              //play run animation
            Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = false;                                                                          //flip the direction of the animation

        }

        else if (Input.GetKey(KeyCode.A) && isMooving == true)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            //rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = true;                                                                            //flip the direction of the animation
                                                                                                                     //fx-particle
        }
        else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolRun", false);
        }

    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && isMooving == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            Player_Animator.SetBool("BoolJump", true);                                                             //play jump animation
        }
        else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolJump", false);
        }
    }

    private void DashAction()
    {

        //-----------------Dash-----------------  
        if (Input.GetKey(KeyCode.LeftShift) && canDash == true && isMooving == true)
        {
            StartCoroutine(Dash());
            //Animator_player.SetBool("Bool_Dash", true);
        }

        //-----------------Animation-----------------

        /*else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolRun", false);
        }*/
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }
}


