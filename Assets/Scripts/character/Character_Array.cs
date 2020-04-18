﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Array : MonoBehaviour
{

    private string[] characterNames = {"Red Chump", "Robot Boi", "Blue Chump", "Penguin Boi"};
    private string[] characterDesc = 
        { "A red boi chump.",
        "A roboty boi chump",
        "A blue boi chump :)",
        "A penguiny? boi chump"};
    private static int playerNumChoice;

    public List<Sprite> characterImages;
    public List<Sprite> characterBackgrounds;
    public List<GameObject> characterPrefabs;

    //public int numOfCharacters;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void setPlayerChoice(int choiceVal)
    {
        playerNumChoice = choiceVal;
    }
    public int getPlayerChoice()
    {
        return playerNumChoice;
    }

    public string[] getCharNames()
    {
        return characterNames;

    }
    public string[] getCharDesc()
    {
        return characterDesc;
    }

}
