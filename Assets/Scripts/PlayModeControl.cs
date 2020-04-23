using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModeControl : MonoBehaviour
{

    public static bool isMultiplayer = false;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayTypeMultiplayer(bool inType)
    {
        isMultiplayer = inType;
    }
}
