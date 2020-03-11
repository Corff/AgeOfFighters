using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject gOPanel;

    private Health checkPHealth;//Checks current health of Player
    private Health checkEHealth;//Checks current health of Enemy

    CharMovement playerM;
    CharMovement enemyM;



    private void Start()
    {
        checkPHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        checkEHealth = GameObject.FindWithTag("Enemy").GetComponent<Health>();
        playerM = GameObject.FindGameObjectWithTag("Player").GetComponent<CharMovement>();
        enemyM = GameObject.FindGameObjectWithTag("Enemy").GetComponent<CharMovement>();
    }
    void Update()
    {
       
        if (checkPHealth.dead == true || checkEHealth.dead == true)
        {
            if(checkPHealth.deathTag == "Player")
            {

                gOPanel.GetComponentInChildren<Text>().text = "Player 2";
                gOPanel.SetActive(true);
                
                
            }
            else if (checkEHealth.deathTag == "Enemy")
            {
                gOPanel.GetComponentInChildren<Text>().text = "Player 1";
                gOPanel.SetActive(true);
            }
            
        }
    }
}
