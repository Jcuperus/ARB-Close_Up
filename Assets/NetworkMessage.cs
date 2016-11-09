using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkMessage : MessageBase {
    public string message;

    public NetworkMessage(string message) {
        this.message = message;
    }
}
