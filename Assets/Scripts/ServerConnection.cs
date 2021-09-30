using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    private string _joinName = String.Empty;
    private string _createName = String.Empty;

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
        if (_joinName == string.Empty || _joinName == "" || 
            _createName == string.Empty || _createName == "" )
            return;
        
        PhotonNetwork.JoinRoom(_joinName);
    }
    
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom()
    {
        if (_joinName == string.Empty || _joinName == "" || 
            _createName == string.Empty || _createName == "")
            return;
        
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;

        PhotonNetwork.CreateRoom(_createName, options, TypedLobby.Default);
    }

    public void InsertNewRoomName(string serverName)
    {
        _createName = serverName;
    }

    public void InsertJoinRoomName(string serverName)
    {
        _joinName = serverName;
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
        //PhotonNetwork.LoadLevel(1);
    }

    public override void OnLeftLobby()
    {
        Debug.Log("Player exited lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Player disconnected");
    }
}
