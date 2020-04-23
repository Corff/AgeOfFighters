using System.Collections;
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
    private static int multiPlayerNumChoice1;
    private static int multiPlayerNumChoice2;
    public List<Sprite> characterImages;
    public List<Sprite> characterBackgrounds;
    public List<GameObject> characterPrefabs;

    //public int numOfCharacters;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void setPlayerChoice(int choiceVal, int charT)
    {
        if(PlayModeControl.isMultiplayer == false)
        {
            playerNumChoice = choiceVal;
        }
        else if(PlayModeControl.isMultiplayer == true && charT == 0)
        {
            multiPlayerNumChoice1 = choiceVal;
        }
        else if (PlayModeControl.isMultiplayer == true && charT == 1)
        {
            multiPlayerNumChoice2= choiceVal;
        }
    }
    public int getPlayerChoice(int charT)
    {
        if(PlayModeControl.isMultiplayer == true && charT == 0)
        {
            return multiPlayerNumChoice1;
        }

        else if (PlayModeControl.isMultiplayer == true && charT == 1)
        {
            return multiPlayerNumChoice2;
        }
        else
        {
            return playerNumChoice;
        }
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
