using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsControl : MonoBehaviour {

    public Slider volumeSlider;
    public LevelManager levelManager;

    private MusicPlayer musicManager;

	// Use this for initialization
	void Start () {
        musicManager = GameObject.FindObjectOfType<MusicPlayer>();//finds object in the hierachy of specified type.
        volumeSlider.value = PlayerPrefsManager.GetMasterVolume();//Sets slider value to player prefs volume.
    }
	
	// Update is called once per frame
	void Update () {
        musicManager.ChangeVolume(volumeSlider.value);//Changes the volume of music.

	}

    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);//Saves the volume and transitions to start screen.
        levelManager.LoadLevel("Main Menu");//Level to be loaded.
    }
}
