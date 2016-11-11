using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkInstanceVars : MessageBase {
    public string messageType;
    public string[] playerList;

    public NetworkInstanceVars(string messageType, string[] playerList) {
        this.messageType = messageType;
        this.playerList = playerList;
    }
}
