using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class networkDebug : NetworkManager{

    public override void OnServerConnect(NetworkConnection conn) {
        Debug.Log("conn: " + conn.address);
    }
}
