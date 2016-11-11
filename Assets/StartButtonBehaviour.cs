using UnityEngine;
using System.Collections;

public class StartButtonBehaviour : MonoBehaviour {
    void Awake() {
        NetworkManagerScript networkManagerScript = (NetworkManagerScript)GameObject.Find("NetworkManager").GetComponent(typeof(NetworkManagerScript));
        this.gameObject.SetActive(networkManagerScript.getIsHost());
    }
}
