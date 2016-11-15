using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedbackCorrectPopupBehaviour : MonoBehaviour {

	void Awake () {
        this.gameObject.SetActive(false);
        UIManager uiManager = (UIManager)GameObject.Find("Managers").GetComponent(typeof(UIManager));
        uiManager.setCorrectFeedbackPopup(this.gameObject.GetComponent<Image>());
    }
	
}
