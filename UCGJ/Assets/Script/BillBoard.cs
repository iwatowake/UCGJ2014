using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {
	private bool isReverse;
	// Use this for initialization
	void Start () {
		this.transform.rotation = Camera.main.transform.rotation;
		lookRight();
	}
	public void lookRight(){
		this.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0,180,0);
	}
	public void lookLeft(){
		this.transform.rotation = Camera.main.transform.rotation;
	}

	// Update is called once per frame
	void Update () {
	}
}
