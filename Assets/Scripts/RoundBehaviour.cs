using UnityEngine;
using System.Collections;

public class RoundBehaviour : MonoBehaviour {
    public int timeLimit = 90;
    public int roundLimit = 3;
    private int roundNumber = 0;
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

        updateObjectiveUI();
    }

    public void updateObjectiveUI() {
        foreach (Transform child in GameObject.Find("ObjectiveHolder").transform) {
            Debug.Log("child destroyed: " + child.gameObject.ToString());
            Destroy(child.gameObject);
        }

        Destroy(GameObject.Find("ObjectiveHolder").transform.GetChild(0).gameObject);

        GameObject clone = (GameObject)Instantiate(objectiveCollection[objective], GameObject.Find("ObjectiveHolder").transform);

        if(clone.GetComponent<WalkingScript>() != null) {
            Destroy(clone.GetComponent<WalkingScript>());
        }

        clone.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        clone.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;

        clone.transform.localPosition = new Vector3(0, 0, 0);
        clone.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }

    public void setObjective(int index) {
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
