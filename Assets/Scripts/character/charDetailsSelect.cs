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

    private GameObject charPOne;
    private GameObject charPTwo;
    private string[] charNames;
    private string[] charDescs;
    private bool isOn = false;
    private bool isClicked;
    private bool secondSelect;



    // Start is called before the first frame update
    void Start()
    {
        if (PlayModeControl.isMultiplayer == true)
        {
            charPOne = GameObject.FindWithTag("CharSel1");
            charPTwo = GameObject.FindWithTag("CharSel2");
            charPOne.SetActive(false);
            charPTwo.SetActive(false);
        }
        charNames = charInfo.GetComponent<Character_Array>().getCharNames();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void hoverRequestOn(int charSNum)
    {
        if (PlayModeControl.isMultiplayer == true && secondSelect == false)
        {
            charPOne.SetActive(true);
            charPOne.transform.Find("NamePanel").GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharNames()[charSNum];
            charPOne.transform.Find("DescPanel").GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharDesc()[charSNum];
            charPOne.GetComponentInChildren<Image>().sprite = charInfo.GetComponent<Character_Array>().characterImages[charSNum];
            charInfo.GetComponent<Character_Array>().setPlayerChoice(charSNum, 0);
        }
        else if (PlayModeControl.isMultiplayer == true && secondSelect == true)
        {
            charPOne.SetActive(true);
            charPTwo.SetActive(true);
            charPTwo.transform.Find("NamePanel 2").GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharNames()[charSNum];
            charPTwo.transform.Find("DescPanel 2").GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharDesc()[charSNum];
            charPTwo.GetComponentInChildren<Image>().sprite = charInfo.GetComponent<Character_Array>().characterImages[charSNum];
            charInfo.GetComponent<Character_Array>().setPlayerChoice(charSNum, 1);
        }
        else
        {
            charPanel.SetActive(true);
            namePanel.GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharNames()[charSNum];
            descPanel.GetComponent<TextMeshProUGUI>().text = charInfo.GetComponent<Character_Array>().getCharDesc()[charSNum];
            charPanel.GetComponentInChildren<Image>().sprite = charInfo.GetComponent<Character_Array>().characterImages[charSNum];
            charInfo.GetComponent<Character_Array>().setPlayerChoice(charSNum, 0);
        }
    }

    public void hoverRequestOof()
    {
        if (PlayModeControl.isMultiplayer == false)
        {
            charPanel.SetActive(false);
        }
        else if(PlayModeControl.isMultiplayer == true && secondSelect == false && isClicked == false)
        {
            charPOne.SetActive(false);
        }
        else if (PlayModeControl.isMultiplayer == true && secondSelect == true && isClicked == false)
        {
            charPTwo.SetActive(false);
        }
    }

    public  void setIsClick()
    {
        isClicked = true;
    }

    public void selectClick()
    {
        GameObject.FindWithTag("CharSel1").GetComponentInChildren<Button>().interactable = false;
        secondSelect = true;
        isClicked = false;
    }
}
