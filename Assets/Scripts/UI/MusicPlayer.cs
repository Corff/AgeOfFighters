using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

    public AudioClip[] levelMusicChangeArray;

    private Scene currentScene;
    private AudioSource audioSource;

    // Use this for initialization
    void Awake () {
		if (instance != null) {
			DestroyImmediate(gameObject);
			print ("Duplicate music player self-destructing");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad (gameObject);
		}
    }

    private void Update()
    {
        audioSource = GetComponent<AudioSource>();
       
    }

    void OnLevelWasLoaded(int level)
    {
        if (levelMusicChangeArray[level] != null)
        {
            AudioClip thisLevelMusic = levelMusicChangeArray[level];//Sets the music track based upon the level.

            if (thisLevelMusic)
            {//If there is music attached.
                audioSource.mute = false;
                audioSource.clip = thisLevelMusic;//This sets the current selected audio track to the clip.
                audioSource.loop = true;//While the if statement is true the audio track will loop/repeat.
                audioSource.Play();//This line plays the current audio track under the variable name audioSource
            }
        }
        else if(levelMusicChangeArray[level] == null){
            audioSource.mute = true;
        }
    }

    public void ChangeVolume(float volume)//Takes volume parameter
    {
        audioSource.volume = volume;//changes audiosource volume relative to volume value
    }
}
