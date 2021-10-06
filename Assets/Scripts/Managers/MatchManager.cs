using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchManager : MonoBehaviourPun
{
    public static MatchManager instance;    
    
    public int[] playerWins = {0,0,0,0};
    [Range(min: 1f, max: 100f)] public int totalRounds  = 1;
    public bool[] playersActive = { false, false, false, false};
    public bool[] isDrawPlayers = { false, false, false, false };
    public bool isDraw;
    
    [HideInInspector] public int playerWinner;
    
    [SerializeField, HideInInspector] private int _playerWinnerRival;
    [SerializeField, HideInInspector] private int _currentRound = 0;
    [SerializeField, HideInInspector] private string _mapName;
    [SerializeField, HideInInspector] private bool _sceneLimiter;
    [SerializeField, HideInInspector] private bool _pointLimiter;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }
    
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
                DisableIsDrawPlayers();

                isDraw = false;
            } 
            else if (playerWins[i] == maxPoints)
            {
                _playerWinnerRival = i;
                isDrawPlayers[_playerWinnerRival] = true;

                isDraw = true;
            }     
        }

        if (isDraw)
        {
            totalRounds += 1;
            isDrawPlayers[playerWinner] = true;

            SelectMap();
        }
        else 
        {
            SceneManager.LoadScene(0);
        }
    }
    
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

    public void RestartDrawMatch()
    {
        totalRounds += 1;

        SelectMap();
    }
}
