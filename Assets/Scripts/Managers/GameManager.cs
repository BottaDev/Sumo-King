using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public static bool gameIsPaused = false;

    public float matchTime = 120;       // Tiempo de juego de la partida (segundos)
    [Range(min: 0, max: 10)]
    public float secInterval = 5;       // Tiempo para que empieze la partida
    public RandomSpawner spawner;
    public UIManager uiManager;

    public GameObject[] totalPlayers;
    int deathCount = 0;                 // Cantidad de players que murieron
    GameObject playerWinner = null;     // Player que gano
    CameraFollow cam;

    bool isGameOver = false;
    bool limiter = false;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

        totalPlayers = GameObject.FindGameObjectsWithTag("Player");

        cam = Camera.main.GetComponent<CameraFollow>();

        ChangeSpeed(0f, 0f);
    }
    void Update(){
        
        if (!limiter)
            IntervalSecuence();

        if (!isGameOver && limiter){

            MatchInProgress();

            if(Input.GetKeyDown(KeyCode.Escape)){
                
                if(gameIsPaused){
                    ResumeGame();
                }else{
                    PauseGame();
                }
            }
        }
    }

    void MatchInProgress(){

        bool matchChecker = TimeIsRunning();      // Si devuelve true, la ronda continua

        if (matchChecker){

            if (matchTime >= 45 && matchTime <= 45.99f)
                spawner.ChangeSpawnRate();

            if (deathCount == totalPlayers.Length){

                StartCoroutine(EndMatch(false));

            } else if (deathCount == (totalPlayers.Length - 1)){

                StartCoroutine("PickWinnerByPush");
            }

        } else{
            PickWinnerByTime();
        }
    }

    void IntervalSecuence() {

        secInterval -= Time.deltaTime;

        if (secInterval < 0){
            secInterval = 0;
        }

        if (secInterval <= 0) {

            limiter = true;

            spawner.StartCoroutine("StartDelay", secInterval);

            ChangeSpeed(5f, 250f);
        } else {

            uiManager.ChangeSecInterval((int) secInterval);
        }
    }

    bool TimeIsRunning() {

        matchTime -= Time.deltaTime;

        if (matchTime >= 0) {

            uiManager.ChangeTimer(matchTime);

            return true;
        } else {
            return false;
        }
    }

    void ChangeSpeed(float speed, float turnSpeed) {

        foreach (GameObject player in totalPlayers) {

            PlayerState state = player.GetComponent<PlayerState>();

            state.speed = speed;
            state.turnSpeed = turnSpeed;
        }
    }

    void PickWinnerByTime() {

        int maxCount = 0;

        bool isDraw = false;

        totalPlayers = GameObject.FindGameObjectsWithTag("Player");             // Se buscan de nuevo los players ya que sino el array podria tener nulls

        foreach (GameObject player in  totalPlayers) {

            PlayerState state = player.GetComponent<PlayerState>();

            if (state.consumablesCount > maxCount) {

                isDraw = false;

                maxCount = state.consumablesCount;

                playerWinner = player;

            } else if (state.consumablesCount == maxCount){
                isDraw = true;
            }
        }

        if (!isDraw){
            StartCoroutine(EndMatch(true));
        } else {

            matchTime = 15;

            uiManager.StartCoroutine("AddTime");
        }
    }

    IEnumerator PickWinnerByPush(){

        yield return new WaitForSeconds(2.5f);

        for (int i = 0; i < totalPlayers.Length; i++){
            
            if (totalPlayers[i] != null){

                playerWinner = totalPlayers[i];
            }
        }
        StartCoroutine(EndMatch(true));
    }

    IEnumerator EndMatch(bool winner){

        uiManager.ChangeTimer(0);

        isGameOver = true;

        spawner.EndMatch();

        uiManager.SetTimerInactive();

        MatchManager match = GameObject.Find("MatchManager").GetComponent<MatchManager>();

        if (winner){

            cam.SelectTarget(playerWinner);

            yield return new WaitForSeconds(4f);

            match.AddPlayerWin(playerWinner);

        } else{
            uiManager.ShowDraw();

            yield return new WaitForSeconds(1.5f);

            match.RestartDrawMatch();
        }
    }

    public void ResumeGame(){

        uiManager.HidePauseMenu();

        Time.timeScale = 1;

        gameIsPaused = false;
    }

    void PauseGame(){

        uiManager.ShowPauseMenu();

        Time.timeScale = 0;

        gameIsPaused = true;
    }

    public void BackToMenu(){

        ResumeGame();

        SceneManager.LoadScene("Menu");
    }

    public void AddDeathPlayer(){
        
        deathCount++; 
    }
}