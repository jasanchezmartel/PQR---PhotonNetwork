using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour {
    private void Start() {
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Instantiate("Frog", new Vector3(-2, -2.2f, 0), Quaternion.identity);
        } else {
            PhotonNetwork.Instantiate("VirtualGuy", new Vector3(-1, -2.2f, 0), Quaternion.identity);
        }
    }
}