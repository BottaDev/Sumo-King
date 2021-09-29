using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ServerConnection : MonoBehaviourPunCallbacks
{
    string joinName;
    string createName;

    [SerializeField] GameObject principalUI;
    [SerializeField] GameObject joinRoomUI;

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

        principalUI.SetActive(false);
        joinRoomUI.SetActive(true);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinName);
    }
    
    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = true;

        PhotonNetwork.CreateRoom(createName, options, TypedLobby.Default);
    }

    public void InsertNewRoomName(string serverName)
    {
        createName = serverName;
    }

    public void InsertJoinRoomName(string serverName)
    {
        joinName = serverName;
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
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {

    }

    public override void OnLeftLobby()
    {
        Debug.Log("Player exited lobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        principalUI.SetActive(true);
        joinRoomUI.SetActive(false);
        Debug.Log("Player disconnected");
    }
}
