using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip[] clip;
    public GAME_STATE game_state;

    // clip[0] = NONE
    // clip[1] = DEFAULT1
    // clip[2] = DEFAULT2
    // clip[3] = COMBAT
    // clip[4] = ORASYON
    // clip[5] = BOSS
    private bool isBossFight;
    public static AudioHandler instance;
    void Start()
    {
        instance = this;
        StateMusic();
    }

    public void SetBossFight(bool isBossFight)
    {
        this.isBossFight = isBossFight;
        StateMusic();
    }
    public void StateMusic()
    {
        if(SinagScript.instance.spawnIndex <= 3)
        {
            ChangeMusic(1);
        }
        else if (SinagScript.instance.spawnIndex >= 4)
        {
            if(isBossFight)
            {
                ChangeMusic(5);
            }
            else
            {
                ChangeMusic(2);
            }           
        }
    }

    public void ChangeMusic(int state)
    {
        GAME_STATE newState = (GAME_STATE)state;

        game_state = newState;

        // Stop any currently playing music
        audioSource.Stop();
        audioSource.mute = false;
        // Set the appropriate clip based on the game state
        switch (game_state)
        {
            case GAME_STATE.DEFAULT1:
                audioSource.clip = clip[1];
                break;
            case GAME_STATE.DEFAULT2:
                audioSource.clip = clip[2];
                break;
            case GAME_STATE.COMBAT:
                audioSource.clip = clip[3];
                break;
            case GAME_STATE.ORASYON:
                audioSource.clip = clip[4];
                break;
            case GAME_STATE.BOSS:
                audioSource.clip = clip[5];
                break;
            case GAME_STATE.MUTE:
                audioSource.mute = true;
                break;
            default:
                // For any other state, use clip[0] (NONE) or handle as needed
                audioSource.clip = clip[0];
                break;
        }

        // Play the new clip
        audioSource.Play();
    }
}

public enum GAME_STATE
{
    MUTE,
    DEFAULT1,
    DEFAULT2,
    COMBAT,
    ORASYON,
    BOSS
}
