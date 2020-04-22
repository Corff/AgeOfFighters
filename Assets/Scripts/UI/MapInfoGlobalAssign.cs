using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapInfoGlobalAssign : MonoBehaviour
{
    public GameObject mapInfo;
    public GameObject namePanel;
    public GameObject descPanel;
    public GameObject mapPanel;
    public GameObject imagePanel;

    private string[] mapNames;
    private string[] mapDesc;
    private Map_Array mapArray;



    // Start is called before the first frame update



    void Start()
    {
        mapArray = mapInfo.GetComponent<Map_Array>();
        mapNames = mapArray.getMapNames();
        mapDesc = mapArray.getMapDesc();

    }


    public void selectRequestOn(int charMNum)
    {
        mapArray.setMapPlayerChoice(charMNum);
        mapPanel.SetActive(true);
        namePanel.GetComponent<Text>().text = mapNames[charMNum];
        descPanel.GetComponent<Text>().text = mapDesc[charMNum];
        imagePanel.GetComponent<Image>().sprite = mapArray.mapImages[charMNum];
    }
    public void selectRequestOff()
    {
        mapPanel.SetActive(false);
    }
}