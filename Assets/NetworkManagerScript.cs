using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManagerScript : MonoBehaviour{
    NetworkClient myClient;

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
        NetworkServer.Listen(4444);
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
}
