using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAudio : MonoBehaviour{

    public AudioSource audioSource;
    public AudioClip uiClick_1;     
    public AudioClip uiClick_2;

    public void Click_1(){

        audioSource.clip = uiClick_1;
        audioSource.Play();
    }

    public void Click_2(){

        audioSource.clip = uiClick_2;
        audioSource.Play();
    }
}
