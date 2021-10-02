using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPun{

    public static PlayerSpawner instance;

    public GameObject[] playerPrefab;
    public bool[] playersActive = { false, false, false, false};
    
    [HideInInspector] public bool[] isDrawPlayers = { false, false, false, false };
    [SerializeField, HideInInspector] private GameObject[] _spawns;
    [SerializeField, HideInInspector] private bool[] _spawnUsed;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    public void GetSpawns(bool isDraw)
    {
        _spawns = GameObject.FindGameObjectsWithTag("Spawn Area");

        _spawnUsed = new bool[] { false, false, false, false };

        SpawnPlayers(!isDraw ? playersActive : isDrawPlayers);
    }

    private void SpawnPlayers(bool[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i])
            {
                bool breaker = false;

                while (!breaker)
                {
                    int random = Random.Range(0, 4);

                    if (!_spawnUsed[random])
                    {
                        _spawnUsed[random] = true;

                        breaker = true;

                        PhotonNetwork.Instantiate("Prefabs/Players/" + playerPrefab[i].name, _spawns[random].transform.position, _spawns[random].transform.rotation);
                    }
                }
            }
        }
    }

    /*public void AddPlayer(int player)
    {
        playersActive[player - 1] = true;
    }*/

    public void DisableAllPlayers()
    {
        for (int i = 0; i < playersActive.Length; i++)
        {
            playersActive[i] = false;
        }
    }

    public void DisableIsDrawPlayers()
    {
        for (int i = 0; i < isDrawPlayers.Length; i++)
        {
            isDrawPlayers[i] = false;
        }
    }

    public void SetPlayerList()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playersActive[i] = true;
        }
    }
}
