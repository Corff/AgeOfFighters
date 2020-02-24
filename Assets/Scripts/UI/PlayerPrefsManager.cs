using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";

    public static void SetMasterVolume (float volume){
		if (volume > 0f && volume < 1f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);//Sets the master volume if it meets the range condition.
		} else {
			Debug.LogError ("Master volume out of range");//Debugs if out of range
		}
	}

	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);//Allows for the extraction of the current master volume value.
	}	
}
