using UnityEngine;
using System.Collections;

public class RoundBehaviour : MonoBehaviour {
    public GameObject objective;
    public int timeLimit = 90;
    private GameObject[] objectiveCollection;
    private string objectiveTag = "Objective";
    private int timeLeft;

    void Awake() {
        timeLeft = timeLimit;
        StartCoroutine(updateTimer());
        objectiveCollection = GameObject.FindGameObjectsWithTag(objectiveTag);
    }

    public void setObjective(int index) {
        objective = objectiveCollection[index];
    }

    private IEnumerator updateTimer() {
        //initial timer?
        while (this.isActiveAndEnabled) {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            Debug.Log("time left: " + timeLeft);
        }
    }

    public bool checkObjective(GameObject selection) {
        return selection.Equals(objective);
    }    
}
