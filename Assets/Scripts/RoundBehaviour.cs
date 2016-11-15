using UnityEngine;
using System.Collections;

public class RoundBehaviour : MonoBehaviour {
    public int timeLimit = 90;
    private int roundNumber = -1;
    private int objective;
    private GameObject[] objectiveCollection;
    private string objectiveTag = "Objective";
    private int timeLeft;

    public void startRound(int objective) {
        timeLeft = timeLimit;
        StartCoroutine(updateTimer());
        setObjective(objective);
        roundNumber++;
    }

    public void updateObjectives() {
        objectiveCollection = GameObject.FindGameObjectsWithTag(objectiveTag);
    }

    public void setObjective(int index) {
        //Debug.Log(objectiveCollection[index]);
        this.objective = index;
    }

    private IEnumerator updateTimer() {
        //initial timer?
        while (this.isActiveAndEnabled) {
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
    }

    public bool checkObjective(GameObject selection) {
        return selection.Equals(objectiveCollection[objective]);
    }    
}
