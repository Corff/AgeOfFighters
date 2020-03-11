using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSelect : MonoBehaviour
{
    public int mapNum;
    public GameObject charInfo;
    public bool isSingle;

    private Character_Array charDataArray;

    // Start is called before the first frame update



    void Start() {
        DontDestroyOnLoad(this.gameObject);
        charDataArray = charInfo.GetComponent<Character_Array>();
        isSingle = charDataArray.isSingleplayer;
    }
}
