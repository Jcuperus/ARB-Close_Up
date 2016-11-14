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
        NetworkClientManager clientManager = (NetworkClientManager)GameObject.Find("Managers").GetComponent(typeof(NetworkClientManager));
        setObjective(clientManager.getObjective());
    }

    public void setObjective(int index) {
        Debug.Log(objectiveCollection[index]);
        objective = objectiveCollection[index];
    }

    public void nextRound() {
        
    }

    private IEnumerator updateTimer() {
        //initial timer?
        while (this.isActiveAndEnabled) {
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
    }

    public bool checkObjective(GameObject selection) {
        return selection.Equals(objective);
    }    
}
