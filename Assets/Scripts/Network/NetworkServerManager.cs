using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class NetworkServerManager : MonoBehaviour {
    private Dictionary<string, string> serverPlayerList = new Dictionary<string, string>();
    private Dictionary<string, int> serverScoreList = new Dictionary<string, int>();
    private NetworkClientManager clientManager;
    private bool first = true;
    private int objectiveCount = 3; //Get amount of objectives
    public int round = 0;

    void Start() {
        clientManager = (NetworkClientManager)GetComponent(typeof(NetworkClientManager));
        DontDestroyOnLoad(this.gameObject);
    }

    public void startRound() {
        int objective = Random.Range(0, objectiveCount-1); //random (range is het aantal npc's)

        //TODO: move this shit
        if (first) { //Init scoreboard
            foreach (KeyValuePair<string, string> player in serverPlayerList) {
                serverScoreList.Add(player.Key, 0);
            }
            first = false;
        }

        NetworkMessageBase clientMessage = new NetworkMessageBase(JsonUtility.ToJson(new NetworkInstanceVars("start", objective)));
        NetworkServer.SendToAll(1337, clientMessage);
    }

    // Create a server and listen on a port
    public void SetupServer() {
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnected);
        NetworkServer.RegisterHandler(1337, OnClientMessage);
    }

    public void OnClientMessage(UnityEngine.Networking.NetworkMessage netMsg) {
        NetworkInstanceVars json = JsonUtility.FromJson<NetworkInstanceVars>(netMsg.reader.ReadString());

        switch(json.messageType) {
            case "winner":
                Debug.Log("Server Message type: winner: " + serverPlayerList[netMsg.conn.address]);
                int objective = Random.Range(0, objectiveCount - 1);
                sendWinnerMessageToClient(netMsg.conn.address, objective);

                //Next round, update scores
                serverScoreList[netMsg.conn.address]++;
                Debug.Log("Scoreboard");
                foreach (KeyValuePair<string, int> score in serverScoreList) {
                    Debug.Log(score.Key + ": " + score.Value + "point(s)");
                }
                break;
            case "name":
                Debug.Log("Server Message type: name: " + json.name);
                serverPlayerList[netMsg.conn.address] = json.name;
                if(!clientManager.getClientPlayerList().Contains(json.name)) {
                    clientManager.addToClientPlayerList(json.name);
                }
                syncPlayerLists();
                break;
            default:
                Debug.Log("unknown message from client type: " + json.messageType);
                break;
        }
    }

    public void OnServerConnected(UnityEngine.Networking.NetworkMessage netMsg) {
        Debug.Log("server: client connected");
        serverPlayerList[netMsg.conn.address] = null;
    }

    private void syncPlayerLists() {
        string jsonPlayerList = JsonUtility.ToJson(new NetworkInstanceVars("player list", clientManager.getClientPlayerList().ToArray()));
        NetworkMessageBase clientMessage = new NetworkMessageBase(jsonPlayerList);
        NetworkServer.SendToAll(1337, clientMessage);
    }

    private void sendWinnerMessageToClient(string ip, int objective) {
        string jsonPlayerList = JsonUtility.ToJson(new NetworkInstanceVars("winner", serverPlayerList[ip], objective));
        NetworkMessageBase clientMessage = new NetworkMessageBase(jsonPlayerList);
        NetworkServer.SendToAll(1337, clientMessage);
    }


}
