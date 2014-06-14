using UnityEngine;
using System.Collections;

public class UnityChanControll : MonoBehaviour {

	private Transform child;
	// Use this for initialization
	void Start () {
		child = this.GetComponentInChildren<Renderer>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		this.rigidbody.AddForce(child.forward * v + child.right *	h);

		if(Input.GetButton("Jump")){
			this.rigidbody.AddForce(child.forward * v + child.right *	h,ForceMode.Impulse);
		}
	}
}
