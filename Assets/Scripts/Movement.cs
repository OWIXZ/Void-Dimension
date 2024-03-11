using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //-----------------ANIM-----------------
    
    [SerializeField] SpriteRenderer sprite_renderer;                                           //I enter the differents variables
    /*[SerializeField] Animator Player_Animator;
    private bool BoolRun;                                                                     //these bool variables allow me to bridge the gap between animation and code
    private bool BoolJump;
    private bool BoolDeath;
    private bool BoolAttack;
    */
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
    //private float rotateSpeed = 50f;             
    private float scale = 5f;
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
        PlayerOneController();
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
            moveSpeed = 5f;
            scale = 5f;
        }
        if (isMooving == false)
        {
            dashSpeed = 0f;
            moveSpeed = 0;
            scale = 0f;
        }

        //Speed                                                                                //these values allow me to increase or decrease my character's speed to adapt to different situations on the map
       /*
        if (Input.GetKey(KeyCode.Q))
        {
            moveSpeed = moveSpeed + 0.5f;
            Debug.Log(moveSpeed);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            moveSpeed = moveSpeed + -0.5f;
            Debug.Log(moveSpeed);
        }
        
        else if (Input.GetKey(KeyCode.E))
        {
            rotateSpeed = rotateSpeed + 0.5f;
            Debug.Log(rotateSpeed);
        }

        else if (Input.GetKey(KeyCode.R))
        {
            rotateSpeed = rotateSpeed + -0.5f;
            Debug.Log(rotateSpeed);
        }
        */

        //-----------------Death-----------------
        /*
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Player_Animator.SetBool("BoolDeath", true);
        }

        //-----------------Attack-----------------
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Player_Animator.SetBool("BoolAttack", true);
        }
        */
        //-----------------Translate-----------------                                                                                                  //when I press the chosen keys, I can move around and launch the corresponding animation
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            //rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            // Player_Animator.SetBool("BoolRun", true);
            sprite_renderer.flipX = true;                                                                            //flip the direction of the animation
                                                                                                                      //fx-particle
             /*Particle_VFX.Play();
             Particle_VFX.transform.eulerAngles = new Vector3(0, -90, 0);

             footstepsSound.enabled = true;
              */
        }

        //-----------------Dash-----------------  
        if (Input.GetKey(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(Dash());
            //Animator_player.SetBool("Bool_Dash", true);
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            //rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            //  Player_Animator.SetBool("BoolRun", true);                                                               //play run animation
            sprite_renderer.flipX = false;                                                                          //flip the direction of the animation

            /*  Particle_VFX.Play();
               Particle_VFX.transform.eulerAngles = new Vector3(0, 90, 0);

               footstepsSound.enabled = true;
            */
        }

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);

            //  Player_Animator.SetBool("BoolJump", true);                                                             //play jump animation
        }

        /*
        //Rotate                                                                                                   //when i press the chosen keys, i can Rotate on sur different axes

        if (Input.GetKey(KeyCode.Keypad0))
        {
        isMooving = true;
            transform.Rotate(Vector3.left * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
        isMooving = true;
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.Keypad2))
        {
        isMooving = true;
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.Keypad3))
        {
        isMooving = true;     
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
        }
        */


        //Scale                                                                                                                       //this is a scale for growing or shrinking my character
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            transform.localScale += new Vector3(-scale * Time.deltaTime, -scale * Time.deltaTime, -scale * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            transform.localScale += new Vector3(scale * Time.deltaTime, scale * Time.deltaTime, scale * Time.deltaTime);
        }


        //-----------------Animation-----------------
        /*
        else                                                                                                                           //I make sure that when I release the key, the animation ends.
        {
            Player_Animator.SetBool("BoolRun", false);

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Particle_VFX.Pause();
                Particle_VFX.Clear();
            }

            Player_Animator.SetBool("BoolAttack", false);
            Player_Animator.SetBool("BoolDeath", false);
            Player_Animator.SetBool("BoolJump", false);

            footstepsSound.enabled = false;
        }
        */
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
}


