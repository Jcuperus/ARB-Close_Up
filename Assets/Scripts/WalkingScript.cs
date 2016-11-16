using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour {
    public int speed = 60;
    public int directionChangeTimer = 3;
    public bool isMoving = true;
    public GameObject northBoundary;
    public GameObject eastBoundary;
    public GameObject southBoundary;
    public GameObject westBoundary;
    private Vector3 rotation = new Vector3();
    private Vector3 spawnPos;

	// Use this for initialization
	void Awake () {
        rotation.Set(0,Random.Range(0, 360),0);
        StartCoroutine(changeDirection());
        spawnPos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(isMoving) {
            this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            //out of bounds check
            if(this.transform.position.z>northBoundary.transform.position.z || this.transform.position.z < southBoundary.transform.position.z || this.transform.position.x < westBoundary.transform.position.x || this.transform.position.x > eastBoundary.transform.position.x) {
                Debug.Log("out of bounds detected: "+this.ToString());
                this.transform.position = spawnPos;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        //Debug.Log("collision enter: "+other.name);
        if(other.GetComponent<Collider>().gameObject.layer != LayerMask.NameToLayer("Objectives")) {
            this.transform.Rotate(new Vector3(0, 180, 0));
        }
    }

    void OnTriggerExit(Collider other) {
        //Debug.Log("collision leave: "+other.name);
    }

    private IEnumerator changeDirection() {
        while(isMoving) {
            yield return new WaitForSeconds(directionChangeTimer);
            rotation.Set(0, Random.Range(0, 360), 0);
            this.transform.Rotate(rotation);
        }
    }
}
