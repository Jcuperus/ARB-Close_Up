using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkInstanceVars : MessageBase {
    public string messageType;
    public string[] playerList;
    public int objective;

    public NetworkInstanceVars(string messageType, string[] playerList) {
        this.messageType = messageType;
        this.playerList = playerList;
    }

    public NetworkInstanceVars(string messageType) {
        this.messageType = messageType;
    }

    public NetworkInstanceVars(string messageType, int objective) {
        this.messageType = messageType;
        this.objective = objective;
    }
}
