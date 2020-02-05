using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private string targetTag;
    public int damageArrayPosition;
    private int damage;
    private Health health;

    private void Awake()
    {
        //Set the tag to the opposite of what the gameobject
        //this script is attached to.
        if (gameObject.transform.parent.parent.parent.tag == "Player")
        {
            targetTag = "Enemy";
        }
        else if (gameObject.transform.parent.parent.parent.tag == "Enemy")
        {
            targetTag = "Player";
        }
        //Get the damage value of the attack
        damage = gameObject.GetComponentInParent<PlayerController>().damageArray[damageArrayPosition];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            health = collision.gameObject.GetComponentInParent<Health>();
            health.takeHealth(damage); //Deal damage to the target
        }
    }
}
