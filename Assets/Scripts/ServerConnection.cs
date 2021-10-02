using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    private string _roomName = String.Empty;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            DisconnectToServer();
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void DisconnectToServer()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player connected to master");

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void JoinRoom()
    {
        if (_roomName == string.Empty || _roomName == "")
            return;
        
        PhotonNetwork.JoinRoom(_roomName);
    }
    
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom()
    {
        if (_roomName == string.Empty || _roomName == "")
            return;
        
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;

        PhotonNetwork.CreateRoom(_roomName, options, TypedLobby.Default);
    }

    public void ChangeRoomName(string serverName)
    {
        _roomName = serverName;
    }

    public void ChangeNickName(string nickName)
    {
        PhotonNetwork.LocalPlayer.NickName = nickName;
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Player joined lobby");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player joined room");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Player exited lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Player disconnected");
        PhotonNetwork.LeaveRoom();
    }

    public string GetRoomName()
    {
        return _roomName;
    }

    public Player[] GetPlayerList()
    {
        return PhotonNetwork.PlayerList;
    }
}
