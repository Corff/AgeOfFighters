using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private Text damageDealt;
    private Text damageBlocked;
    private Text lightAttacks;
    private Text heavyAttacks;
    private Text rangedAttacks;
    private Text specialAttacks;
    private Text totalHits;
    private Text[] textList;
    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Player1")
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
        else
        {
            health = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Health>();
        }
        textList = gameObject.GetComponentsInChildren<Text>();
        damageDealt = textList[1].GetComponent<Text>();
        damageBlocked = textList[2].GetComponent<Text>();
        lightAttacks = textList[3].GetComponent<Text>();
        heavyAttacks = textList[4].GetComponent<Text>();
        rangedAttacks = textList[5].GetComponent<Text>();
        totalHits = textList[7].GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        damageDealt.text = $"Damage Dealt: {health.damageDealt}";
        damageBlocked.text = $"Damage Blocked: {health.damageBlocked}";
        lightAttacks.text = $"Light Attacks Used: {health.lightAttackUsed}";
        heavyAttacks.text = $"Heavy Attacks Used: {health.heavyAttackUsed}";
        rangedAttacks.text = $"Ranged Attacks Used: {health.rangedAttackUsed}";
        //specialAttacks.text = $"Special Attacks Used: {specialAttacks}";
        totalHits.text = $"Total Hits: {health.totalHit}";
    }
}
