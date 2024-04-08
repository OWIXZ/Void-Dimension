using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //-----------------ANIM-----------------

    [SerializeField] SpriteRenderer sprite_renderer;                                           //I enter the differents variables
    [SerializeField] Animator Player_Animator;                                                                     //these bool variables allow me to bridge the gap between animation and code
    Animation anim;
    //-----------------MOVEMENT-----------------
    [Header("Dashing proprieties")]
    [SerializeField] bool canJump = true;
    [SerializeField] bool canDash = true;
    private float dashSpeed = 15f;
    private float dashingTime = 0.4f;
    private float dashingCooldown = 1f;
    private float JumpingCooldown = 1.1f;
    private float tm;
    private IEnumerator coroutine;

    [Header("Respawn")]
    private Vector2 respawnPoint;
    public GameObject FallDetector;

    [Header("Movement")]
    private float moveSpeed = 10f;
    [SerializeField] int jumpPower;

    bool isGrounded = false;
    public bool isMooving = true;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private Transform groundPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundSize;

    //-----------------SOUND-----------------

    //public AudioSource footstepsSound;


    [Header("Particule")]
    [SerializeField] ParticleSystem dust;


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
        Ground_Detection();
        PlayerOneController();
        Jump();
        DashAction();
    }

    IEnumerator Dash()
    {
        isMooving = false;
        canDash = false;
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
        // Animator_player.SetBool("BoolDash", false);
        rb.gravityScale = originalGravity;
        isMooving = true;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }


    IEnumerator JumpCo()
    {
        canJump = false;
        yield return new WaitForSeconds(JumpingCooldown);
        canJump = true;
    }

    IEnumerator Animation()
    {
        dust.Play();
        yield return new WaitForSeconds(0.1f);
        dust.Stop();
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
            StartCoroutine(Animation());
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = false;                                                                          //flip the direction of the animation 
        }

        else if (Input.GetKey(KeyCode.A) && isMooving)
        {
            StartCoroutine(Animation());
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = true;                                                                            //flip the direction of the animation
        }
        else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolRun", false);
        }
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && isMooving && canJump)
        {
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
        if (Input.GetKey(KeyCode.LeftShift) && canDash && isMooving)
        {
            StartCoroutine(Dash());
            //Animator_player.SetBool("BoolDash", true);
        }

        //-----------------Animation-----------------

        /*else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolDash", false);
        }*/
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
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
}


