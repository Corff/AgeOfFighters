using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D rb;
    public CAttack enemyAttack;
    public CharMovement cmove;


    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "EnemyAI")
        {
            Debug.Log("I am AI");
            enemyAttack = gameObject.GetComponent<CAttack>();
            player = GameObject.FindGameObjectWithTag("Player");
            rb = gameObject.GetComponent<Rigidbody2D>();
            cmove = gameObject.GetComponent<CharMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.tag == "EnemyAI")
        {
            WalkToPlayer();
            float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
            //Debug.Log(distance);
            if (distance < 2)
            {
                attackPlayer();
            }
        }
    }

    void WalkToPlayer()
    {
        
        float posDiff = gameObject.transform.localPosition.x - player.GetComponent<Transform>().localPosition.x;

        if (posDiff > 0)
        {
            Vector2 movement = new Vector2(10, rb.velocity.y);
            rb.velocity += movement;
            gameObject.transform.localPosition += new Vector3(-0.01f, 0, 0);
            cmove.MoveHorizontal(-1);
            //Debug.Log("Left");
            gameObject.transform.localScale = new Vector3(-0.3f, 0.3f, 1);
        }

        if (posDiff < 0)
        {

            Vector2 movement = new Vector2(-10, rb.velocity.y);
            rb.velocity += movement;
            gameObject.transform.localPosition += new Vector3(0.01f, 0, 0);
            cmove.MoveHorizontal(1);
            //Debug.Log("Right");
            gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }

        //Debug.Log(posDiff);

    }
    void attackPlayer()
    {

        enemyAttack.LightAttackOn();

    }
}
