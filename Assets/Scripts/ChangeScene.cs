using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void StartGame()
    {
        MatchManager.instance.SetPlayerList();
        MatchManager.instance.StartGame();
    }
}
