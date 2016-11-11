using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButtonBehaviour : MonoBehaviour {
    void Awake() {
        Button joinButton = GameObject.Find("StartButton").GetComponent<Button>();
        joinButton.onClick.AddListener(networkManagerScript.handleStart);

        NetworkManagerScript networkManagerScript = (NetworkManagerScript)GameObject.Find("NetworkManager").GetComponent(typeof(NetworkManagerScript));
        this.gameObject.SetActive(networkManagerScript.getIsHost());
    }
}
