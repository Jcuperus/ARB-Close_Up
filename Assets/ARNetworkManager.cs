using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ARNetworkManager : NetworkLobbyManager {
    NetworkWriter nw = new NetworkWriter();
    NetworkReader nr = new NetworkReader();
    private NetworkConnection conn;
    private bool isRecieving = true;
    public float fetchInterval = 0.1f;


    public override void OnClientConnect(NetworkConnection conn) {
        base.OnClientConnect(conn);
        Debug.Log("client connect: " + conn.address);
        this.conn = conn;

        conn.Send(0, new NetworkMessage("test123"));
        //StartCoroutine(serverMessage("test message"));
    }

    public override void OnLobbyStartServer() {
        StartCoroutine(fetchMessages());
        base.OnLobbyStartServer();
    }

    private IEnumerator fetchMessages() {
        while (isRecieving) {
            yield return new WaitForSeconds(fetchInterval);
            Debug.Log();
            
        }
        //return 
    }

    public override void OnLobbyClientEnter() {
        Debug.Log("Player joined");
    }
}
