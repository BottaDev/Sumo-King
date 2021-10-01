using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private MatchManager _manager;
    
    private void Start()
    {
        _manager = FindObjectOfType<MatchManager>();
    }

    public void StartGame()
    {
        _manager.StartGame();
    }
}
