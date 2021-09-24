using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour{

    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;

    Resolution[] resolutions;

    void Start(){

        GetResolutions();

        GetFullScreenMode();

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");

        int savedWidth = PlayerPrefs.GetInt("ScreenWidth");
        int savedHeight = PlayerPrefs.GetInt("ScreenHeight");

        Screen.SetResolution(savedWidth, savedHeight, Screen.fullScreen);
    }

    void GetFullScreenMode(){

        int mode = PlayerPrefs.GetInt("FullScreen");
        
        if (mode == 1){         // FullScreen activado

            Screen.fullScreen = true;
            fullScreenToggle.isOn = true;

        } else if(mode == 0){     // FullScreen desactivado

            Screen.fullScreen = false;
            fullScreenToggle.isOn = false;
        }
    }

    void GetResolutions(){

        resolutions = Screen.resolutions;       // Todas las resoluciones soportadas por el monitor (fulllscreen)

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++){

            string option = resolutions[i].width + " x " + resolutions[i].height;

            options.Add(option);

            if ((resolutions[i].width == Screen.currentResolution.width) && (resolutions[i].height == Screen.currentResolution.height)){

                currentResolutionIndex = i;
            }

            Debug.Log(resolutions[i]);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void SaveOptions(){
        PlayerPrefs.Save();
    }

    public void SetResolution(int resolutionIndex){     // Se ejecuta al seleccionar una resolucion

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("ScreenWidth", resolution.width);
        PlayerPrefs.SetInt("ScreenHeight", resolution.height);
        
        SaveOptions();
    }

    public void SetVolume(float volume){        // Se ejecuta al modificar la barra de volumen

        audioMixer.SetFloat("Volume", volume);

        PlayerPrefs.SetFloat("Volume", volume);
        
        SaveOptions();
    }

    public void SetFullscreen(bool isFullScreen){       // Se ejecuta al apretar el boton de Fullscreen
        
        Screen.fullScreen = isFullScreen;
        
        if (isFullScreen){
            PlayerPrefs.SetInt("FullScreen", 1);
        } else{
            PlayerPrefs.SetInt("FullScreen", 0);
        }

        SaveOptions();
    }
}
