using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkClientManager : MonoBehaviour {
    private NetworkClient myClient;
    private bool isHost = false;
    private List<string> clientPlayerList = new List<string>();
    private string nickname;
    private UIManager uiManager;
    private int objective;

    void Start() {
        DontDestroyOnLoad(this.gameObject);
        uiManager = (UIManager)GetComponent(typeof(UIManager));
    }

    // Create a client and connect to the server port
    public void SetupClient() {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(1337, OnServerMessage);
        string ipInput = GameObject.Find("IPInputField/Text").GetComponent<Text>().text;
        myClient.Connect(ipInput, 4444);
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient() {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(1337, OnServerMessage);
    }

    public void OnServerMessage(UnityEngine.Networking.NetworkMessage netMsg) {
        NetworkInstanceVars json = JsonUtility.FromJson<NetworkInstanceVars>(netMsg.reader.ReadString());

        switch(json.messageType) {
            case "start":
                Debug.Log("Client Message type: start");
                SceneManager.LoadScene("mainScene");
                objective = json.objective;
                break;
            case "player list":
                Debug.Log("Client Message type: player list");
                clientPlayerList = new List<string>(json.playerListName);
                uiManager.updatePlayerListUI(clientPlayerList);
                break;
            case "winner":
                Debug.Log("Client Message type: winner: " + json.name);

                //winner panel show subroutine
                StartCoroutine(uiManager.showWinnerPanel(json.name));
                //
                break;
            default:
                Debug.Log("unknown message from server type: " + json.messageType);
                break;
        }
    }

    public void OnConnected(UnityEngine.Networking.NetworkMessage netMsg) {
        Debug.Log("Connected to server");
        nickname = GameObject.Find("NameInputField/Text").GetComponent<Text>().text;
        SceneManager.LoadScene("lobbyScene");
        myClient.Send(1337, new NetworkMessageBase(JsonUtility.ToJson(new NetworkInstanceVars("name", nickname))));
    }

    public bool getIsHost() {
        return isHost;
    }

    public void setHost(bool host) {
        isHost = host;
    }

    public int getObjective() {
        return objective;
    }

    public void setClientPlayerList(List<string> newPlayerList) {
        clientPlayerList = newPlayerList;
    }

    public void addToClientPlayerList(string entry) {
        clientPlayerList.Add(entry);
    }

    public List<string> getClientPlayerList() {
        return clientPlayerList;
    }

    public void sendWinnerMessageToServer() {
        myClient.Send(1337, new NetworkMessageBase(JsonUtility.ToJson(new NetworkInstanceVars("winner"))));
    }
}
