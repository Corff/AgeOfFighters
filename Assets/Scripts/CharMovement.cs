using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public bool isCrouched = false;
    public float speed;
    //public static float distance;

    //Movement variables
    public bool dashBuffer = false;
    public float dash = 0f;
    public bool isWalking = false;
    public bool isDashing = false;
    public bool isShielding = false;
    private List<int?> moveQueue = new List<int?>(15);

    //Condition variables
    public bool facingRight;
    public bool isGrounded;

    public ParticleSystem dust;

    //Jump Calculations
    public float dist = 1f;
    private float jumpForce = -500f;
    [SerializeField] private float groundedTimer = 0.5f;

    //Components
    private Rigidbody2D rigidBody;
    private RaycastHit2D hit;
    private Vector3 dir;
    private Vector3 movement;
    private Animator anim;
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

    private void Start()
    {
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

    private void Update()
    {
        if (Input.GetButtonDown("Crouch") && gameObject.tag == "Player")
        {
            CrouchOn();
        }
        else if (Input.GetButtonDown("EnemyCrouch") && gameObject.tag == "Enemy")
        {
            CrouchOn();
        }

        if (Input.GetButtonUp("Crouch") && gameObject.tag == "Player")
        {
            CrouchOff();
        }

        if (Input.GetButtonUp("EnemyCrouch") && gameObject.tag == "Enemy")
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

        if (!isGrounded && groundedTimer >= 0.2f)
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
        }

        if (Input.GetButtonDown("Jump") && isGrounded && gameObject.tag == "Player")
        {
            Debug.Log("Jump");
            JumpOn();
        }
        else if (Input.GetButtonDown("EnemyJump") && isGrounded && gameObject.tag == "Enemy")
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

    private void FixedUpdate()
    {
        float moveHorizontal = 0f;

        if (((!isCrouched && isGrounded) || !isGrounded) && !isShielding)
        {
            if (gameObject.tag == "Player")
            {
                moveHorizontal = Input.GetAxis("Horizontal");
                MoveHorizontal(moveHorizontal, true);
            }
            else
            {
                moveHorizontal = Input.GetAxis("EnemyHorizontal");
                MoveHorizontal(moveHorizontal, true);
            }
        }
    }
    private void Awake()
    {
        MoveHorizontal(-1f, 
    }
    /// <summary>
    /// Call within FixedUpdate. Used to move the player.
    /// </summary>
    /// <param name="moveHorizontal">Value between -1 and 1 (inclusive).</param>
    /// <param name="calculateDash">When true, the calculations on the dash queue will be made.
    /// <para>For AI, this should probably be false.
    /// Test</para>
    /// </param>
    public void MoveHorizontal(float moveHorizontal, bool calculateDash = false)
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
    /// Make the character dash.
    /// </summary>
    public void Dash()
    {
        throw new NotImplementedException();
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

