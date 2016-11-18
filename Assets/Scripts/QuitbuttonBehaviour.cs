using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitbuttonBehaviour : MonoBehaviour {
    void Awake() {
        UIManager uiManagerScript = (UIManager)GameObject.Find("Managers").GetComponent(typeof(UIManager));
        NetworkClientManager clientManager = (NetworkClientManager)GameObject.Find("Managers").GetComponent(typeof(NetworkClientManager));
        Button quitButton = this.GetComponent<Button>();
        quitButton.onClick.AddListener(uiManagerScript.handleQuit);
    }
}
