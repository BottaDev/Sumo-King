using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyManager_Offline : MonoBehaviour{

    public GameObject[] playerPrefab;
    public Transform winnerSpawn;
    public Transform[] spawns;

    public TextMeshProUGUI weHaveText;
    public TextMeshProUGUI timeRemaning;
    public GameObject textArea;

    float timeLeft = 12;

    MatchManager matchManager;
    bool[] spawnUsed = new bool[] { false, false, false };

    void Start(){

        matchManager = GameObject.Find("MatchManager").GetComponent<MatchManager>();

        SpawnPlayers();
    }

    void Update(){

        timeLeft -= Time.deltaTime;

        timeRemaning.text = timeLeft.ToString("f0");

        if (timeLeft >= 10.1f && timeLeft <= 10.5f){
            weHaveText.gameObject.SetActive(false);
            textArea.gameObject.SetActive(true);
        }

        if(timeLeft <= 0){
            BackToMenu();
        }

    }

    void SpawnPlayers(){

        spawnUsed = new bool[] { false, false, false };

        GameObject winner = Instantiate(playerPrefab[matchManager.playerWinner], winnerSpawn.position, winnerSpawn.rotation);
        GameObject crown = winner.GetComponent<PlayerState>().crown;
        winner.GetComponent<PlayerState>().pushForce = 14;
        crown.SetActive(true);
        winner.gameObject.GetComponentInChildren<CountOfConsumables>().enabled = false;

        for (int i = 0; i < PlayerSpawner.instance.playersActive.Length; i++){
            
            if (PlayerSpawner.instance.playersActive[i]){
                
                if (i != matchManager.playerWinner){

                    bool breaker = false;

                    while (!breaker){

                        int random = Random.Range(0, 3);

                        if (!spawnUsed[random]){

                            spawnUsed[random] = true;

                            breaker = true;
                            
                            GameObject player = Instantiate(playerPrefab[i], spawns[random].transform.position, spawns[random].transform.rotation);
                            player.GetComponent<PlayerState>().pushForce = 14;
                            player.gameObject.GetComponentInChildren<CountOfConsumables>().enabled = false;
                        }
                    }
                } else {
                    continue;
                }
            }
        }
    }

    void BackToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
