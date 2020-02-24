using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool dead;
    public int health = 100; //Make Private once implemented
    public string deathTag;
    private Slider healthSlider;
    private Blocking blocking;
    private SpecialAttackControl accessSP;
    private GameObject workingObj;
   

    void Start()
    {
        accessSP = gameObject.GetComponent<SpecialAttackControl>();
        if(gameObject.tag == "Player")
        {
            healthSlider = GameObject.FindWithTag("PlayerHealth").GetComponent<Slider>();
        }
        else if(gameObject.tag == "Enemy")
        {
            healthSlider = GameObject.FindWithTag("EnemyHealth").GetComponent<Slider>();
        }
        healthSlider.value = health;
        blocking = GetComponent<Blocking>();
    }

    void Update()
    {
        healthSlider.value = health;
        if (health <= 0)
        {
            deathTag = gameObject.tag;
            dead = true;
        }
    }

    /// <summary>
    /// Deals damage to the character.
    /// </summary>
    /// <param name="amount">Value to be taken.</param>
    public void TakeHealth(int amount) //IDE1006 Name Violation
    {
        Debug.Log(blocking.blocked);
        //Checks for blocking before taking health, takes stamina instead of blocking.
        if (blocking.perfectBlock) //If the block is perfect take only half the amount off.
        {
            blocking.timer.time = blocking.timer.time - (amount / 2);
        }
        else if (blocking.blocked) //Consider making this a private method
        {
            blocking.timer.time -= amount;
        }
        else
        {
            health -= amount;
            healthSlider.value = health;
            if(gameObject.tag == "Player")
            {
                workingObj = GameObject.FindGameObjectWithTag("Enemy");
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
            }
            if (gameObject.tag == "Enemy")
            {
                workingObj = GameObject.FindGameObjectWithTag("Player");
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
            }
        }
    }

    /// <summary>
    /// Deals damage to the character.
    /// </summary>
    /// <param name="amount">Value to be taken.</param>
    /// <param name="ranged">True indicates the attack is ranged and can be reflected.</param>
    public void TakeHealth(int amount, GameObject go)
    {
        if (blocking.perfectBlock) //If the block is perfect take only half the amount off.
        {
            blocking.timer.time = blocking.timer.time - (amount / 2);
            blocking.ReturnProjectile(go);
        }
        else if (blocking.blocked)
        {
            blocking.timer.time -= amount;
        }
        else
        {
            health -= amount;
            healthSlider.value = health;
            if (gameObject.tag == "Player")
            {
                workingObj = GameObject.FindGameObjectWithTag("Enemy");
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
            }
            if (gameObject.tag == "Enemy")
            {
                workingObj = GameObject.FindGameObjectWithTag("Player");
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
            }
        }
    }

    /// <summary>
    /// Heals the character.
    /// </summary>
    /// <param name="amount">Value to be added.</param>
    public void AddHealth(int amount) //IDE1006 Name Violation
    {
        health += amount;
        healthSlider.value = health;
    }


}
