using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons_Application : MonoBehaviour{
    
    public GameObject settingsArea;

    bool isActive = false;

    public void ExitGame(){
        Application.Quit();
    }

    public void TurnObject(){

        isActive = !isActive;

        settingsArea.SetActive(isActive);
    }

    public void PlayButtonSettings(){

        isActive = false;

        settingsArea.SetActive(false);
    }
}
