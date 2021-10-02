using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private MatchManager _manager;
    private PlayerSpawner _spawner;
    
    private void Start()
    {
        _manager = FindObjectOfType<MatchManager>();
        _spawner = FindObjectOfType<PlayerSpawner>();
    }

    public void StartGame()
    {
        _spawner.SetPlayerList();
        _manager.StartGame();
    }
}
