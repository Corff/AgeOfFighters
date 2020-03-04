using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    //*** = Not Implemented
    public bool isCrouched = false;
    public float speed;
    //public static float distance;

    //Movement variables
    public bool dashBuffer = false;
    public float dash = 0f;
    public bool isWalking = false;
    public bool isDashing = false;
    public bool isShielding = false;

    //Condition variables
    public bool facingRight;
    public bool isGrounded;

    public ParticleSystem dust;

    //??
    public float dist = 1f;
    private float jumpForce = -500f;
    private float groundedTimer = 0;

    //Components
    private Rigidbody2D rigidBody;
    private RaycastHit2D hit;
    private Vector3 dir;
    private Vector3 movement;
    private Animator anim;
    private BoxCollider2D boxCollider;

    //These variables could be placed in another script
    private Transform EnemyPos;
    private Transform PlayerPos;

    void Flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void CreateDust()
    {
        if (isGrounded)
        {
            dust.Play();
        }
    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        anim = GetComponent<Animator>();

        EnemyPos = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();

        if (gameObject.tag == "Enemy")
        {
            facingRight = false;
            Flip();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Crouch") && gameObject.tag == "Player")
        {
            CrouchOn();
        }
        else if (Input.GetButtonDown("Crouch") && gameObject.tag == "Enemy")
        {
            CrouchOn();
        }

        if (Input.GetButtonUp("Crouch") && gameObject.tag == "Player")
        {
            CrouchOff();
        }

        if (Input.GetButtonUp("Crouch") && gameObject.tag == "Enemy")
        {
            CrouchOff();
        }

        dir = Vector2.down;
        Vector2 endpoint = transform.position + new Vector3(0.24f, 0);
        Vector2 startpoint = transform.position + new Vector3(-0.24f, 0);
        Debug.DrawRay(transform.position, dir * dist, Color.green);
        Debug.DrawRay(startpoint, dir * dist, Color.green);
        Debug.DrawRay(endpoint, dir * dist, Color.green);
        groundedTimer += Time.deltaTime;

        if(!isGrounded && groundedTimer >= 0.2f)
        {
            if (Physics2D.Raycast(transform.position, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
            }

            else
            {
                isGrounded = false;
            }

            if (Physics2D.Raycast(endpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
            }

            else
            {
                isGrounded = false;
            }
            Debug.LogWarning("Update");
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("J");
            }
            if (Input.GetButtonDown("Jump") && isGrounded && gameObject.tag == "Player")
            {
                Debug.Log("Jump");
                JumpOn();
            }

            else if (Input.GetButtonDown("Jump") && isGrounded && gameObject.tag == "Enemy")
            {
                JumpOn();
            }

            if (EnemyPos.position.x < gameObject.transform.position.x && gameObject.tag == "Player")
            {
                facingRight = false;

                if (gameObject.transform.localScale.x > 0)
                {
                    Flip();
                }
            }

            else
            {
                facingRight = true;

                if (gameObject.transform.localScale.x < 0)
                {
                    Flip();
                }
            }

            if (PlayerPos.position.x < gameObject.transform.position.x && gameObject.tag == "Enemy")
            {
                facingRight = false;

                if (gameObject.transform.localScale.x > 0)
                {
                    Flip();
                }
            }

            else if (PlayerPos.position.x > gameObject.transform.position.x && gameObject.tag == "Enemy")
            {
                facingRight = true;

                if (gameObject.transform.localScale.x < 0)
                {
                    Flip();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = 0f;

        if (((!isCrouched && isGrounded) || !isGrounded) && !isShielding)
        {
            if (gameObject.tag == "Player")
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                MoveHorizontal(moveHorizontal);
            }

            else
            {
                moveHorizontal = Input.GetAxis("EnemyHorizontal");
                MoveHorizontal(moveHorizontal);
            }
        }
    }

    /// <summary>
    /// Call within FixedUpdate. Used to move the player.
    /// </summary>
    /// <param name="moveHorizontal">Value between -1 and 1 (inclusive).</param>
    public void MoveHorizontal(float moveHorizontal)
    {
        if (moveHorizontal > 1 || moveHorizontal < -1)
        {
            throw new ArgumentException("The value must be between 1 and -1 (inclusive)");
        }
        movement = new Vector3(moveHorizontal * (speed), rigidBody.velocity.y, 0);
        //anim.SetBool("Running", true);
        //Replace with moveHorizontal != 0 && !isWalking && !isDashing
        if (moveHorizontal > 0 && !isWalking && !isDashing)
        {
            dashBuffer = true;
        }

        else if (moveHorizontal < 0 && !isWalking && !isDashing)
        {
            dashBuffer = true;
        }

        rigidBody.velocity = movement;

        if (moveHorizontal == 0)
        {
            anim.SetBool("isRunning", false);
            dashBuffer = false;
            isWalking = false;
            isDashing = false;
            //speed = 5f;
        }

        if (moveHorizontal > 0)
        {
           anim.SetBool("isRunning", true);
        }

        else if (moveHorizontal< 0)
        {
           anim.SetBool("isRunning", true);
        }

        if (dashBuffer == true)
        {
            dash += Time.deltaTime;
        }

        else if (dashBuffer == false)
        {
            dash = 0;
        }
    }

    /// <summary>
    /// Makes the character jump.
    /// </summary>
    public void JumpOn()
    {
        rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);
        groundedTimer = 0;
        isGrounded = false;
        anim.SetTrigger("isJumping");
        CreateDust();
        Debug.Log("Attempt");
    }

    /// <summary>
    /// Make the character stand back up.
    /// </summary>
    private void CrouchOff()
    {
        isCrouched = false;
        isShielding = false;
        anim.SetBool("isCrouched", false);
    }

    /// <summary>
    /// Make the character crouch.
    /// </summary>
    void CrouchOn()
    {
        isCrouched = true;
        isShielding = true;
        anim.SetBool("isCrouched", true);
    }
}

