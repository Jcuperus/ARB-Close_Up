using UnityEngine;
using System.Collections;

public class WalkingScript : MonoBehaviour {
    public int speed = 60;
    public int directionChangeTimer = 3;
    public bool isMoving = true;
    private Vector3 rotation;

	// Use this for initialization
	void Awake () {
        //rotation.Set(0,Random.Range(0, 360),0);
        StartCoroutine(changeDirection());
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(speed*Time.deltaTime,0,0));
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("collision");
        float newRotation = rotation.y + 180;
        rotation.Set(0, newRotation, 0);
        this.transform.Rotate(rotation);
    }

    private IEnumerator changeDirection() {
        while(isMoving) {
            yield return new WaitForSeconds(directionChangeTimer);
            //rotation.Set(0, Random.Range(0, 360), 0);
            //this.transform.Rotate(rotation);
        }
    }
}
