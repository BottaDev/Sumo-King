using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour{

    public string scene = "Tutorial";

    public void FindMatchManager(){

        MatchManager manager = GameObject.Find("MatchManager").GetComponent<MatchManager>();

        manager.StartGame();
    }

    public void SceneChange(){

        SceneManager.LoadScene(scene);
    }
}
