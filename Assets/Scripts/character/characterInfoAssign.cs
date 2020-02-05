using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class characterInfoAssign : MonoBehaviour
{
    public int charNum;
    public GameObject charInfo;
    public bool isEnemy = false;

    private Image currentCharImage;
    private Character_Array charDataArray;

    // Start is called before the first frame update



    void Start()
    {

        currentCharImage = GetComponent<Image>();
        charDataArray = charInfo.GetComponent<Character_Array>();
        currentCharImage.sprite = charDataArray.characterImages[charNum];
    }
}
