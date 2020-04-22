using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class mapAssignStore : MonoBehaviour
{
    public int mapNum;
    public GameObject mapInfo;

    private Image currentMapImage;
    private Map_Array mapArray;



    // Start is called before the first frame update



    void Start()
    {

        currentMapImage = GetComponent<Image>();
        mapArray = mapInfo.GetComponent<Map_Array>();
        currentMapImage.sprite = mapArray.mapImages[mapNum];
       
    }

}