using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MenuMusic : MonoBehaviour
{

    FMOD.Studio.EventInstance Music;

   void start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/TestMusic");
        Music.start();
    }

    private void Update()
    {
        
    }

}
