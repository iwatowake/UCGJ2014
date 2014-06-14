using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class UnityChanControll : MonoBehaviour, UnityChanCollisionInterface {
	private const float gravity = -9.8f;
	[System.Serializable]
	public class Player{
		public int id;
		[SerializeField]
		private int power = 100;
		public GaugeController gauge;
		public CounterController count;
		public BillBoard bb;
		public int Power{
			get{
				return power;
			}
			set{
				power = value;
				bb.setPower(value);
				gauge.setGauge(value);
				count.setPoints(value);
			}
		}

		public void AddPower(int pow){
			Power = Power + pow;
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
		spawnUni8(pow);
	}
	public void GetPower(int pow){
		player.AddPower(pow);
		if(player.Power > 50){
			setSize(2.0f);
		}else if(player.Power > 30){
			setSize(1.0f);
		}else{
			setSize(0.5f);
		}

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
	private Animator animator;
	private Vector3 baseScale;

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
		player.bb = GetComponentInChildren<BillBoard>();
		baseScale = this.transform.localScale;
		charge = 0;

		setSE ();
	}
	void setSE(){
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("power26","Charge"));
	}

	private bool isInit = false;
	// Update is called once per frame
	void Update () {
		if(!isInit){
			player.Power = player.Power;
			isInit = true;
		}
		float h = Input.GetAxis(player.getHString()) *  speed;
		float v = Input.GetAxis(player.getVString()) * speed;

		if(h > 0.1f){
			player.bb.lookRight();
		}else if (h < -0.1f){
			player.bb.lookLeft();
		}

		switch(ucState){
		case UCState.Idle:

			if(Input.GetButtonDown(player.getChargeString())){
				setState(UCState.Charge);
				charge = 0;
			}else{
				player.bb.SetRun(Mathf.Abs(h)> 0.1f || Mathf.Abs(v) > 0.1f);
				controller.Move(new Vector3(h,gravity,v) * Time.deltaTime);
			}
			break;
		case UCState.Charge:
			if(!Input.GetButton(player.getChargeString())){
				attackDir = new Vector3(h,0,v);
				if(attackDir.magnitude <= 0.01f){
					if(player.bb.IsRight()){
						attackDir = Vector3.right;
					}else{
						attackDir = Vector3.left;
					}
				}
				player.bb.SetRun(true);
				attackPow = Mathf.Clamp(charge * 2,1.0f,3.0f) * speed;
				setState(UCState.Attack);
			}else{
				charge += Time.deltaTime;
			}
//			GameObject.FindG
			break;
		case UCState.Attack:
			if(charge > 0){
				controller.Move(attackDir.normalized*Time.deltaTime * attackPow);
				charge -= Time.deltaTime * 3;
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
				this.OnCollImpact(-c.normal * uc.player.Power / 10f);
				return;
			}else
			if(this.ucState == UCState.Attack
			   && (uc.ucState == UCState.Idle||uc.ucState == UCState.Charge)){
				uc.Damage(this.player.Power/20 + 1);
				uc.OnCollImpact(-c.normal * uc.player.Power / 10f);
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
			player.bb.setIdle();
			break;
		case UCState.Charge:
			SoundPlayer.Instance.playSE("Charge");
			break;
		case UCState.Attack:
			break;
		case UCState.OnDamage:
			player.bb.setDamage();
			if(param > 0){
				Invoke ("ReturnIdle",param);
			}
			break;
		case UCState.Pararaise:
			player.bb.setDamage();
			if(param > 0){
				Invoke ("ReturnIdle",param);
			}
			break;
		case UCState.Impacted:
			player.bb.setDamage();
			break;
		}
	}
	private void setSize(float mul){
		this.transform.localScale = baseScale * mul;
	}

	void ReturnIdle(){
		CancelInvoke("ReturnIdle");
		setState(UCState.Idle);
	}

	public GameObject uni8Coin;
	private void spawnUni8(int num){
		for(int i = 0; i < num; i++){
			int x = Random.Range(-5,5);
			int y = Random.Range(0,5);
			int z = Random.Range(-5,5);
			Instantiate(uni8Coin,this.transform.position + new Vector3(x,y,z),Quaternion.identity);
		}
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
