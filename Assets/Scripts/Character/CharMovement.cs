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

    //Dash rewrite -- VAR NEED ORGANISING
    bool dashTimerRunning = false;
    float dashTimer = 0f;
    float dashWindow = 1f;
    bool dashFailed = false;
    public float dashDuration = 0.25f;
    public float dashSpeed = 30f;
    private bool dashDurationTimerRunning;
    private float dashDurationTimer;
    public float dashCoolDown = 1f;
    private bool dashCoolDownTimerRunning;
    private float dashCoolDownTimer;

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
            DashManager(-1);
        }
        else if (Input.GetButtonDown("DashRight") && gameObject.tag == "Player")
        {
            DashManager(1);
        }
        else if (Input.GetButtonDown("EnemyDashLeft") && gameObject.tag == "Enemy")
        {
            DashManager(-1);
        }
        else if (Input.GetButtonDown("EnemyDashRight") && gameObject.tag == "Enemy")
        {
            DashManager(1);
        }

        //Probably needs some of this replacing with the timer object
        if (dashTimerRunning)
        {
            dashTimer += Time.deltaTime; //This is the time frame to get the second input in to dash
            if (dashTimer >= dashWindow) //When out of time clear out the queue
            {
                DashTimerReset();
            }
        }
        if (dashDurationTimerRunning) //Countdown how long the dash speed buff lasts
        {
            dashDurationTimer -= Time.deltaTime;
            if (dashDurationTimer <= 0)
            {
                dashDurationTimer = dashDuration;
                speed = 5f;
                dashDurationTimerRunning = false;
                DashTimerReset();
                dashCoolDownTimerRunning = true;
            }
        }
        if (dashCoolDownTimerRunning)
        {
            dashCoolDownTimer -= Time.deltaTime;
            if (dashCoolDownTimer <= 0)
            {
                dashCoolDownTimer = dashCoolDown;
                dashCoolDownTimerRunning = false;
                DashTimerReset();
            }
        }
    }

    /// <summary>
    /// Used to check for a dash attempt and control the adding of new inputs to the dash queue.
    /// </summary>
    /// <param name="dir">Value of either 1 or -1, the direction of the dash.</param>
    private void DashManager(int dir)
    {
        if(dashQueue.Count >= 2)
        {
            dashQueue.RemoveAt(0);
        }
        if (dashCoolDownTimerRunning) //If dash is on cooldown reset it and clear the queue.
        {
            DashTimerReset();
            return;
        }
        if (!dashTimerRunning) //If the dash timer is not running this is the first time we have had an input recently.
        {
            dashTimerRunning = true;
            dashQueue.Add(dir); //Add the latest input
        }
        else //Only check if there has been atleast 2 inputs recently.
        {
            //Check if we are going to dash.
            //If we dont dash clean out the queue.
            //If the items match then we dash.
            dashQueue.Add(dir);
            dashFailed = true;
            for (int i = 1; i < dashQueue.Count; i++)
            {
                if (dashQueue[i] == dashQueue[i-1])
                {
                    dashFailed = false;
                    Dash();
                    return;
                }
            }
            if (dashFailed) //If we didnt dash reset the queue
            {
                DashTimerReset();
            }
        }
    }

    /// <summary>
    /// Make the character dash.
    /// </summary>
    public void Dash() //Turn on particle effect in here
    {
        Debug.Log("Dash");
        if (!dashDurationTimerRunning) //Prevent dash firing twice
        {
            speed = dashSpeed;
            dashDurationTimerRunning = true;
        }

    }

    /// <summary>
    /// Used to reset the dash timer 
    /// </summary>
    private void DashTimerReset()
    {
        dashTimerRunning = false;
        //This may need changing, maybe just use two queues, one for just dashing and one for attacks
        dashQueue.Clear();
        dashTimer = 0;
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

        rigidBody.velocity = movement;

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
        else if (moveHorizontal< 0)
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

