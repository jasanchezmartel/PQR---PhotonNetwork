using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Connection : MonoBehaviourPunCallbacks {
    private void Start() {
        Photon.Pun.PhotonNetwork.ConnectUsingSettings();
    }

    override
        public void OnConnectedToMaster() {
        print("Conectado al master");
    }
    private void OnConnectedToServer() {
        print("Conectado al servidor");
    }

    public void ButtonConnect() {
        RoomOptions options = new RoomOptions() { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default);
    }

    override
        public void OnJoinedRoom() {
        print("Conectada a la sala" + PhotonNetwork.CurrentRoom.Name);
    }
}
