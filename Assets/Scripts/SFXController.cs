using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{

    public AudioClip[] characterSFX;//Sfx array of all character related sounds, starts at index 7
    //1.Walk 2.Run 3.LPunch 4.HPunch 5.Special 6.Jump 7.Crouch


    private characterInfoAssign charInfoAccess;//CharInfo Access
    private Animator playerAnim;//Player Animator
    private Animator enemyAnim;//Enemy Animator
    private AudioSource playerAudio;
    private AudioSource enemyAudio;
    private int playerCharPrefNum;//Prefab choice
    private int enemyCharPrefNum;//Prefab choice
    private bool sfxIsPlaying = false;//Set sound to playing once
    private bool isConstantSFX = false;
    private bool hasDeAnim = false;
    private int randSfxNum;//Randomise SFX
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GameObject.FindWithTag("Player").GetComponent<Animator>();//Grabs Player Animator
        enemyAnim = GameObject.FindWithTag("Enemy").GetComponent<Animator>();//Grabs Enemy Animator
        playerAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();//Grabs Player AudioSource
        enemyAudio = GameObject.FindWithTag("Enemy").GetComponent<AudioSource>();//Grabs Enemy AudioSource
    }

    // Update is called once per frame
    void Update()
    {
        //charSFX(playerAnim, playerAudio);
        //charSFX(enemyAnim, enemyAudio);
    }

    public void soundCall(GameObject Gm, string typeAnim)
    {
        if(Gm.tag == "Player")
        {
            charCallSFX(playerAnim, playerAudio, typeAnim);
        }
        else if (Gm.tag == "Enemy")
        {
            charCallSFX(enemyAnim, enemyAudio, typeAnim);
        }
    }

    void charCallSFX(Animator currentAnimator, AudioSource currentAudioS, string animTypeC)
    {
        
        if ((animTypeC == "Walk" || animTypeC == "BackwardWalk"))
        {
            if (currentAudioS.isPlaying == false)
            {
                currentAudioS.clip = characterSFX[1 * 16];
                currentAudioS.Play();
            }
            isConstantSFX = true;
        }
        else if (animTypeC == "Run")
        {
            currentAudioS.clip = characterSFX[(1 * 16) + 1];
            currentAudioS.Play();
            Debug.Log("Enemy Punch Call");
        }
        else if (animTypeC == "Punch")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 1 + randSfxNum];
            currentAudioS.Play();

        }
        else if (animTypeC == "HPunch")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 3 + randSfxNum];
            currentAudioS.Play();
        }
        else if (animTypeC == "Ranged")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 5 + randSfxNum];
            currentAudioS.Play();
        }
        else if (animTypeC == "Special")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 7 + randSfxNum];
            currentAudioS.Play();
        }
        else if (animTypeC == "Jump")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 9 + randSfxNum];
            currentAudioS.Play();
        }
        else if (animTypeC == "Crouch")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 11 + randSfxNum];
            currentAudioS.Play();
        }
        else if (animTypeC == "Block")
        {
            randSfxNum = Random.Range(1, 3);
            currentAudioS.clip = characterSFX[(1 * 16) + 13 + randSfxNum];
            currentAudioS.Play();
        }
    }

}
