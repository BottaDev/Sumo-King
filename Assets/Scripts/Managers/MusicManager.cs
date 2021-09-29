using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour{

    public static MusicManager instance = null;
    public AudioSource source;

    bool musicIsPlaying = false;

    void Awake(){

        if (instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Esta funcion sirve para detectar que escena fue cargada
    public void LoadScene(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        
        if ((scene.name == "Menu") || (scene.name == "MatchLobby_Offline")){
            StopMusic();
        } else{

            if (!musicIsPlaying)
                PlayMusic();
        }
    }

    void StopMusic(){

        source.Stop();

        musicIsPlaying = false;
    }

    void PlayMusic(){
        
        source.Play();

        musicIsPlaying = true;
    }
}
