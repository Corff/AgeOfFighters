using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blocking : MonoBehaviour
{
    //Variable names need reconsidering

    private float stamina = 100; //This is also the max value of the timer
    public bool blocked = false;
    public float blockCost;
    public float perfectWindow; //The time window for a perfect block to count
    public bool perfectBlock = false;
    public TimeControl timer;
    private TimeControl perfect;
    public GameObject Timer;
    public Animator anim;
    private SFXController soundAccess;
    private CharMovement charMovement;

    public void ReturnProjectile(GameObject projectileGO)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        charMovement = GetComponent<CharMovement>();
        if (gameObject.tag == "Player")
        {
            staminaSlider = GameObject.FindGameObjectWithTag("PlayerStamina").GetComponent<Slider>();
        }
        else if (gameObject.tag == "Enemy")
        {
            staminaSlider = GameObject.FindGameObjectWithTag("EnemyStamina").GetComponent<Slider>();
        }
        soundAccess = GameObject.FindGameObjectWithTag("GameController").GetComponent<SFXController>();
        timer = Instantiate(Timer, new Vector2(100, 100), Quaternion.identity).GetComponent<TimeControl>();
        perfect = Instantiate(Timer, new Vector2(100, 100), Quaternion.identity).GetComponent<TimeControl>();
        //timer = new Timer(stamina, false, staminaDegenRate); //Create a timer for counting down the remaining block time.
        timer.countDown = false;
        //perfect = new Timer(perfectWindow, false); //Create a timer for the perfect block window.
        perfect.time = perfectWindow;
        perfect.countDown = false;
    }

    /*The Current block design means that the block works in any direction, even though currently the shield is only shown in one direction.
     * Logic:
     If the block is just pressed activate the perfect block, once perfect block runs out activate normal blocking.
     Once the block time / stamina has run out and the block button is still being pressed, turn off blocking but do not start the regen of stamina
     If the block key is released stamina will regen upto the maximum value.*/
    private void Update()
    {

        if (gameObject.tag == "Player")
        {
            if (Input.GetButton("Block")) //If there is stamina left...
            {
                if (Input.GetButton("Block") && !timer.timeUp) //If there is stamina left...
                {
                    if (Input.GetButtonDown("Block"))
                    {
                        soundAccess.soundCall(gameObject, "Block");
                    }
                    block.SetActive(true);
                    anim.SetBool("isBlocking", true);
                    perfect.countDown = true; //Start the degen for stamina and the perfect window
                    timer.countDown = true;
                    if (!perfect.timeUp) //If there is time left on the perfect window
                    {
                        perfectBlock = true;
                    }
                    else //If the perfect window is up set normal blocking value
                    {
                        perfectBlock = false;
                        blocked = true;
                    }
                    //ENABLE THE BLOCK PREFAB
                }
                anim.SetBool("isBlocking", true);
                perfect.countDown = true; //Start the degen for stamina and the perfect window
                timer.countDown = true;
                if (!perfect.timeUp) //If there is time left on the perfect window
                {
                    if (Input.GetButtonUp("Block"))
                    {
                        soundAccess.soundCall(gameObject, "Block");
                    }
                    block.SetActive(false);
                    anim.SetBool("isBlocking", false);
                    blocked = false;
                    //Block fail sound effect (Play once). Use flag to tell.
                }
                //If the button is released start regening
                else
                {
                    if (Input.GetButtonUp("Block"))
                    {
                        soundAccess.soundCall(gameObject, "Block");
                    }
                    block.SetActive(false);
                    anim.SetBool("isBlocking", false);
                    perfectBlock = false;
                    blocked = false;
                    timer.countDown = false;
                    if (timer.time < stamina) //Refill stamina upto the maximum value
                    {
                        timer.time += Time.deltaTime * staminaRechargeRate;
                    }
                    else
                    {
                        timer.time = stamina;
                    }
                }
            }

            //If the button is released start regening
            else
            {
                if (Input.GetButtonUp("Block"))
                {
                    soundAccess.soundCall(gameObject, "Block");
                    block.SetActive(true);
                    perfect.countDown = true; //Start the degen for stamina and the perfect window
                    timer.countDown = true;
                    if (!perfect.timeUp) //If there is time left on the perfect window
                    {
                        perfectBlock = true;
                    }
                    else //If the perfect window is up set normal blocking value
                    {
                        perfectBlock = false;
                        blocked = true;
                    }

                }
                anim.SetBool("isBlocking", false);
                perfectBlock = false;
                blocked = false;
                timer.countDown = false;
            }
        }

        else if (gameObject.tag == "Enemy")
        {
            if (Input.GetButton("EnemyBlock")) //If there is stamina left...
            {
                if (Input.GetButtonDown("EnemyBlock"))
                {
                    soundAccess.soundCall(gameObject, "Block");
                }
                anim.SetBool("isBlocking", true);
                perfect.countDown = true; //Start the degen for stamina and the perfect window
                timer.countDown = true;
                if (!perfect.timeUp) //If there is time left on the perfect window
                {
                    perfectBlock = true;
                }
                else //If the perfect window is up set normal blocking value
                {
                    perfectBlock = false;
                    blocked = true;
                }
                //ENABLE THE BLOCK PREFAB
            }
            //If the button is released start regening
            else
            {
                if (Input.GetButtonUp("EnemyBlock"))
                {
                    soundAccess.soundCall(gameObject, "Block");
                }
                anim.SetBool("isBlocking", false);
                perfectBlock = false;
                blocked = false;
                timer.countDown = false;
            }
        }
    }
}
