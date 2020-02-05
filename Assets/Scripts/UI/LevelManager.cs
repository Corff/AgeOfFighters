using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public float timer;
    public string setName;


    public void Update()
    {
        if (timer == 0)
        {
            LoadLevel(setName);
        }
        else if(timer < 0)
        {
            
        }
        else
        {
            timer = timer - Time.deltaTime;
        }
    }

    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }

    public void QuitReqest()
    {
        Debug.Log("I want to quit!");
        Application.Quit();
    }
}