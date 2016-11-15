using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerLabel : MonoBehaviour {
    public int timeLimit = 5;
    private int timer;
    private Text label;

    void Awake () {
        //this.gameObject.SetActive(false);
        this.timer = timeLimit;
        label = this.gameObject.GetComponent<Text>();
        StartCoroutine(countDown());
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator countDown() {
        while (timer >= 0) {
            label.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer--;
        }
        this.gameObject.SetActive(false);
        timer = timeLimit;
    }
}
