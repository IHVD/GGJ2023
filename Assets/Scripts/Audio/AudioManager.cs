using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance 
    { 
        get; private set;
    }

    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;
    FMOD.Studio.Bus Master;

    float MusicVolume = 0.5f;
    float SFXVolume = 0.5f;
    float MasterVolume = 1f;

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("Bus:/Master/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("Bus:/Master/SFX");
        Master = FMODUnity.RuntimeManager.GetBus("Bus:/Master");
        

        if (instance != null)
        {
            Debug.LogError("Foundmore audio managers");
        }
        instance = this;


    }

    public void PlayOneShot(EventReference sound, Vector3 worldpos)
    {
        RuntimeManager.PlayOneShot(sound, worldpos);
    }


    void update()
    {
        Music.setVolume (MusicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(MasterVolume);

    }


    public void MasterVolumeLevel(float newMasterVolume)
    {
        MasterVolume = newMasterVolume;
    }

    public void MusicVolumeLevel(float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel(float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }

    

}
