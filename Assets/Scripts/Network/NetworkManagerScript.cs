using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NetworkManagerScript : MonoBehaviour{
    NetworkClient myClient;
    private bool isHost = false;
    private List<string> serverPlayerList = new List<string>();
    private string nickname;
    public int objective;
    //public RoundBehaviour round;

    void Start(){
        DontDestroyOnLoad(this.gameObject);

        Button hostButton = GameObject.Find("HostButton").GetComponent<Button>();
        hostButton.onClick.AddListener(handleHost);

        Button joinButton = GameObject.Find("JoinButton").GetComponent<Button>();
        joinButton.onClick.AddListener(handleJoin);
    }

    public void handleHost(){
        SetupServer();
        SetupLocalClient();
    }

    public void handleJoin(){
        SetupClient();
    }

    public void handleStart() {
        int objective = 0; //random (range is het aantal npc's)
        NetworkMessageBase clientMessage = new NetworkMessageBase(JsonUtility.ToJson(new NetworkInstanceVars("start", objective)));
        NetworkServer.SendToAll(1337, clientMessage);
    }

    // Create a server and listen on a port
    public void SetupServer(){
        isHost = true;
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnected);
        NetworkServer.RegisterHandler(1337, OnClientMessage);
    }

    // Create a client and connect to the server port
    public void SetupClient(){
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(1337, OnServerMessage);
        string ipInput = GameObject.Find("IPInputField/Text").GetComponent<Text>().text;
        myClient.Connect(ipInput, 4444);
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient(){
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(1337, OnServerMessage);
    }

    // client function
    public void OnConnected(UnityEngine.Networking.NetworkMessage netMsg){
        Debug.Log("Connected to server");
        nickname = GameObject.Find("NameInputField/Text").GetComponent<Text>().text;
        SceneManager.LoadScene("lobbyScene");
    }

    //client function
    public void OnServerMessage(UnityEngine.Networking.NetworkMessage netMsg) {
        NetworkInstanceVars json = JsonUtility.FromJson<NetworkInstanceVars>(netMsg.reader.ReadString());

        switch(json.messageType) {
            case "player list":
                Debug.Log("Client Message type: player list");
                GameObject.Find("PlayerListText").GetComponent<Text>().text = "";
                foreach(string playahIP in json.playerList) {
                    GameObject.Find("PlayerListText").GetComponent<Text>().text += "\n" + playahIP;
                }
                break;
            case "start":
                Debug.Log("Client Message type: start");
                SceneManager.LoadScene("mainScene");
                objective = json.objective;
                break;
            default:
                Debug.Log("unknown server message type: "+json.messageType);
                break;
        }
    }

    //server function
    public void OnClientMessage(UnityEngine.Networking.NetworkMessage netMsg) {
        NetworkInstanceVars json = JsonUtility.FromJson<NetworkInstanceVars>(netMsg.reader.ReadString());

        switch(json.messageType) {
            case "winner":
                Debug.Log("Server Message type: winner");
                
                break;
            default:
                Debug.Log("unknown client message type: " + json.messageType);
                break;
        }
    }

    // server function
    public void OnServerConnected(UnityEngine.Networking.NetworkMessage netMsg){
        Debug.Log("server: client connected");
        StartCoroutine(updatePlayerList());
    }

    private IEnumerator updatePlayerList(){
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("PlayerListText").GetComponent<Text>().text = "";
        serverPlayerList = new List<string>();
        foreach(NetworkConnection item in NetworkServer.connections) {
            serverPlayerList.Add(item.address);
            GameObject.Find("PlayerListText").GetComponent<Text>().text += "\n" + item.address;
        }
        string jsonPlayerList = JsonUtility.ToJson(new NetworkInstanceVars("player list", serverPlayerList.ToArray()));
        NetworkMessageBase clientMessage = new NetworkMessageBase(jsonPlayerList);
        NetworkServer.SendToAll(1337, clientMessage);
    }

    public bool getIsHost() {
        return isHost;
    }

    public void sendWinnerMessageToServer() {
        myClient.Send(1337, new NetworkMessageBase(JsonUtility.ToJson(new NetworkInstanceVars("winner"))));
    }
}
