using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviourPun
{
    public GameObject pauseMenu;

    public GameObject player1Area;
    public GameObject player2Area;
    public GameObject player3Area;
    public GameObject player4Area;

    public TextMeshProUGUI player1Wins;
    public TextMeshProUGUI player2Wins;
    public TextMeshProUGUI player3Wins;
    public TextMeshProUGUI player4Wins;

    public TextMeshProUGUI secInterval;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI timePlus;
    public TextMeshProUGUI isDraw;

    private void Start()
    {
        //bool[] activePlayers = PlayerSpawner.instance.playersActive;
        bool[] activePlayers = {false, false, false, false};
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            activePlayers[i] = true;
        }

        for (int i = 0; i < activePlayers.Length; i++){

            if(activePlayers[i]){
                
                if (i==0){
                    player1Area.SetActive(true);
                } else if (i==1){
                    player2Area.SetActive(true);
                } else if (i == 2){
                    player3Area.SetActive(true);
                } else if (i == 3){
                    player4Area.SetActive(true);
                }
            }
        }

        MatchManager match = FindObjectOfType<MatchManager>();

        player1Wins.text = match.playerWins[0].ToString();
        player2Wins.text = match.playerWins[1].ToString();
        player3Wins.text = match.playerWins[2].ToString();
        player4Wins.text = match.playerWins[3].ToString();
    }

    // Muestra el tiempo que falta para que termine la ronda
    public void ChangeTimer(float time)
    {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);

        timer.text = minutes.ToString() + ":" + seconds.ToString("00");

        if (time == 0){

            StartCoroutine(SetInactive(0.5f, timer.gameObject));
        }
    }

    // Muestra los 5 segundos al principio de cada ronda
    public void ChangeSecInterval(int time)
    {
        secInterval.text = time.ToString();

        if(time <= 0)
        {
            secInterval.text = "GO!";
            StartCoroutine(SetInactive(1f, secInterval.gameObject));
        }
    }

    private IEnumerator SetInactive(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);

        obj.SetActive(false);
    }

    // Funcion que muestra el +15 al lado del tiempo
    public IEnumerator AddTime()
    {
        timePlus.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        StartCoroutine(SetInactive(0f, timePlus.gameObject));
    }

    public void SetTimerInactive()
    {
        StartCoroutine(SetInactive(0f, timer.gameObject));
    }

    public void ShowDraw()
    {
        isDraw.gameObject.SetActive(true);
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}
