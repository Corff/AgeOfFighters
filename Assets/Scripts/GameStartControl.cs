using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartControl : MonoBehaviour
{
    public GameObject charInfo;
    public bool isEnemy;
    public bool isVs;
    public bool genRandom;
    public bool isAi;
    public GameObject background;
    public GameObject mapInf;

    private Map_Array mapScript;
    private int playerChoice;
    private Image vsImage;
    private List<Sprite> charSpriteList;
    private List<Sprite> charBackList;
    private string[] charTempNames;
    private Text vsText;
    private static int enemyNum = 0;
    private Image backImage;
    private GameObject playerPrefab;
    private GameObject enemyPrefab;
    private List<GameObject> characterPrefabList;
    private SpriteRenderer[] playerBodyParts;
    private SpriteRenderer[] enemyBodyParts;




    // Start is called before the first frame update
    void Start()
    {
        charTempNames = charInfo.GetComponent<Character_Array>().getCharNames();
        playerChoice = charInfo.GetComponent<Character_Array>().getPlayerChoice();
        vsImage = GetComponent<Image>();
        backImage = GetComponentInParent<Image>();
        vsText = GetComponentInChildren<Text>();
        charSpriteList = charInfo.GetComponent<Character_Array>().characterImages;
        charBackList = charInfo.GetComponent<Character_Array>().characterBackgrounds;
        characterPrefabList = charInfo.GetComponent<Character_Array>().characterPrefabs;
        mapScript = mapInf.GetComponent<Map_Array>();
        background.GetComponent<MeshRenderer>().material = mapScript.mapTextures[mapScript.getMapPlayerChoice()];
        Debug.Log("Length minus 1 " + (characterPrefabList.Count - 1));
        if(genRandom == true)
        {
            enemyNum = Random.Range(0, characterPrefabList.Count);
        }
        Debug.Log("EnemyNum " + enemyNum);
        isAi = !PlayModeControl.isMultiplayer;
        if (isVs == false)
        {
            Debug.Log("EnemyNum " + enemyNum);
            playerPrefab = characterPrefabList[playerChoice];
            enemyPrefab = characterPrefabList[enemyNum];
            Debug.Log("Enemy Prefab " + enemyPrefab);
            var playerInstance = Instantiate(playerPrefab, new Vector2(-5f, -1.75f), Quaternion.identity);
            var enemyInstance = Instantiate(enemyPrefab, new Vector2(5f, -1.75f), Quaternion.identity);
            playerInstance.tag = "Player";
            enemyInstance.tag = "Enemy";
            if (isAi)
            {
                enemyInstance.transform.GetChild(7).gameObject.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy == false && isVs == true)
        {
            backImage.sprite = charBackList[playerChoice];
            vsImage.sprite = charSpriteList[playerChoice];
            vsText.text = charTempNames[playerChoice];
        }
        else if (isEnemy == true && isVs == true)
        {
            backImage.sprite = charBackList[enemyNum];
            vsImage.sprite = charSpriteList[enemyNum];
            vsText.text = charTempNames[enemyNum];
        }
    }

    public SpriteRenderer[] getPlayerParts()
    {
        return playerBodyParts;
    }

    public SpriteRenderer[] getEnemyParts()
    {
        return enemyBodyParts;
    }
}
