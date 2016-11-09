using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkManagerScript : MonoBehaviour{
    NetworkClient myClient;
    private string nickname;
    private bool isHost = false;
    private List<string> serverPlayerList = new List<string>();

    void Start(){
        DontDestroyOnLoad(this.gameObject);

        Button hostButton = GameObject.Find("HostButton").GetComponent<Button>();
        hostButton.onClick.AddListener(handleHost);

        Button joinButton = GameObject.Find("JoinButton").GetComponent<Button>();
        joinButton.onClick.AddListener(handleJoin);
    }

    private void handleHost(){
        SetupServer();
        SetupLocalClient();
    }

    private void handleJoin(){
        SetupClient();
    }

    void Update(){
        
    }

    // Create a server and listen on a port
    public void SetupServer(){
        isHost = true;
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnected);
    }

    // Create a client and connect to the server port
    public void SetupClient(){
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        string ipInput = GameObject.Find("IPInputField/Text").GetComponent<Text>().text;
        myClient.Connect(ipInput, 4444);
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient(){
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
    }

    // client function
    public void OnConnected(UnityEngine.Networking.NetworkMessage netMsg){
        Debug.Log("Connected to server");
        SceneManager.LoadScene("lobbyScene");
    }

    // server function
    public void OnServerConnected(UnityEngine.Networking.NetworkMessage netMsg){
        Debug.Log("server: client connected");
        StartCoroutine(updatePlayerList());
    }

    private IEnumerator updatePlayerList(){
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("PlayerListText").GetComponent<Text>().text = "";
        foreach(NetworkConnection item in NetworkServer.connections) {
            serverPlayerList.Add(item.address);
            GameObject.Find("PlayerListText").GetComponent<Text>().text += "\n" + item.address;
        }
    }
}
