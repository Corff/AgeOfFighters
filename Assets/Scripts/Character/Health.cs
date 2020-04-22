using System;
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
    private CharMovement charMovement;
    public float stunTime = 2f;

    public float damageBlocked = 0f;
    public float damageDealt = 0f;
    public int lightAttackUsed = 0;
    public int rangedAttackUsed = 0;
    public int heavyAttackUsed = 0;
    public int specialAttackUsed = 0;
    public int totalHit = 0;
    private float target;
    private float current;
    public float slideSpeed = 1f;
    private ComboCounter comboCounter;

    void Start()
    {
        current = health;
        target = health;
        charMovement = GetComponent<CharMovement>();
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
        comboCounter = GameObject.FindWithTag("GameController").GetComponent<ComboCounter>();
    }

    void Update()
    {
        UpdateSlider();
        if (health <= 0)
        {
            deathTag = gameObject.tag;
            dead = true;
        }
    }

    /// <summary>
    /// Disables the input of the character.
    /// </summary>
    private IEnumerator DisableInput()
    {
        charMovement.inputActive = false;
        yield return new WaitForSeconds(stunTime);
        charMovement.inputActive = true;
    }

    /// <summary>
    /// Deals damage to the character.
    /// </summary>
    /// <param name="amount">Value to be taken.</param>
    public void TakeHealth(float amount)
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
            damageBlocked += amount;
        }
        else
        {
            UpdateSlider(amount);
            StartCoroutine(DisableInput()); //Delta time might be better.
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);  //Changes the health bar colour based on the character's HP
            if (gameObject.tag == "Player")
            {
                workingObj = GameObject.FindGameObjectWithTag("Enemy");
                workingObj.GetComponent<Health>().damageDealt += amount;
                workingObj.GetComponent<Health>().totalHit += 1;
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
                comboCounter.IncrementEHitCounter();
            }
            if (gameObject.tag == "Enemy")
            {
                workingObj = GameObject.FindGameObjectWithTag("Player");
                workingObj.GetComponent<Health>().damageDealt += amount; //Update the damage dealt on the appropriate character script.
                workingObj.GetComponent<Health>().totalHit += 1;
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
                comboCounter.IncrementPHitCounter();
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
            UpdateSlider(amount);
            fill.color = gradient.Evaluate(healthSlider.normalizedValue);  //Changes the health bar colour based on the character's HP
            if (gameObject.tag == "Player")
            {
                workingObj = GameObject.FindGameObjectWithTag("Enemy");
                workingObj.GetComponent<Health>().damageDealt += amount;
                workingObj.GetComponent<Health>().totalHit += 1;
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
                comboCounter.IncrementEHitCounter();
            }
            if (gameObject.tag == "Enemy")
            {
                workingObj = GameObject.FindGameObjectWithTag("Player");
                workingObj.GetComponent<Health>().damageDealt += amount; //Update the damage dealt on the appropriate character script.
                workingObj.GetComponent<Health>().totalHit += 1;
                workingObj.GetComponent<SpecialAttackControl>().IncrementSpecialValue(10);
                comboCounter.IncrementPHitCounter();
            }
        }
    }

    /// <summary>
    /// Gradually change the health slider so it is smooth.
    /// </summary>
    private void UpdateSlider(float amount = 0)
    {
        if (amount != 0)
        {
            health -= amount;
            target = health;
        }
        if (current != target)
        {
            healthSlider.value = healthSlider.value - Time.deltaTime * 30;
            current = healthSlider.value;
        }
        if (current < target)
        {
            current = target;
            healthSlider.value = target;
        }
    }

}
