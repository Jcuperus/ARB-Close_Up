using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class networkDebug : NetworkLobbyManager {

    public override void OnServerConnect(NetworkConnection conn) {
        Debug.Log("conn: " + conn.address);
    }

    public override void OnLobbyClientEnter() {
        
    }
}
