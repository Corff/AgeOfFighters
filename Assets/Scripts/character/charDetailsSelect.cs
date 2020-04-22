using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class charDetailsSelect : MonoBehaviour
{

    public GameObject namePanel;
    public GameObject descPanel;
    public GameObject charPanel;
    public GameObject charInfo;

    private string[] charNames;
    private string[] charDescs;
    private bool isOn = false;


    // Start is called before the first frame update
    void Start()
    {
        charNames = charInfo.GetComponent<Character_Array>().getCharNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void selectRequestOn(int charSNum)
    {
        isOn = true;
        charPanel.SetActive(true);
        namePanel.GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharNames()[charSNum];
        descPanel.GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharDesc()[charSNum];
        charPanel.GetComponentInChildren<Image>().sprite = charInfo.GetComponent<Character_Array>().characterImages[charSNum];
        charInfo.GetComponent<Character_Array>().setPlayerChoice(charSNum);

    }
}
