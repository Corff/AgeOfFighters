using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D rb;
    public CAttack enemyAttack;
    public CharMovement cmove;
    bool isAi;
    bool walking;
    bool running;
    int walk = 1;


    // Start is called before the first frame update
    void Start()
    {
        isAi = gameObject.transform.GetChild(7).gameObject.activeSelf;
        if (isAi)
        {
            Debug.Log("I am AI");
            enemyAttack = gameObject.GetComponent<CAttack>();
            player = GameObject.FindGameObjectWithTag("Player");
            rb = gameObject.GetComponent<Rigidbody2D>();
            cmove = gameObject.GetComponent<CharMovement>();
            chooseWalk();
            jump();
            crouch();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (isAi)
        {
            float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            //Debug.Log(distance);
            if (distance < 2)
            {
                attackPlayer();
            }

            if (distance > 5)
            {
                rangedPlayer();
            }

            //WalkToPlayer();

        }


    }

    private void FixedUpdate()
    {
        if (walking)
        {
            WalkToPlayer();
        }
        if (running)
        {
            RunFromPlayer();
        }
    }

    void WalkToPlayer()
    {

        float posDiff = gameObject.transform.localPosition.x - player.GetComponent<Transform>().localPosition.x;

        if (posDiff > 0)
        {
            cmove.MoveHorizontal(-walk, ref rb);
        }

        if (posDiff < 0)
        {
            cmove.MoveHorizontal(walk, ref rb);
        }

        //Debug.Log(posDiff);

    }
    void RunFromPlayer()
    {
        float posDiff = gameObject.transform.localPosition.x - player.GetComponent<Transform>().localPosition.x;

        if (posDiff < 0)
        {
            cmove.MoveHorizontal(-walk, ref rb);
        }

        if (posDiff > 0)
        {
            cmove.MoveHorizontal(walk, ref rb);
        }
    }
    void attackPlayer()
    {
        
        if (Random.Range(0, 2) == 1)
        {
            enemyAttack.LightAttackOn();
        }else
        {
            enemyAttack.HeavyAttackOn();
        }
        Debug.Log(Random.Range(0,2));

    }
    void rangedPlayer()
    {
        enemyAttack.RangedAttackOn();
    }

    void chooseWalk()
    {
        Debug.Log("Choosing");
        int rand = Random.Range(0, 10);

        walking = false;
        running = false;

        if (rand <= 3)
        {
            walking = true;
            Debug.Log(rand + "walkTo");

        }
        else
        {
            running = true;
            Debug.Log(rand + "runFrom");
        }
        Invoke("chooseWalk", 5);
    }
    void jump()
    {
        int rand = Random.Range(0, 2);
        if(rand == 1)
        {
            cmove.JumpOn();
        }
        Invoke("jump", 2);
    }
    void crouch()
    {
        int rand = Random.Range(0, 2);
        int rand2 = Random.Range(0, 5);
        if (rand == 1)
        {
            StartCoroutine(crouchTime(rand2));
        }
        Invoke("crouch", 2);
    }
    IEnumerator crouchTime(float a)
    {
        cmove.CrouchOn();
        walk = 0;
        yield return new WaitForSeconds(a);
        cmove.CrouchOff();
        walk = 1;
    }
}
