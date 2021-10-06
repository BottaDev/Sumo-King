using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuInput_Offline : MonoBehaviour {

    public GameObject playButton;
    public TextMeshProUGUI joinText1;
    public TextMeshProUGUI joinText2;
    public TextMeshProUGUI joinText3;
    public TextMeshProUGUI joinText4;

    public GameObject player1Model;
    public GameObject player2Model;
    public GameObject player3Model;
    public GameObject player4Model;

    #region INPUT AND VISUALS
    string horizontalAxisP1;
    string verticalAxisP1;

    string horizontalAxisP2;
    string verticalAxisP2;

    string horizontalAxisP3;
    string verticalAxisP3;

    string horizontalAxisP4;
    string verticalAxisP4;


    float horizontalInputP1;
    float verticalInputP1;

    float horizontalInputP2;
    float verticalInputP2;

    float horizontalInputP3;
    float verticalInputP3;

    float horizontalInputP4;
    float verticalInputP4;

    bool player1IsEnabled = false;
    bool player2IsEnabled = false;
    bool player3IsEnabled = false;
    bool player4IsEnabled = false;
    #endregion

    bool inputLimiter1 = false;
    bool inputLimiter2 = false;
    bool inputLimiter3 = false;
    bool inputLimiter4 = false;
    int activePlayers = 0;

    void OnEnable() {

        activePlayers = 0;

        inputLimiter1 = false;
        inputLimiter2 = false;
        inputLimiter3 = false;
        inputLimiter4 = false;

        player1IsEnabled = false;
        player2IsEnabled = false;
        player3IsEnabled = false;
        player4IsEnabled = false;

        SetObject(joinText1.gameObject, true);
        SetObject(joinText2.gameObject, true);
        SetObject(joinText3.gameObject, true);
        SetObject(joinText4.gameObject, true);

        SetObject(playButton, false);

        SetObject(player1Model, false);
        SetObject(player2Model, false);
        SetObject(player3Model, false);
        SetObject(player4Model, false);

        MatchManager.instance.DisableAllPlayers();
    }

    void Start() {

        verticalAxisP1 = "Vertical";
        horizontalAxisP1 = "Horizontal";

        verticalAxisP2 = "Vertical P2";
        horizontalAxisP2 = "Horizontal P2";

        verticalAxisP3 = "Vertical P3";
        horizontalAxisP3 = "Horizontal P3";

        verticalAxisP4 = "Vertical P4";
        horizontalAxisP4 = "Horizontal P4";
    }

    /*void Update() {

        horizontalInputP1 = Input.GetAxis(horizontalAxisP1);
        verticalInputP1 = Input.GetAxis(verticalAxisP1);

        horizontalInputP2 = Input.GetAxis(horizontalAxisP2);
        verticalInputP2 = Input.GetAxis(verticalAxisP2);

        horizontalInputP3 = Input.GetAxis(horizontalAxisP3);
        verticalInputP3 = Input.GetAxis(verticalAxisP3);

        horizontalInputP4 = Input.GetAxis(horizontalAxisP4);
        verticalInputP4 = Input.GetAxis(verticalAxisP4);


        if (((horizontalInputP1 != 0) || (verticalInputP1 != 0)) && !inputLimiter1){

            inputLimiter1 = true;

            EnablePlayer(player1IsEnabled, joinText1.gameObject, player1Model, 1);
        }
            
        if (((horizontalInputP2 != 0) || (verticalInputP2 != 0)) && !inputLimiter2){
            
            inputLimiter2 = true;

            EnablePlayer(player2IsEnabled, joinText2.gameObject, player2Model, 2);
        }
         
        if (((horizontalInputP3 != 0) || (verticalInputP3 != 0)) && !inputLimiter3){
            
            inputLimiter3 = true;

            EnablePlayer(player3IsEnabled, joinText3.gameObject, player3Model, 3);
        }

        if (((horizontalInputP4 != 0) || (verticalInputP4 != 0)) && !inputLimiter4){
            
            inputLimiter4 = true;

            EnablePlayer(player4IsEnabled, joinText4.gameObject, player4Model, 4);
        }
    }

    void EnablePlayer(bool player, GameObject joinText, GameObject playerModel, int playerNumber){

        activePlayers++;

        player = true;

        SetObject(joinText, false);
        SetObject(playerModel, true);

        PlayerSpawner.instance.AddPlayer(playerNumber);

        if (activePlayers >= 2)
            SetObject(playButton, true);
    }*/

    private void SetObject(GameObject obj, bool mode)
    {
        obj.SetActive(mode);
    }
}
