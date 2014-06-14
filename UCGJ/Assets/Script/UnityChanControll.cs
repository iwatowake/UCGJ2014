using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class UnityChanControll : MonoBehaviour {
	[System.Serializable]
	public class Player{
		public int id;

		public string getVString(){
			return "Vertical" + id;
		}
		public string getHString(){
			return "Horizontal" + id;
		}
	}

	[SerializeField]
	private Player player;
	private CharacterController controller;
	public int speed;

	private Transform child;
	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		child = this.GetComponentInChildren<Renderer>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis(player.getHString()) * Time.deltaTime * speed;
		float v = Input.GetAxis(player.getVString()) * Time.deltaTime * speed;

		controller.Move(new Vector3(h,0,v));
//		this.rigidbody.AddForce(child.forward * v + child.right *	h);


		if(Input.GetButton("Jump")){
			this.rigidbody.AddForce(child.forward * v + child.right *	h,ForceMode.Impulse);
		}
	}
}
