using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviourPun
{
    public int[] playerWins = {0,0,0,0};
    [Range(min: 1f, max: 100f)] public int totalRounds  = 1;
    [HideInInspector] public int playerWinner;
    
    [SerializeField] private int _playerWinnerRival;
    [SerializeField] private int _currentRound = 0;
    [SerializeField] private string _mapName;
    [SerializeField] private bool _isDraw;
    [SerializeField] private bool _sceneLimiter;
    [SerializeField] private bool _pointLimiter;
    
    private void LoadScene()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        _sceneLimiter = false;
        _pointLimiter = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se agrego un limitador, ya que por alguna razon desconocida la funcion se ejecutaba 2 veces
        if (((scene.name == "SumoArena_Offline") || (scene.name == "Hills_Offline")) && !_sceneLimiter){

            _sceneLimiter = true;

            _currentRound += 1;

            PlayerSpawner.instance.GetSpawns(_isDraw);
        } 
        else if (scene.name == "Menu")
        {
            SetTotalRounds(1);

            _currentRound = 0;

            playerWins = new int[] { 0, 0, 0, 0 }; 
        } 
    }

    public void SetTotalRounds(int value)
    {
        totalRounds = value;
    }

    private int GetTotalPlayers()
    {
        /*for (int i = 0; i < PlayerSpawner.instance.playersActive.Length; i++)
        {
            if (PlayerSpawner.instance.playersActive[i])
                totalPlayers++;
        }*/
        var totalPlayers = PhotonNetwork.PlayerList.Length;
        return totalPlayers;
    }

    private void SelectMap()
    {
        int random = Random.Range(1, 3);

        switch (random)
        {
            case 1:
                _mapName = "SumoArena_Offline";
                break;

            case 2:
                _mapName = "Hills_Offline";
                break;
        }

        //SceneManager.LoadScene(_mapName);
        PhotonNetwork.LoadLevel(_mapName);
        LoadScene();
        MusicManager.instance.LoadScene();
    }

    public void StartGame()
    {
        if (GetTotalPlayers() >= 2)
            SelectMap();
    }

    public void AddPlayerWin(GameObject playerWinner)
    {
        if (!_pointLimiter)
        {
            _pointLimiter = true;

            switch (playerWinner.GetComponent<PlayerInput>().playerNum)
            {
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

    private void CheckGameRound()
    {
        if(_currentRound < totalRounds)
            SelectMap();
        else
            CheckWinner();
    }

    private void CheckWinner()
    {
        int maxPoints = 0;

        for (int i = 0; i < playerWins.Length; i++)
        {
            if (playerWins[i] > maxPoints)
            {
                maxPoints = playerWins[i];
                playerWinner = i;
                PlayerSpawner.instance.DisableIsDrawPlayers();

                _isDraw = false;
            } 
            else if (playerWins[i] == maxPoints)
            {
                _playerWinnerRival = i;
                PlayerSpawner.instance.isDrawPlayers[_playerWinnerRival] = true;

                _isDraw = true;
            }     
        }

        if (_isDraw)
        {
            totalRounds += 1;
            PlayerSpawner.instance.isDrawPlayers[playerWinner] = true;

            SelectMap();
        }
        else 
        {
            SceneManager.LoadScene(0);
        }
    }

    public void RestartDrawMatch()
    {
        totalRounds += 1;

        SelectMap();
    }
}
