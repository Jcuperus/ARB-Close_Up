using UnityEngine;
using System.Collections;

public class WinnerPanelBehaviour : MonoBehaviour {
    private UIManager uim;

	// Use this for initialization
	void Awake () {
        gameObject.SetActive(false);
        uim = GameObject.Find("Managers").GetComponent<UIManager>();
        uim.setWinnerPanel(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
