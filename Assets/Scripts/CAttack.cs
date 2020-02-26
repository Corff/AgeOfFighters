using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAttack : MonoBehaviour
{

    public GameObject punchCheck;
    public GameObject heavyPunchCheck;
    public Transform shotOrigin;
    public GameObject projectile;
    public TimeControl timer;
    public int[] damageArray = { 0, 0, 0, 0 };
    public float rangedCooldown;

    private Animator anim;
    private SpecialAttackControl specialAC;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        specialAC = GetComponent<SpecialAttackControl>();
        timer = Instantiate(timer, new Vector2(100, 100), Quaternion.identity).GetComponent<TimeControl>();
        timer.countDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LightAttack") && gameObject.tag == "Player")
        {
            punchCheck.SetActive(true);
            anim.SetTrigger("isPunching");
        }

        if (Input.GetButtonUp("LightAttack") && gameObject.tag == "Player")
        {
            punchCheck.SetActive(false);
        }

        if (Input.GetButtonDown("HeavyAttack") && gameObject.tag == "Player")
        {
            heavyPunchCheck.SetActive(true);
            anim.SetTrigger("Heavy Punch");
        }

        if (Input.GetButtonUp("HeavyAttack") && gameObject.tag == "Player")
        {
            heavyPunchCheck.SetActive(false);
        }

        if (Input.GetButtonDown("RangedAttack") && gameObject.tag == "Player")
        {
            Debug.Log(timer.time);
            if (timer.timeUp)
            {
                var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
                proj.tag = "Player";
                anim.SetTrigger("rangedAttack");
                timer.time = rangedCooldown;
            }
        }

        if (Input.GetButtonDown("EnemyLightAttack") && gameObject.tag == "Enemy")
        {
            punchCheck.SetActive(true);
            anim.SetTrigger("Punch");
        }

        if (Input.GetButtonUp("EnemyLightAttack") && gameObject.tag == "Enemy")
        {
            punchCheck.SetActive(false);
        }

        if (Input.GetButtonDown("EnemyHeavyAttack") && gameObject.tag == "Enemy")
        {
            heavyPunchCheck.SetActive(true);
            anim.SetTrigger("Heavy Punch");
        }

        if (Input.GetButtonUp("EnemyHeavyAttack") && gameObject.tag == "Enemy")
        {
            heavyPunchCheck.SetActive(false);
        }

        if (Input.GetButtonDown("EnemyRangedAttack") && gameObject.tag == "Enemy")
        {
            Debug.Log(timer.time);
            if (timer.timeUp)
            {
                var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
                proj.tag = "Enemy";
                anim.SetTrigger("rangedAttack");
                timer.time = rangedCooldown;
            }
        }

        if (Input.GetButtonDown("SpecialAttack") && gameObject.tag == "Player")
        {
            specialAC.SpecialTrigger(gameObject.tag);
        }
        if (Input.GetButtonDown("EnemySpecialAttack") && gameObject.tag == "Enemy")
        {
            specialAC.SpecialTrigger(gameObject.tag);
        }

        if (Input.GetButtonUp("SpecialAttack") && gameObject.tag == "Player")
        {
            specialAC.SpecialOff(gameObject.tag);
        }
        if (Input.GetButtonUp("EnemySpecialAttack") && gameObject.tag == "Enemy")
        {
            specialAC.SpecialOff(gameObject.tag);
        }
    }
}
