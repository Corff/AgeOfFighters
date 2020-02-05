using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackControl : MonoBehaviour
{

    public GameObject specialAttackCheck;

    private int specialVal;
    private GameStartControl gameControlAccess;
    private Slider specialSlider;
    private Animator animator;
    private WaitForSeconds chargingTime;
    private SpriteRenderer[] charPBodyParts;
    private SpriteRenderer[] charEBodyParts;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        if (gameObject.tag == "Player")
        {
            specialSlider = GameObject.FindWithTag("PlayerSpecial").GetComponent<Slider>();
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("BodyPart"))
            {
                charPBodyParts = gameObject.GetComponentsInChildren<SpriteRenderer>();
            }
            specialSlider.value = specialVal;
        }
        else if (gameObject.tag == "Enemy")
        {
            specialSlider = GameObject.FindWithTag("EnemySpecial").GetComponent<Slider>();
            foreach(GameObject x in GameObject.FindGameObjectsWithTag("BodyPart"))
            {
                charEBodyParts = gameObject.GetComponentsInChildren<SpriteRenderer>();
            }
            specialSlider.value = specialVal;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(specialVal >= 100 && Input.GetKeyDown(KeyCode.F) && gameObject.tag == "Player")
        {
            foreach(SpriteRenderer x in charPBodyParts)
            {
                x.GetComponent<SpriteRenderer>().color = Color.red;
            }
            specialAttackCheck.SetActive(true);
            animator.SetTrigger("Special Attack");
            specialVal = 0;
        }

        if(Input.GetKeyUp(KeyCode.F) && gameObject.tag == "Player")
        {
            foreach (SpriteRenderer x in charPBodyParts)
            {
                x.GetComponent<SpriteRenderer>().color = Color.white;
            }
            specialAttackCheck.SetActive(false);
        }

        if (specialVal >= 100 && Input.GetKeyDown(KeyCode.RightShift) && gameObject.tag == "Enemy")
        {
            foreach (SpriteRenderer x in charEBodyParts)
            {
                x.GetComponent<SpriteRenderer>().color = Color.red;
            }
            specialAttackCheck.SetActive(true);
            animator.SetTrigger("Special Attack");
            specialVal = 0;
        }

        if (Input.GetKeyUp(KeyCode.RightShift) && gameObject.tag == "Enemy")
        {
            foreach (SpriteRenderer x in charEBodyParts)
            {
                x.GetComponent<SpriteRenderer>().color = Color.white;
            }
            specialAttackCheck.SetActive(false);
        }
        specialSlider.value = specialVal;
    }

    public void setSpecialValue(int addSp)
    {
        specialVal = specialVal + addSp;
    }
}
