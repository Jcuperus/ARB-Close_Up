using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButtonBehaviour : MonoBehaviour {
    void Awake() {
        NetworkManagerScript networkManagerScript = (NetworkManagerScript)GameObject.Find("NetworkManager").GetComponent(typeof(NetworkManagerScript));
        this.gameObject.SetActive(networkManagerScript.getIsHost());

        Button joinButton = GameObject.Find("StartButton").GetComponent<Button>();
        joinButton.onClick.AddListener(networkManagerScript.handleStart);
    }
}
