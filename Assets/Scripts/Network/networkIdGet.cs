using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;

public class networkIdGet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("ip test");
        string hostName = Dns.GetHostName();
        IPHostEntry host = Dns.GetHostEntry(hostName);
        foreach (IPAddress ip in host.AddressList) {
            if (ip.AddressFamily == AddressFamily.InterNetwork) {
                this.GetComponentInParent<Text>().text = "IP: " + ip.ToString();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
