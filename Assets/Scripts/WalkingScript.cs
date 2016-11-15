using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour {
    public int speed = 60;
    public int directionChangeTimer = 3;
    public bool isMoving = true;
    private Vector3 rotation = new Vector3();

	// Use this for initialization
	void Awake () {
        rotation.Set(0,Random.Range(0, 360),0);
        StartCoroutine(changeDirection());
    }
	
	// Update is called once per frame
	void Update () {
        if(isMoving) {
            this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
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
