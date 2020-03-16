using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSet : MonoBehaviour
{

    public GameObject mapObj;

    private Map_Array mapInfoScript;
    private int mapChoice;
    private Material currentMapMaterial;
    private MeshRenderer backgroundRenderer;

    // Start is called before the first frame update
    void Start()
    {
        mapInfoScript = mapObj.GetComponent<Map_Array>();
        mapChoice = mapInfoScript.getMapPlayerChoice();
        currentMapMaterial = mapInfoScript.mapTextures[mapChoice];
        backgroundRenderer = this.GetComponent<MeshRenderer>();
        backgroundRenderer.material = currentMapMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
