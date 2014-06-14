using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class UnityChanControll : MonoBehaviour, UnityChanCollisionInterface {
	private const float gravity = -9.8f;
	[System.Serializable]
	public class Player{
		public int id;

		public string getVString(){
			return "Vertical" + id;
		}
		public string getHString(){
			return "Horizontal" + id;
		}
		public string getChargeString(){
			return "Charge" + id;
		}
	}
	private enum UCState{
		Idle,
		Charge,
		Attack,
		OnDamage
	}



	[SerializeField]
	private Player player;
	private CharacterController controller;
	private Transform child;
	private int charge;

	public int speed;
	private UCState ucState;

	private Vector3 lastDir = Vector3.zero;
	void Awake(){
		this.tag = "Player";
	}

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		child = this.GetComponentInChildren<Renderer>().transform;
		ucState = UCState.Idle;
		charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		float h = Input.GetAxis(player.getHString()) *  speed;
		float v = Input.GetAxis(player.getVString()) * speed;

		switch(ucState){
		case UCState.Idle:
			if(Input.GetButtonDown(player.getChargeString())){
				ucState = UCState.Charge;
				charge = 0;
			}else{
				controller.Move(new Vector3(h,gravity,v) * Time.deltaTime);
			}
			break;
		case UCState.Charge:
			if(!Input.GetButton(player.getChargeString())){
				lastDir = new Vector3(h,0,v);
				ucState = UCState.Attack;
			}else{
				charge += 1;
			}
			break;
		case UCState.Attack:
			if(charge > 0){
				controller.Move(lastDir*Time.deltaTime * speed * 3);
				charge -= 2;
			}else{
				ucState = UCState.Idle;
				lastDir = Vector3.zero;
				charge = 0;
			}
			break;
		case UCState.OnDamage:
			break;
		}
	}
	void OnControllerColliderHit(ControllerColliderHit c){
		if(c.transform.tag == "Player"){

			UnityChanControll uc = c.transform.GetComponent<UnityChanControll>();
			Debug.Log ("Collision with P:" + ucState + " : " + uc.ucState);
			Debug.Log ("Collision with " + c.gameObject.name);
			if(uc.ucState == UCState.Attack
			   && (this.ucState == UCState.Idle||this.ucState == UCState.Charge)){
				Debug.Log ("Collision with P2");
				Destroy(this.gameObject);
				return;
			}
			else if(this.ucState == UCState.Attack
			        && (uc.ucState == UCState.Idle||uc.ucState == UCState.Charge)){
				Debug.Log ("Collision with P3");
				Destroy(c.gameObject);
				return;
			}
		}
		else if(c.transform.tag == "obj"){
			if(this.ucState == UCState.Attack){
				this.ucState = UCState.Idle;
			}
		}
	}
	
	public void OnCollDamage(int damage){}
	public void OnCollPararise(float sec){}
	public void OnCollImpact(Vector3 pow){}

}
