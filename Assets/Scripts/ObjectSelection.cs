 using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectSelection : MonoBehaviour {
    private RaycastHit hitInfo;
    public LayerMask colLayer;
    public RoundBehaviour round;
    private bool canSelect = true;
    private int wrongAnswerDelay = 5;

	// Use this for initialization
	void Start () {
        round = GameObject.Find("Managers").GetComponent<RoundBehaviour>();
        round.updateObjectives();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) //Mouse click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            CastRay(ray);
        }

        foreach (Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began) //Touch
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                CastRay(ray);
            }
        }
	}

    void CastRay(Ray ray)
    {
        RaycastHit rhit;
        GameObject gObjectHit = null;
        
        if (Physics.Raycast(ray, out rhit, 1000.0f) && canSelect)
        {
            if (round.checkObjective(rhit.transform.gameObject)) {
                Debug.Log("Correct selection");
                NetworkClientManager clientManager = (NetworkClientManager)GameObject.Find("Managers").GetComponent(typeof(NetworkClientManager));
                clientManager.sendWinnerMessageToServer();
            }
            else {
                Debug.Log("Incorrect selection");
                StartCoroutine(disableSelection());
            }
        }
    }

    IEnumerator disableSelection() {
        canSelect = false;
        yield return new WaitForSeconds(wrongAnswerDelay);
        canSelect = true;
    }
}
