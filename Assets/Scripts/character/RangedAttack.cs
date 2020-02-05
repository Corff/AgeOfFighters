using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotOrigin;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && gameObject.tag == "Player")
        {
            var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
            proj.tag = "Player";
            anim.SetTrigger("rangedAttack");
        }

        else if (Input.GetKeyDown("p") && gameObject.tag == "Enemy")
        {
            var proj = Instantiate(projectile, shotOrigin.position, transform.rotation);
            proj.tag = "Enemy";
            anim.SetTrigger("rangedAttack");
        }
    }
}
