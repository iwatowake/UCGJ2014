using UnityEngine;
using System.Collections;

public class UnityChanControll1 : MonoBehaviour {

	private Transform child;
	// Use this for initialization
	void Start () {
		child = this.GetComponentInChildren<Renderer>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal1");
		float v = Input.GetAxis("Vertical1");

		this.rigidbody.AddForce(child.forward * v + child.right *	h);
	}
}
