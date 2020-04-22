using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private string targetTag;
    public int damageArrayPosition;
    private float damage;
    private Health health;

    private void Start()
    {
        //Set the tag to the opposite of what the gameobject
        //this script is attached to.
        if (gameObject.transform.parent.parent.parent.parent.tag == "Player")
        {
            Debug.LogWarning("If damage breaks, this probably needs re-writing");
            targetTag = "Enemy";
        }
        else if (gameObject.transform.parent.parent.parent.parent.tag == "Enemy")
        {
            targetTag = "Player";
        }
        //Get the damage value of the attack
        damage = gameObject.GetComponentInParent<CAttack>().damageArray[damageArrayPosition];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == targetTag)
        {
            Debug.Log("Health");
            health = collision.gameObject.GetComponentInParent<Health>();
            health.TakeHealth(damage); //Deal damage to the target
        }
    }
}
