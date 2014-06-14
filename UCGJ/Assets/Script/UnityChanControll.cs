﻿using UnityEngine;
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

		setState(UCState.OnDamage,1.5f);
		GameObject obj = (GameObject)Instantiate(particlePrefab,this.transform.position,this.transform.rotation);
		ParticleSystem p = obj.GetComponent<ParticleSystem>();
		p.emissionRate = pow;
		if(player.Power < 0){
			Destroy(this.gameObject);
		}
		Destroy(obj,5.0f);
	}
	private enum UCState{
		Idle,
		Charge,
		Attack,
		OnDamage,
		Pararaise,
		Impacted
	}



	[SerializeField]
	public Player player;
	private CharacterController controller;
	public float charge;
	private bool LookRight;
	private BillBoard bb;
	private Animator animator;

	public int speed;
	private UCState ucState;

	public GameObject particlePrefab;

	private Vector3 attackDir = Vector3.zero;
	private float attackPow = 0.0f;
	private Vector3 impactPow;

	void Awake(){
		this.tag = "Player";
	}

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		bb = GetComponentInChildren<BillBoard>();
		charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis(player.getHString()) *  speed;
		float v = Input.GetAxis(player.getVString()) * speed;

		if(h > 0.1f){
			bb.lookRight();
		}else if (h < -0.1f){
			bb.lookLeft();
		}

		switch(ucState){
		case UCState.Idle:

			if(Input.GetButtonDown(player.getChargeString())){
				setState(UCState.Charge);
				charge = 0;
			}else{
				bb.SetRun(Mathf.Abs(h)> 0.1f || Mathf.Abs(v) > 0.1f);
				controller.Move(new Vector3(h,gravity,v) * Time.deltaTime);
			}
			break;
		case UCState.Charge:
			if(!Input.GetButton(player.getChargeString())){
				attackDir = new Vector3(h,0,v);
				attackPow = Mathf.Clamp(charge/3.0f,1.0f,3.0f) * speed;
				setState(UCState.Attack);
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
				setState(UCState.Idle);
				attackDir = Vector3.zero;
				charge = 0;
			}
			break;
		case UCState.OnDamage:
			break;
		case UCState.Pararaise:
			break;
		case UCState.Impacted:
			if(impactPow.magnitude > 0.1f){
				controller.Move(impactPow * Time.deltaTime);
				impactPow *= 0.99f;
			}else{
				setState(UCState.Idle);
			}
			break;
		}
	}
	void OnControllerColliderHit(ControllerColliderHit c){
		if(c.transform.tag == "Player"){

			UnityChanControll uc = c.transform.GetComponent<UnityChanControll>();
			if(uc.ucState == UCState.Attack
			   && (this.ucState == UCState.Idle||this.ucState == UCState.Charge)){
				this.Damage(uc.player.Power/20 + 1);
				this.OnCollImpact(-c.normal * uc.player.power / 10f);
				return;
			}else
			if(this.ucState == UCState.Attack
			   && (uc.ucState == UCState.Idle||uc.ucState == UCState.Charge)){
				uc.Damage(this.player.Power/20 + 1);
				uc.OnCollImpact(-c.normal * uc.player.power / 10f);
				return;
			}
		}
		else if(c.transform.tag == "obj"){
			if(this.ucState == UCState.Attack){
				setState(UCState.Idle);
			}
		}
	}

	void OnParticleCollision(){
		player.AddPower(1);
	}

	private void setState(UCState st, float param = 0.0f){
		if(ucState == st){
			return;
		}
		ucState = st;
		switch(st){
		case UCState.Idle:
			bb.setIdle();
			break;
		case UCState.Charge:
			break;
		case UCState.Attack:
			break;
		case UCState.OnDamage:
			bb.setDamage();
			if(param > 0){
				Invoke ("ReturnIdle",param);
			}
			break;
		case UCState.Pararaise:
			bb.setDamage();
			if(param > 0){
				Invoke ("ReturnIdle",param);
			}
			break;
		case UCState.Impacted:
			bb.setDamage();
			break;
		}
	}

	void ReturnIdle(){
		CancelInvoke("ReturnIdle");
		setState(UCState.Idle);
	}
	
	public void OnCollDamage(int damage){
		Damage(damage);
	}
	public void OnCollPararise(float sec){
		setState(UCState.Pararaise);
	}
	public void OnCollImpact(Vector3 pow){
		impactPow = pow;
		setState(UCState.Impacted);
	}

}
