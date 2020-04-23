using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject gOPanel;

    private Health checkPHealth;//Checks current health of Player
    private Health checkEHealth;//Checks current health of Enemy
    private FadePanel fp;


    private void Start()
    {
        //fp = gOPanel.GetComponent<FadePanel>();
        //checkPHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        //checkEHealth = GameObject.FindWithTag("Enemy").GetComponent<Health>();
    }
    void Update()
    {
        fp = gOPanel.GetComponent<FadePanel>();
        checkPHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
        checkEHealth = GameObject.FindWithTag("Enemy").GetComponent<Health>();
        if (checkPHealth.dead == true || checkEHealth.dead == true)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GameObject.FindWithTag("Enemy").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (checkPHealth.deathTag == "Player")
            {

                gOPanel.GetComponentInChildren<Text>().text = "Player 2";
                gOPanel.SetActive(true);
                fp.GOPanelFadeON = true;
            }
            else if (checkEHealth.deathTag == "Enemy")
            {
                gOPanel.GetComponentInChildren<Text>().text = "Player 1";
                gOPanel.SetActive(true);
                fp.GOPanelFadeON = true;
            }
        }
    }
}
