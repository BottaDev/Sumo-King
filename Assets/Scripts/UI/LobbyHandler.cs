using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class LobbyHandler : MonoBehaviourPun
{
    public TextMeshProUGUI roomName;
    public List<TextMeshProUGUI> playerList;
    
    private void Update()
    {
        var myList = PhotonNetwork.PlayerList;

        for (int i = 0; i < playerList.Count; i++)
        {
            if(i >= myList.Length)
            {
                playerList[i].text = "";
                continue;
            }
            playerList[i].text = myList[i].NickName;
        }
    }

    private void OnEnable()
    {
        if (PhotonNetwork.CurrentRoom.Name == null)
            return;
        
        roomName.text = PhotonNetwork.CurrentRoom.Name + " Lobby";
    }
}
