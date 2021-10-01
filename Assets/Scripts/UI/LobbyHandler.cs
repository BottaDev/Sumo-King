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

    private ServerConnection _connection;
    
    private void Update()
    {
        var myList = _connection.GetPlayerList();

        for (int i = 0; i < playerList.Count; i++)
        {
            if(i >= myList.Length)
            {
                playerList[i].text = "";
                continue;
            }
            
            playerList[i].text = i == 0 ? myList[i].NickName + " (Master)" : myList[i].NickName;
        }
    }

    private void OnEnable()
    {
        if (_connection == null)
            _connection = FindObjectOfType<ServerConnection>();
        
        roomName.text = _connection.GetRoomName() + " Lobby";
    }
}
