using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviour{
        
    public int[] playerWins = {0,0,0,0};
    [Range(min: 1f, max: 100f)]
    public int totalRounds  = 1;

    [HideInInspector]
    public int playerWinner;
    int playerWinnerRival;
    int currentRound = 0;

    string mapName;

    bool isDraw = false;
    bool sceneLimiter = false;
    bool pointLimiter = false;

    // Esta funcion sirve para detectar que escena fue cargada
    void LoadScene(){
        SceneManager.sceneLoaded += OnSceneLoaded;

        sceneLimiter = false;
        pointLimiter = false;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){ 
    
        if (((scene.name == "SumoArena_Offline") || (scene.name == "Hills_Offline")) && !sceneLimiter){   // Se agrego un limitador, ya que por alguna razon desconocida la funcion se ejecutaba 2 veces

            sceneLimiter = true;

            currentRound += 1;

            if (!isDraw){
                PlayerSpawner.instance.GetSpawns(false);
            } else {
                PlayerSpawner.instance.GetSpawns(true);
            }

        } else if (scene.name == "Menu"){
            SetTotalRounds(1);

            currentRound = 0;

            playerWins = new int[] { 0, 0, 0, 0 }; 
        } 
    }

    public void SetTotalRounds(int value){      // Funcion seteada en el menu

        totalRounds = value;
    }

    void GetTotalPlayers(){

        int totalPlayers = 0;

        for (int i = 0; i < PlayerSpawner.instance.playersActive.Length; i++){

            if (PlayerSpawner.instance.playersActive[i]){
                totalPlayers++;
            }
        }
    }

    void SelectMap(){

        int random = Random.Range(1, 3);

        switch (random){

            case 1:
                mapName = "SumoArena_Offline";
                break;

            case 2:
                mapName = "Hills_Offline";
                break;
        }

        SceneManager.LoadScene(mapName);

        LoadScene();

        MusicManager.instance.LoadScene();
    }

    public void StartGame(){     //Se ejecuta cuando el boton "Play!" fue seleccionado en el menu

        GetTotalPlayers();

        SelectMap();
    }

    public void AddPlayerWin(GameObject playerWinner){
        
        if (!pointLimiter){

            pointLimiter = true;

            switch (playerWinner.GetComponent<PlayerInput>().playerNum){

                case PlayerInput.PlayerNum.Player1:
                    playerWins[0] += 1;
                    break;
                case PlayerInput.PlayerNum.Player2:
                    playerWins[1] += 1;
                    break;
                case PlayerInput.PlayerNum.Player3:
                    playerWins[2] += 1;
                    break;
                case PlayerInput.PlayerNum.Player4:
                    playerWins[3] += 1;
                    break;
            }

            CheckGameRound();
        }
    }

    void CheckGameRound(){
        
        if(currentRound < totalRounds){
            SelectMap();
        } else{
            CheckWinner();
        }
    }

    void CheckWinner(){

        int maxPoints = 0;
        int rivalPoints = 0;

        for (int i = 0; i < playerWins.Length; i++){

            if (playerWins[i] > maxPoints){

                maxPoints = playerWins[i];

                playerWinner = i;

                PlayerSpawner.instance.DisableIsDrawPlayers();

                isDraw = false;

            } else if (playerWins[i] == maxPoints){

                rivalPoints = playerWins[i];

                playerWinnerRival = i;
                
                PlayerSpawner.instance.isDrawPlayers[playerWinnerRival] = true;

                isDraw = true;
            }     
        }

        if (isDraw){

            totalRounds += 1;

            PlayerSpawner.instance.isDrawPlayers[playerWinner] = true;

            SelectMap();
        } else {
            // MOSTRAR GANADOR
            print("Juego terminado");

            SceneManager.LoadScene("MatchLobby_Offline");


        }
    }

    public void RestartDrawMatch(){     // Se ejecuta cuando los jugadores caen al mismo tiempo de la plataforma
        
        totalRounds += 1;

        SelectMap();
    }
}
