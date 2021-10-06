using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawner : MonoBehaviourPun{

    public static PlayerSpawner instance;

    public GameObject[] playerPrefab;
    
    [SerializeField, HideInInspector] private GameObject[] _spawns;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        GetSpawns();
    }

    private void GetSpawns()
    {
        _spawns = GameObject.FindGameObjectsWithTag("Spawn Area");

        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        int playerIndex = 0;
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (Equals(PhotonNetwork.PlayerList[i], PhotonNetwork.LocalPlayer))
            {
                playerIndex = i;   
                break;
            }
        }

        if (MatchManager.instance.isDraw && !MatchManager.instance.isDrawPlayers[playerIndex])
            return;

        PhotonNetwork.Instantiate("Prefabs/Players/" + playerPrefab[playerIndex].name, _spawns[playerIndex].transform.position, _spawns[playerIndex].transform.rotation);
    }

    /*public void AddPlayer(int player)
    {
        playersActive[player - 1] = true;
    }*/
}
