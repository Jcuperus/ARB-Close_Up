using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FeedbackIncorrectPopupBehaviour : MonoBehaviour {

	void Awake () {
        this.gameObject.SetActive(false);
        UIManager uiManager = (UIManager)GameObject.Find("Managers").GetComponent(typeof(UIManager));
        uiManager.setIncorrectFeedbackPopup(this.gameObject.GetComponent<Image>());
    }
	
}
