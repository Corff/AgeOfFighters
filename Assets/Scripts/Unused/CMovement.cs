using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class CMovement : MonoBehaviour
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

    //These variables could be placed in another script
    private Transform EnemyPos;
    private Transform PlayerPos;

    private SFXController soundAccess; 

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isGrounded = true;
        anim = GetComponent<Animator>();
        soundAccess = GameObject.FindGameObjectWithTag("GameController").GetComponent<SFXController>();

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
        var hitbox = GetComponent<BoxCollider2D>();

        if (Input.GetButtonDown("Crouch") && gameObject.tag == "Player") //Enter Crouch
        {
            soundAccess.soundCall(gameObject, "Crouch"); 
            isCrouched = true;
            //@@ Testing out another method of crouching
            
            //transform.localScale -= new Vector3(0, 0.5f, 0);
            //jumpForce = -400f;
            //hitbox.size = new Vector3(1, 2.62f, 1);
            //hitbox.offset = new Vector2(0, -0.27f);
            
            isShielding = true;
            /*
            if (isGrounded)
            {
                transform.localPosition = new Vector3(transform.position.x, -0.25f, 0);
            }
            */
            //Crouching hitbox (box collider) implemented into the animation itself
            anim.SetBool("isCrouched", true);
        }

        if (Input.GetButtonDown("EnemyCrouch") && gameObject.tag == "Enemy") //Enter Crouch
        {
            isCrouched = true;

            //transform.localScale -= new Vector3(0, 0.5f, 0);
            //jumpForce = -400f;
            //hitbox.size = new Vector3(1, 2.62f, 1);
            //hitbox.offset = new Vector2(0, -0.27f);

            isShielding = true;
            //if (isGrounded)
            //{
            //    transform.localPosition = new Vector3(transform.position.x, -0.25f, 0);
            //}
            anim.SetBool("isCrouched", true);
        }

        //else if (!isGrounded && !isCrouched)
        //{
        //	if (Input.GetKeyDown(KeyCode.DownArrow))
        //	{
        //		isCrouched = true;
        //		transform.localScale -= new Vector3(0, 0.5f, 0);
        //		jumpForce = -400f;
        //		hitbox.size = new Vector3(1, 0.5f, 1);
        //	}
        //}

        if (Input.GetButtonUp("Crouch") && isCrouched && gameObject.tag == "Player") //Exit crouch
        {
            soundAccess.soundCall(gameObject, "Crouch");
            isCrouched = false;
            isShielding = false;
            //@@ Testing out another method of crouching
            /*
            transform.localScale += new Vector3(0, 0.5f, 0);
            jumpForce = -500f;
            hitbox.size = new Vector3(0.72f, 2.9f, 1);
            hitbox.offset = new Vector2(0, 0);
            */
            anim.SetBool("isCrouched", false);
        }

        if (Input.GetButtonUp("EnemyCrouch") && isCrouched && gameObject.tag == "Enemy") //Exit crouch
        {
            isCrouched = false;
            isShielding = false;
            //transform.localScale += new Vector3(0, 0.5f, 0);
            //jumpForce = -500f;
            //hitbox.size = new Vector3(0.72f, 2.9f, 1);
            //hitbox.offset = new Vector2(0, 0);
            anim.SetBool("isCrouched", false);
        }

        dir = Vector2.down;
        Vector2 endpoint = transform.position + new Vector3(1, 0);
        Vector2 startpoint = transform.position + new Vector3(-1, 0);
        Debug.DrawRay(transform.position, dir * dist, Color.green);
        groundedTimer += Time.deltaTime;

        //Position --- Convert to else ifs
        if (!isGrounded && groundedTimer >= 0.2f)
        {
            if (Physics2D.Raycast(transform.position, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
                //speed = 5f;
            }

            else
            {
                isGrounded = false;
            }

            //Endpoint
            if (Physics2D.Raycast(endpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
                //speed = 5f;
            }

            else
            {
                isGrounded = false;
            }

            //Startpoint
            if (Physics2D.Raycast(startpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
                //speed = 5f;
            }

            else
            {
                isGrounded = false;
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded && gameObject.tag == "Player") //Jumping
        {
            rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);
            groundedTimer = 0;
            isGrounded = false;
            soundAccess.soundCall(gameObject, "Jump");
            anim.SetTrigger("isJumping");
            CreateDust();
        }

        if (Input.GetButtonDown("EnemyJump") && isGrounded && gameObject.tag == "Enemy")
        {
            Debug.Log("Debug Alpha");
            rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);
            groundedTimer = 0;
            isGrounded = false;
            soundAccess.soundCall(gameObject, "Jump");
            anim.SetTrigger("isJumping");
            CreateDust();
        }

        //This needs implementing with the blocking script.
        if (Input.GetKeyDown(KeyCode.F)) //Blocking Start
        {
            isShielding = true;
        }

        else if (Input.GetKeyUp(KeyCode.F)) //Blocking Stop
        {
            isShielding = false;
        }

        //Player and Enemy will always be facing each other.
        //if X scale is positive, player will be facing right.
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

    

    void FixedUpdate()
    {
        float moveHorizontal = 0f;

        if (((!isCrouched && isGrounded) || !isGrounded) && !isShielding)
        {
            if (gameObject.tag == "Player")
            {
                moveHorizontal = Input.GetAxis("Horizontal");
            }
            else
            {
                moveHorizontal = Input.GetAxis("EnemyHorizontal");
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
        }

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
           soundAccess.soundCall(gameObject,"Walk");
           anim.SetBool("isRunning", true);

        }

        else if (moveHorizontal < 0)
        {
            soundAccess.soundCall(gameObject, "Walk");
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

        if (dashBuffer == true && !isWalking && dash > 0 && dash <= 1)
        {
            if (Input.GetButtonDown("Cancel") && gameObject.tag == "Player")
            {
                dashBuffer = false;
                speed = 10f;
                movement = new Vector3(Input.GetAxis("Horizontal") * (speed + 5), rigidBody.velocity.y, 0);
                dash = 0;
                isDashing = true;
                //anim.SetBool("Running", false);
                //Dash Animation
            }
            else if (Input.GetButtonDown("Cancel") && gameObject.tag == "Enemy")
            {
                dashBuffer = false;
                speed = 10f;
                movement = new Vector3(Input.GetAxis("Horizontal") * (speed + 5), rigidBody.velocity.y, 0);
                dash = 0;
                isDashing = true;
                //anim.SetBool("Running", false);
                //Dash Animation
            }
        }

        if (dash > 1)
        {
            isWalking = true;
            dash = 0;
            dashBuffer = false;
        }
    }
    //These could be static in a seperate script
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
}