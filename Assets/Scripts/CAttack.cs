using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAttack : MonoBehaviour
{

    public GameObject punchCheck;
    public GameObject heavyPunchCheck;
    public Transform shotOrigin;
    public GameObject projectile;

    private Animator anim;
    public int[] damageArray = { 0, 0, 0, 0 };

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LightAttack") && gameObject.tag == "Player")
        {
            punchCheck.SetActive(true);
            anim.SetTrigger("Punch");
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
            var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
            proj.tag = "Player";
            anim.SetTrigger("rangedAttack");
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
            var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
            proj.tag = "Enemy";
            anim.SetTrigger("rangedAttack");
        }
    }
}
