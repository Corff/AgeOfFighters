using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am AI");
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        WalkToPlayer();

        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        Debug.Log(distance);


    }

    void WalkToPlayer()
    {
        
        float posDiff = gameObject.transform.localPosition.x - player.GetComponent<Transform>().localPosition.x;

        if (posDiff > 0)
        {
            Vector2 movement = new Vector2(10, rb.velocity.y);
            rb.velocity += movement;
            Debug.Log("Left");
        }

        if (posDiff < 0)
        {

            Vector2 movement = new Vector2(-10, rb.velocity.y);
            rb.velocity += movement;
            Debug.Log("Right");
        }

        //Debug.Log(posDiff);

    }
    void attackPlayer()
    {

    }
}
