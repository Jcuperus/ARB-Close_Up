using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkMessageBase : MessageBase {
    public string message;

    public NetworkMessageBase(string message) {
        this.message = message;
    }
}
