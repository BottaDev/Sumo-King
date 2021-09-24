using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour{

    public static PlayerSpawner instance;

    public GameObject[] playerPrefab;
    public bool[] playersActive = { false, false, false, false};
    [HideInInspector]
    public bool[] isDrawPlayers = { false, false, false, false };

    GameObject[] spawns;
    bool[] spawnUsed;

    void Awake(){

        if (instance == null){
            instance = this;
        }else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void GetSpawns(bool isDraw){

        spawns = GameObject.FindGameObjectsWithTag("Spawn Area");

        spawnUsed = new bool[] { false, false, false, false };

        if (!isDraw){
            SpawnPlayers(playersActive);
        } else{
            SpawnPlayers(isDrawPlayers);
            //SpawnDrawPlayers();
        }
    }

    void SpawnPlayers(bool[] array){

        for (int i = 0; i < array.Length; i++){

            if (array[i]){
                
                bool breaker = false;

                while (!breaker){

                    int random = Random.Range(0, 4);

                    if (!spawnUsed[random]){

                        spawnUsed[random] = true;

                        breaker = true;

                        Instantiate(playerPrefab[i], spawns[random].transform.position, spawns[random].transform.rotation);
                    }
                }
            }
        }
    }
    /*
    void SpawnDrawPlayers(){                // Funcion que se ejecuta cuando hay empate 

        for (int i = 0; i < isDrawPlayers.Length; i++){

            if (isDrawPlayers[i]){

                bool breaker = false;

                while (!breaker){

                    int random = Random.Range(0, 4);

                    if (!spawnUsed[random]){

                        spawnUsed[random] = true;

                        breaker = true;

                        Instantiate(playerPrefab[i], spawns[random].transform.position, spawns[random].transform.rotation);
                    }
                }
            }
        }
    }*/

    public void AddPlayer(int player){

        playersActive[player - 1] = true;
    }

    public void DisableAllPlayers(){

        for (int i = 0; i < playersActive.Length; i++){

            playersActive[i] = false;
        }
    }

    public void DisableIsDrawPlayers(){

        for (int i = 0; i < isDrawPlayers.Length; i++){

            isDrawPlayers[i] = false;
        }
    }
}
