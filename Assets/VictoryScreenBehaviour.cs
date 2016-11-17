using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryScreenBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Awake () {
        string victoryMessage = GameObject.Find("Managers").GetComponent<UIManager>().getVictoryMessage();
        gameObject.GetComponent<Text>().text = victoryMessage;
	}
}
