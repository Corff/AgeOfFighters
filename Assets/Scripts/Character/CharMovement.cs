using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour
{
    public bool isCrouched = false;
    public float speed = 5f;
    public float dashSpeed = 10f;
    public float backdashDuration;
    //public static float distance;

    //Movement variables
    public bool dashBuffer = false;
    public float dash = 0f;
    public bool isWalking = false;
    public bool isDashing = false;
    public bool isShielding = false;
    private List<int?> dashQueue = new List<int?>(15);

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

    //Fixed time step
    private float timeBuffer = 0f;
    private int offset = 1;

    //Input Lockout
    public bool inputActive = false;

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
        timeBuffer += Time.deltaTime;

        // if (timeBuffer >= offset)
        //{
        if (inputActive)
        {
            if (Input.GetButtonDown("Crouch") && gameObject.tag == "Player")
            {
                CrouchOn();
                Debug.Log("RESET");
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

            if (Input.GetButtonDown("DashLeft") && gameObject.tag == "Player")
            {
                DashManager(1);
            }
            else if (Input.GetButtonDown("DashRight") && gameObject.tag == "Player")
            {
                DashManager(1);
            }
            else if (Input.GetButtonDown("EnemyDashLeft") && gameObject.tag == "Enemy")
            {
                DashManager(1);
            }
            else if (Input.GetButtonDown("EnemyDashRight") && gameObject.tag == "Enemy")
            {
                DashManager(1);
            }
        }
       // }

       // timeBuffer -= offset;
    }

    private void DashManager(int v)
    {
        if (dashQueue.Count == 15)
        {
            dashQueue.RemoveAt(0);
        }
        dashQueue.Add(v);
    }

    private void FixedUpdate()
    {
        float moveHorizontal = 0f;

        if (((!isCrouched && isGrounded) || !isGrounded) && !isShielding && inputActive) //TODO: Replace isShielding
        {
            if (gameObject.tag == "Player")
            {
                moveHorizontal = Input.GetAxis("Horizontal");
            }
            else
            {
                moveHorizontal = Input.GetAxis("EnemyHorizontal");
            }
            if ((moveHorizontal > 0 || moveHorizontal < 0) && !isWalking && !isDashing)
            {
                dashBuffer = true;
            }
            MoveHorizontal(moveHorizontal);
        }
        if (dashBuffer && !isWalking && dash > 0 && dash <= 1 && dashQueue.Count >= 2)
        {
            for (int i = 1; i < dashQueue.Count; i++)
            {
                if (dashQueue[i-1] == 1 && dashQueue[i] == 1)
                {
                    speed = 10f;
                    isDashing = true;
                }
            }
        }

        if (dashBuffer)
        {
            dash += Time.deltaTime;
        }
        else if (!dashBuffer)
        {
            dash = 0;
        }
        if (dash > 0.35f)
        {
            isWalking = true;
            dash = 0;
            dashBuffer = false;
        }

        if (isWalking || moveHorizontal == 0 && dashQueue.Count >= 2)
        {
            try
            {
                if (dashQueue[dashQueue.Count - 1] == 1)
                {
                    dashQueue[dashQueue.Count - 1] = 0;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Debug.LogError("DASH QUEUE ERROR");
                dashQueue.Clear();
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
        movement = new Vector3(moveHorizontal * (speed + 5), rigidBody.velocity.y, 0);
        //anim.SetBool("Running", true);

        rigidBody.velocity = movement;

        if (moveHorizontal == 0)
        {
            anim.SetBool("isRunning", false);
            dashBuffer = false;
            isWalking = false;
            speed = 5f; //dont hardcode
            isDashing = false;
        }

        if (moveHorizontal > 0)
        {
           anim.SetBool("isRunning", true);
        }
        else if (moveHorizontal< 0)
        {
           anim.SetBool("isRunning", true);
        }
    }

    /// <summary>
    /// Call within FixedUpdate. Used to move the player.
    /// </summary>
    /// <param name="moveHorizontal">Value between -1 and 1 (inclusive).</param>
    /// <param name="rb">Rigid body of the AI</param>
    public void MoveHorizontal(float moveHorizontal,  ref Rigidbody2D rb)
    {
        if (moveHorizontal > 1 || moveHorizontal < -1)
        {
            throw new ArgumentException("The value must be between 1 and -1 (inclusive)");
        }
        movement = new Vector3(moveHorizontal * (speed + 5), rigidBody.velocity.y, 0);
        //anim.SetBool("Running", true);

        rb.velocity = movement;

        Debug.Log(rigidBody.velocity);

        if (moveHorizontal == 0)
        {
            anim.SetBool("isRunning", false);
            dashBuffer = false;
            isWalking = false;
            //speed = 5f;
        }

        if (moveHorizontal > 0)
        {
            anim.SetBool("isRunning", true);
        }
        else if (moveHorizontal < 0)
        {
            anim.SetBool("isRunning", true);
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
    public void CrouchOff()
    {
        isCrouched = false;
        isShielding = false;
        anim.SetBool("isCrouched", false);
    }

    /// <summary>
    /// Make the character crouch.
    /// </summary>
    public void CrouchOn()
    {
        isCrouched = true;
        isShielding = true;
        anim.SetBool("isCrouched", true);
    }
}


