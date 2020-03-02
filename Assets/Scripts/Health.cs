using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool dead;
    public float health = 100; //Make Private once implemented
    public string deathTag;
    private Slider healthSlider;
    private Blocking blocking;
    private SpecialAttackControl accessSP;
    private GameObject workingObj;
    public Gradient gradient; //Reponsible for changing health bar colours depending on how much health you have
    private Image fill; //the Bar of the Health Bar.
   

    void Start()
    {
        accessSP = gameObject.GetComponent<SpecialAttackControl>();
        if(gameObject.tag == "Player")
        {
            healthSlider = GameObject.FindWithTag("PlayerHealth").GetComponent<Slider>();
            fill = GameObject.FindWithTag("PlayerHealthBar").GetComponent<Image>();
        }
        else if(gameObject.tag == "Enemy")
        {
            healthSlider = GameObject.FindWithTag("EnemyHealth").GetComponent<Slider>();
            fill = GameObject.FindWithTag("EnemyHealthBar").GetComponent<Image>();
        }
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue); //Changes the health bar colour based on the character's HP
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
    public void TakeHealth(float amount) //IDE1006 Name Violation
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
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);  //Changes the health bar colour based on the character's HP
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
    /// Deals damage to the character.
    /// </summary>
    /// <param name="amount">Value to be taken.</param>
    /// <param name="go">The game object of the ranged object.</param>
    public void TakeHealth(float amount, GameObject go)
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
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);  //Changes the health bar colour based on the character's HP
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
    public void AddHealth(float amount)
    {
        health += amount;
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);  //Changes the health bar colour based on the character's HP
    }


}
