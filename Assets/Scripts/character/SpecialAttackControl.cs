using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackControl : MonoBehaviour
{

    public GameObject specialAttackCheck;

    public int specialVal;
    private GameStartControl gameControlAccess;
    private Slider specialSlider;
    private Animator animator;
    private WaitForSeconds chargingTime;
    private SpriteRenderer[] charPBodyParts;
    private SpriteRenderer[] charEBodyParts;
    private CMovement cMovement;
    private RaycastHit2D raycast;
    private RaycastHit2D raycastLeft;
    public float specialDOT;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        cMovement = GetComponent<CMovement>();
        animator = GetComponent<Animator>();
        if (gameObject.tag == "Player")
        {
            specialSlider = GameObject.FindWithTag("PlayerSpecial").GetComponent<Slider>();
            specialSlider.value = specialVal;
        }
        else if (gameObject.tag == "Enemy")
        {
            specialSlider = GameObject.FindWithTag("EnemySpecial").GetComponent<Slider>();
            specialSlider.value = specialVal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        specialSlider.value = specialVal;
        Debug.DrawRay(gameObject.transform.position, Vector2.left, Color.blue);
        Debug.DrawRay(gameObject.transform.position, Vector2.right, Color.blue);
    }

    public void IncrementSpecialValue(int addSp) 
    {
        specialVal += addSp;
    }

    public void SpecialTrigger(string tag)
    {
        if (specialVal >= 100 && tag == "Player")
        {
            specialAttackCheck.SetActive(true);
            GameObject.FindWithTag("Enemy").GetComponent<Health>().TakeHealth(gameObject.GetComponent<CAttack>().damageArray[3]);
            specialVal = 0;
        }

        if (specialVal >= 100 && tag == "Enemy")
        {
            specialAttackCheck.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<Health>().TakeHealth(gameObject.GetComponent<CAttack>().damageArray[3]);
            specialVal = 0;
        }
    }

    public void SpecialOff(string tag) { 
        if (tag == "Player")
        {
            specialAttackCheck.SetActive(false);
            done = false;
        }
        if (tag == "Enemy")
        {
            specialAttackCheck.SetActive(false);
            done = false;
        }
    }
}
