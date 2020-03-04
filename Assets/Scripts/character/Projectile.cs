using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damagePosition;
    private float damage;

    private string targetTag;

    private CMovement pc;
    private Health health;
    public GameObject destroyEffect;
    private void Start()
    {
        pc = GameObject.FindGameObjectWithTag(gameObject.tag).GetComponent<CMovement>();
        if (!pc.facingRight)
        {
            speed *= -1;
        }
        Invoke("DestroyProjectile", lifeTime);
        //Create a calaculate target class to be used here and in AttackCollision.
        if (gameObject.tag == "Player")
        {
            targetTag = "Enemy";
        }
        else if (gameObject.tag == "Enemy")
        {
            targetTag = "Player";
        }
        //Get the damage value of the attack
        damage = GameObject.FindGameObjectWithTag(gameObject.tag).GetComponent<CAttack>().damageArray[damagePosition];
    }
    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            health = collision.gameObject.GetComponentInParent<Health>();
            health.TakeHealth(damage, gameObject); //Deal damage to the target
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

    }

}
