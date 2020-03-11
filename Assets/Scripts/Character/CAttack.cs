﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CAttack : MonoBehaviour
{

    public GameObject punchCheck;
    public GameObject heavyPunchCheck;
    public Transform shotOrigin;
    public GameObject projectile;
    public TimeControl timer;
    public float[] damageArray = { 0, 0, 0, 0 };
    public float rangedCooldown;
    public float stale = 0f;

    private float lightAttackDamage;
    private float heavyAttackDamage;
    private float rangedAttackDamage;
    private float specialAttackDamage;
    private Animator anim;
    private SpecialAttackControl specialAC;
    private Queue<int> moveQueue;
    private Health health;
    private CharMovement charMovement;
    private Dictionary<string, bool> coolDownState;
    private float attackCooldown = 0.5f; //If you want individual attacks to have individual cooldowns, then make the dictionary an array.
    private SFXController soundAccess;

    // Start is called before the first frame update
    void Start()
    {
        coolDownState = new Dictionary<string, bool>()
        {
            {"LightAttack", true },
            {"HeavyAttack", true },
            {"RangedAttack", true },
        };
        charMovement = GetComponent<CharMovement>();
        health = GetComponent<Health>();
        soundAccess = GameObject.FindGameObjectWithTag("GameController").GetComponent<SFXController>();
        anim = GetComponent<Animator>();
        specialAC = GetComponent<SpecialAttackControl>();
        timer = Instantiate(timer, new Vector2(100, 100), Quaternion.identity).GetComponent<TimeControl>();
        timer.countDown = true;
        moveQueue = new Queue<int>(9);
        lightAttackDamage = damageArray[0];
        heavyAttackDamage = damageArray[1];
        rangedAttackDamage = damageArray[2];
        specialAttackDamage = damageArray[3];
        Debug.Log(health.health);
    }

    // Update is called once per frame
    void Update()
    {
        if (charMovement.inputActive)
        {
            if (Input.GetButtonDown("LightAttack") && gameObject.tag == "Player")
            {
                LightAttackOn();
            }

            if (Input.GetButtonUp("LightAttack") && gameObject.tag == "Player")
            {
                LightAttackOff();
            }

            if (Input.GetButtonDown("HeavyAttack") && gameObject.tag == "Player")
            {
                HeavyAttackOn();
            }

            if (Input.GetButtonUp("HeavyAttack") && gameObject.tag == "Player")
            {
                HeavyAttackOff();
            }

            if (Input.GetButtonDown("RangedAttack") && gameObject.tag == "Player")
            {
                RangedAttackOn();
                //Debug.Log("Ranged");
            }

            if (Input.GetButtonDown("EnemyLightAttack") && gameObject.tag == "Enemy")
            {
                soundAccess.soundCall(gameObject, "Punch");
                punchCheck.SetActive(true);
                anim.SetTrigger("isPunching");
                LightAttackOn();
            }

            if (Input.GetButtonUp("EnemyLightAttack") && gameObject.tag == "Enemy")
            {
                LightAttackOff();
            }

            if (Input.GetButtonDown("EnemyHeavyAttack") && gameObject.tag == "Enemy")
            {
                soundAccess.soundCall(gameObject, "HPunch");
                heavyPunchCheck.SetActive(true);
                anim.SetTrigger("Heavy Punch");
                HeavyAttackOn();
            }

            if (Input.GetButtonUp("EnemyHeavyAttack") && gameObject.tag == "Enemy")
            {
                HeavyAttackOff();
            }

            if (Input.GetButtonDown("EnemyRangedAttack") && gameObject.tag == "Enemy")
            {
                RangedAttackOn();
            }

            if (Input.GetButtonDown("SpecialAttack") && gameObject.tag == "Player")
            {
                soundAccess.soundCall(gameObject, "Special");
                specialAC.SpecialTrigger(gameObject.tag);
            }
            if (Input.GetButtonDown("EnemySpecialAttack") && gameObject.tag == "Enemy")
            {
                specialAC.SpecialTrigger(gameObject.tag);
            }

            if (Input.GetButtonUp("SpecialAttack") && gameObject.tag == "Player")
            {
                soundAccess.soundCall(gameObject, "Special");
                specialAC.SpecialOff(gameObject.tag);
            }
            if (Input.GetButtonUp("EnemySpecialAttack") && gameObject.tag == "Enemy")
            {
                specialAC.SpecialOff(gameObject.tag);
            }
        }
    }
    public void LightAttackOn()
    {
        if (!coolDownState["LightAttack"])
        {
            return;
        }
        StartCoroutine(AttackDelay("LightAttack"));
        soundAccess.soundCall(gameObject, "Punch");
        punchCheck.SetActive(true);
        anim.SetTrigger("isPunching");
        health.lightAttackUsed += 1;
        if (moveQueue.Count == 9)
        {
            moveQueue.Dequeue();
        }

        moveQueue.Enqueue(1);
        stale = StaleMoves(1);
        if (stale == 0)
        {
            damageArray[0] = lightAttackDamage;
        }
        else
        {
            damageArray[0] -= stale / 5;
            if (damageArray[0] < 1)
            {
                damageArray[0] = 1;
            }
        }
    }

    public void HeavyAttackOn()
    {
        if (!coolDownState["HeavyAttack"])
        {
            return;
        }
        StartCoroutine(AttackDelay("HeavyAttack"));
        soundAccess.soundCall(gameObject, "HPunch");
        heavyPunchCheck.SetActive(true);
        anim.SetTrigger("isHeavyPunching");
        health.heavyAttackUsed += 1;
        if (moveQueue.Count == 9)
        {
            moveQueue.Dequeue();
        }
        moveQueue.Enqueue(2);
        stale = (StaleMoves(2));
        if (stale == 0)
        {
            damageArray[1] = heavyAttackDamage;
        }
        else
        {
            damageArray[1] -= stale / 2;
            if (damageArray[1] < 1)
            {
                damageArray[1] = 1;
            }
        }
    }

    public void RangedAttackOn()
    {
        if (!coolDownState["RangedAttack"])
        {
            return;
        }
        StartCoroutine(AttackDelay("RangedAttack"));
        health.rangedAttackUsed += 1;
        if (timer.timeUp)
        {
            StartCoroutine(RangedAttack());
            timer.time = rangedCooldown;
            if (moveQueue.Count == 9)
            {
                moveQueue.Dequeue();
            }
            moveQueue.Enqueue(3);
            stale = StaleMoves(3);
            if (stale == 0)
            {
                damageArray[2] = rangedAttackDamage;
            }
            else
            {
                damageArray[2] -= stale / 2.5f;
                if (damageArray[2] < 1)
                {
                    damageArray[2] = 1;
                }
            }
        }
    }

    public void LightAttackOff()
    {
        punchCheck.SetActive(false);
    }

    public void HeavyAttackOff()
    {
        heavyPunchCheck.SetActive(false);
    }

    float StaleMoves(int n)
    {
        float scale = 0f;
        foreach (int i in moveQueue)
        {
            if (i == 1)
            {
                if (n == 1)
                {
                    scale += 1;
                }
            }
            else if (i == 2)
            {
                if (n == 2)
                {
                    scale += 1;
                }
            }
            else if (i == 3)
            {
                if (n == 3)
                {
                    scale += 1;
                }
            }
        }
        return scale - 1;
    }
    IEnumerator RangedAttack()
    {

        soundAccess.soundCall(gameObject, "Ranged");
        anim.SetTrigger("rangedAttack");

        yield return new WaitForSeconds(0.4f);
        var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
        proj.tag = gameObject.tag;
    }

    IEnumerator AttackDelay(string attackName)
    {
        coolDownState[attackName] = false;
        yield return new WaitForSeconds(attackCooldown);
        coolDownState[attackName] = true;
    }
}

