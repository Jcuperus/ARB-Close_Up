using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkInstanceVars : MessageBase {
    public string messageType;
    public string[] playerListName;
    public int objective;
    public string name;

    public NetworkInstanceVars(string messageType, string[] playerListName) {
        this.messageType = messageType;
        this.playerListName = playerListName;
    }

    public NetworkInstanceVars(string messageType) {
        this.messageType = messageType;
    }

    public NetworkInstanceVars(string messageType, int objective) {
        this.messageType = messageType;
        this.objective = objective;
    }

    public NetworkInstanceVars(string messageType, string name) {
        this.messageType = messageType;
        this.name = name;
    }

    public NetworkInstanceVars(string messageType, string name, int objective) {
        this.messageType = messageType;
        this.name = name;
        this.objective = objective;
    }
}
