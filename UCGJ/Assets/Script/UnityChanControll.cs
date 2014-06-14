using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class UnityChanControll : MonoBehaviour, UnityChanCollisionInterface {
	private const float gravity = -9.8f;
	[System.Serializable]
	public class Player{
		public int id;
		public int power = 100;
		public int Power{
			get{
				return power;
			}
		}

		public void AddPower(int pow){
			power += pow;
		}
		public string getVString(){
			return "Vertical" + id;
		}
		public string getHString(){
			return "Horizontal" + id;
		}
		public string getChargeString(){
			return "ButtonA" + id;
		}
	}
	public void Damage(int pow){
		player.AddPower(-pow);
		GameObject obj = (GameObject)Instantiate(particlePrefab,this.transform.position,this.transform.rotation);
		ParticleSystem p = obj.GetComponent<ParticleSystem>();
		p.emissionRate = pow;
		if(player.Power < 0){
			Destroy(this.gameObject);
		}
	}
	private enum UCState{
		Idle,
		Charge,
		Attack,
		OnDamage,
		Pararaise
	}



	[SerializeField]
	public Player player;
	private CharacterController controller;
	public float charge;
	private bool LookRight;
	private BillBoard bb;

	public int speed;
	private UCState ucState;

	public GameObject particlePrefab;

	private Vector3 attackDir = Vector3.zero;
	private float attackPow = 0.0f;
	void Awake(){
		this.tag = "Player";
	}

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		bb = GetComponentInChildren<BillBoard>();
		ucState = UCState.Idle;
		charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
		float h = Input.GetAxis(player.getHString()) *  speed;
		float v = Input.GetAxis(player.getVString()) * speed;

		if(h > 0){
			bb.lookRight();
		}else{
			bb.lookLeft();
		}

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
				attackDir = new Vector3(h,0,v);
				attackPow = Mathf.Clamp(charge/3.0f,1.0f,3.0f) * speed;
				ucState = UCState.Attack;
			}else{
				charge += Time.deltaTime;
			}
//			GameObject.FindG
			break;
		case UCState.Attack:
			if(charge > 0){
				controller.Move(attackDir*Time.deltaTime * speed * attackPow);
				charge -= Time.deltaTime * 2;
			}else{
				ucState = UCState.Idle;
				attackDir = Vector3.zero;
				charge = 0;
			}
			break;
		case UCState.OnDamage:
			break;
		case UCState.Pararaise:
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
				this.Damage(uc.player.Power/10 + 1);
				return;
			}else
			if(this.ucState == UCState.Attack
			   && (uc.ucState == UCState.Idle||uc.ucState == UCState.Charge)){
				Debug.Log ("Collision with P2");
				uc.Damage(this.player.Power/10 + 1);
				return;
			}
		}
		else if(c.transform.tag == "obj"){
			if(this.ucState == UCState.Attack){
				this.ucState = UCState.Idle;
			}
		}
	}

	void OnParticleCollision(){
		Debug.Log ("PaticleColl");
		player.AddPower(1);
	}

	private void setState(UCState st){
		ucState = st;
		switch(st){
		case UCState.Idle:
			break;
		case UCState.Charge:
			break;
		case UCState.Attack:
			break;
		case UCState.OnDamage:
			break;
		case UCState.Pararaise:
			break;
		}
	}
	
	public void OnCollDamage(int damage){
		Damage(damage);
	}
	public void OnCollPararise(float sec){}
	public void OnCollImpact(Vector3 pow){}

}
