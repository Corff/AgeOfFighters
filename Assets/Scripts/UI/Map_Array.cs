using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Array : MonoBehaviour
{




    private string[] mapNames = { "City", "Night Farmlnad", "Fenland", "Desert", "Mountains" };
    private string[] mapDesc =
        { "Cityscape, watch out for Shia LaBouef",
        "Lovely farm with a starry sky", "Flatest Place On Earth", "This place is damn hot, wear sandals.", "Dem some big bois in the background, don't try to climb them you'll probably die"};
    private static int playerMapNumChoice;

    public List<Sprite> mapImages;

    public List<Material> mapTextures;

    //public int numOfMaps;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void setMapPlayerChoice(int choiceVal)
    {
        playerMapNumChoice = choiceVal;
    }
    public int getMapPlayerChoice()
    {
        return playerMapNumChoice;
    }

    public string[] getMapNames()
    {
        return mapNames;

    }
    public string[] getMapDesc()
    {
        return mapDesc;
    }


}


