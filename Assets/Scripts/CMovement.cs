﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class CMovement : MonoBehaviour
{
    //*** = Not Implemented
    public bool isCrouched = false;
 
    //public static float distance;
    public bool dashBuffer = false;
    public bool isWalking = false;
    public bool isDashing = false;
    public bool isShielding = false;
    public bool facingRight;
    public bool isGrounded;
    public float dash = 0f;
    public float speed = 5f;
    public float dist = 1f;
    public ParticleSystem dust;
    private float jumpForce = -500f;
    private float groundedTimer = 0;
    private Rigidbody rigidBody;
    private RaycastHit2D hit;
    private Vector3 dir;
    private Vector3 movement;
    private Animator anim;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isGrounded = true;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var hitbox = GetComponent<BoxCollider>();

        if (Input.GetButtonDown("Crouch") && gameObject.tag == "Player") //Enter Crouch
        {
            isCrouched = true;
            transform.localScale -= new Vector3(0, 0.5f, 0);
            jumpForce = -400f;
            hitbox.size = new Vector3(1, 0.5f, 1);
            isShielding = true;
            if (isGrounded)
            {
                transform.localPosition = new Vector3(transform.position.x, -0.25f, 0);
            }
            //anim.SetBool("isCrouched", true); ***
        }

        if (Input.GetButtonDown("EnemyCrouch") && gameObject.tag == "Enemy") //Enter Crouch
        {
            isCrouched = true;
            transform.localScale -= new Vector3(0, 0.5f, 0);
            jumpForce = -400f;
            hitbox.size = new Vector3(1, 0.5f, 1);
            isShielding = true;
            if (isGrounded)
            {
                transform.localPosition = new Vector3(transform.position.x, -0.25f, 0);
            }
            //anim.SetBool("isCrouched", true); ***
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
            isCrouched = false;
            isShielding = false;
            transform.localScale += new Vector3(0, 0.5f, 0);
            jumpForce = -500f;
            hitbox.size = new Vector3(1, 1, 1);
            //anim.SetBool("isCrouched", false); ***
        }

        if (Input.GetButtonUp("EnemyCrouch") && isCrouched && gameObject.tag == "Enemy") //Exit crouch
        {
            isCrouched = false;
            isShielding = false;
            transform.localScale += new Vector3(0, 0.5f, 0);
            jumpForce = -500f;
            hitbox.size = new Vector3(1, 1, 1);
            //anim.SetBool("isCrouched", false); ***
        }

        dir = Vector3.down;
        Vector3 endpoint = transform.position + new Vector3(1, 0);
        Vector3 startpoint = transform.position + new Vector3(-1, 0);
        Debug.DrawRay(transform.position, dir * dist, Color.green);
        groundedTimer += Time.deltaTime;

        //Position --- Convert to else ifs
        if (!isGrounded && groundedTimer >= 0.2f)
        {
            if (Physics.Raycast(transform.position, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
                speed = 5f;
            }

            else
            {
                isGrounded = false;
            }

            //Endpoint
            if (Physics.Raycast(endpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
                speed = 5f;
            }

            else
            {
                isGrounded = false;
            }

            //Startpoint
            if (Physics.Raycast(startpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y);
                isGrounded = true;
                speed = 5f;
            }

            else
            {
                isGrounded = false;
            }
        }

        if (Input.GetButtonDown("Jump") && isGrounded && gameObject.tag == "Player") //Jumping
        {
            rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);
            speed = 3.5f;
            groundedTimer = 0;
            isGrounded = false;
            anim.SetTrigger("jump");
            CreateDust();
        }

        if (Input.GetButtonDown("EnemyJump") && isGrounded && gameObject.tag == "Enemy")
        {
            Debug.Log("Debug Alpha");
            rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);
            speed = 3.5f;
            groundedTimer = 0;
            isGrounded = false;
            anim.SetTrigger("jump");
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
            movement = new Vector3(moveHorizontal * (speed + 5), rigidBody.velocity.y, 0);
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
            speed = 5f;
        }

        if (moveHorizontal > 0)
        {
           anim.SetBool("isRunning", true);
        }

        else if (moveHorizontal < 0)
        {
           anim.SetBool("isRunning", true);
        }

        if (moveHorizontal > 0 && facingRight == true)
        {
            facingRight = false;
            Flip();
        }

        else if (moveHorizontal < 0 && facingRight == false)
        {
            facingRight = true;
            Flip();
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
            if (Input.GetButtonDown("Dash") && gameObject.tag == "Player")
            {
                dashBuffer = false;
                speed = 10f;
                movement = new Vector3(Input.GetAxis("Horizontal") * (speed + 5), rigidBody.velocity.y, 0);
                dash = 0;
                isDashing = true;
                //anim.SetBool("Running", false);
                //Dash Animation
            }
            else if (Input.GetButtonDown("EnemyDash") && gameObject.tag == "Enemy")
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