using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    public bool isGrounded;
    public bool onPlayer;

    private Rigidbody2D rb;
    private Animator anim;

    public bool facingRight = true; //Made public for ranged attack direction

    public Transform groundCheck;
    public GameObject punchCheck;
    public GameObject heavyPunchCheck;
    public float checkRadius;

    //Check if these are being used still
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public Transform punchPos;
    public float punchRange;

    public ParticleSystem dust;

    public int[] damageArray = { 20, 50, 30, 400};//Light Attack, Heavy Attack, Ranged Attached and Special Attack Damage
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded == true || onPlayer == true) && gameObject.tag == "Player")
        {
            rb.velocity = Vector2.up * jumpForce;
            CreateDust();
            anim.SetTrigger("jump");
        }

        if (isGrounded || onPlayer && gameObject.tag == "Player")
        {
            if(rb.velocity != Vector2.zero)
            {
                CreateDust();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && gameObject.tag == "Player") // Primary mouse button (left click)
        {
            punchCheck.SetActive(true);
            anim.SetTrigger("Punch");
        }

        if (Input .GetKeyUp(KeyCode.E) && gameObject.tag == "Player")
        {     
            punchCheck.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R) && gameObject.tag == "Player") // Primary mouse button (left click)
        {
            heavyPunchCheck.SetActive(true);
            anim.SetTrigger("Heavy Punch");
        }

        if (Input.GetKeyUp(KeyCode.R) && gameObject.tag == "Player")
        {
            heavyPunchCheck.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && (isGrounded == true || onPlayer == true) && gameObject.tag == "Enemy")
        {
            rb.velocity = Vector2.up * jumpForce;
            CreateDust();
            anim.SetTrigger("jump");
        }

        if (isGrounded || onPlayer && gameObject.tag == "Enemy")
        {
            if (rb.velocity != Vector2.zero)
            {
                CreateDust();
            }
        }

        if (Input.GetKeyDown(KeyCode.Insert) && gameObject.tag == "Enemy") // Primary mouse button (left click)
        {
            punchCheck.SetActive(true);
            anim.SetTrigger("Punch");
        }

        if (Input.GetKeyUp(KeyCode.Insert) && gameObject.tag == "Enemy")
        {
            punchCheck.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.End) && gameObject.tag == "Enemy") // Primary mouse button (left click)
        {
            heavyPunchCheck.SetActive(true);
            anim.SetTrigger("Heavy Punch");
        }

        if (Input.GetKeyUp(KeyCode.End) && gameObject.tag == "Enemy")
        {
            heavyPunchCheck.SetActive(false);
        }

    }

    void FixedUpdate()
    {
        if (gameObject.tag == "Player") {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            onPlayer = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsPlayer);

            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (moveInput == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }
        if (gameObject.tag == "Enemy")
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
            onPlayer = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsPlayer);

            moveInput = Input.GetAxis("Enemy Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (moveInput == 0)
            {
                anim.SetBool("isRunning", false);
            }
            else
            {
                anim.SetBool("isRunning", true);
            }

            if (facingRight == false && moveInput > 0)
            {
                Flip();
            }
            else if (facingRight == true && moveInput < 0)
            {
                Flip();
            }
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
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
