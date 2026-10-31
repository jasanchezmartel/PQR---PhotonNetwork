using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Conection : MonoBehaviourPunCallbacks {
    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster() {
        print("conectado al master");
    }

    public void ButtonConnect() {
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom() {
        Debug.Log("conectada a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("hay " + PhotonNetwork.CurrentRoom.PlayerCount + " jugadores conectados");
    }

    private void Update() {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount > 1) {
            PhotonNetwork.LoadLevel(1);
            Destroy(this);
        }
    }
}