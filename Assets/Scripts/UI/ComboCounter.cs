using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{

    public GameObject PCCounter;
    public GameObject ECCounter;
    private Text PCounter;
    private Text ECounter;
    private Health pHpAccess;
    private Health eHpAccess;
    private int phitCounter;
    private int ehitCounter;
    private int tempCount;
    private float coolTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        PCounter = PCCounter.GetComponentInChildren<Text>();
        ECounter = ECCounter.GetComponentInChildren<Text>();
        PCounter.text = "0";
        ECounter.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        PCounter.text = phitCounter.ToString();
        ECounter.text = ehitCounter.ToString();
        if (ehitCounter <= 0)
        {
            if(coolTime >= 2f){
                tempCount = phitCounter;
            }
            coolTime -= Time.deltaTime;
            if (coolTime <= 0f)
            {
                if(tempCount == phitCounter)
                {
                    PCCounter.SetActive(false);
                    phitCounter = 0;
                    coolTime = 2f;
                }
                else
                {
                    coolTime = 2f;
                }
            }
        }
        if (phitCounter <= 0)
        {
            if (coolTime >= 2f)
            {
                tempCount = ehitCounter;
            }
            coolTime -= Time.deltaTime;
            if (coolTime <= 0f)
            {
                if (tempCount == ehitCounter)
                {
                    ECCounter.SetActive(false);
                    ehitCounter = 0;
                    coolTime = 2f;
                }
                else
                {
                    coolTime = 2f;
                }
            }
        }

    }


    public void IncrementPHitCounter()
    {
        phitCounter = phitCounter + 1;
        PCCounter.SetActive(true);
        ECCounter.SetActive(false);
        ehitCounter = 0;
    }

    public void IncrementEHitCounter()
    {
        ehitCounter = ehitCounter + 1;
        PCCounter.SetActive(false);
        ECCounter.SetActive(true);
        phitCounter = 0;
    }

}
