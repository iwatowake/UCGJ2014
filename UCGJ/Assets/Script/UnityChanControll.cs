using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class UnityChanControll : MonoBehaviour, UnityChanCollisionInterface {
	private const float gravity = -9.8f;
	private const int BIG_SPEED = 4;
	private const int NORM_SPEED = 10;
	private const int LITTLE_SPEED = 20;
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
			speed = BIG_SPEED;
			setSize(2.0f);
		}else if(player.Power > 20){
			speed = NORM_SPEED;
			setSize(1.0f);
		}else{
			speed = LITTLE_SPEED;
			setSize(0.5f);
		}

	}
	private enum UCState{
		Idle,
		Charge,
		Attack,
		OnDamage,
		AfterDamage,
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

	private int speed;
	private UCState ucState;

	public GameObject particlePrefab;

	private Vector3 attackDir = Vector3.zero;
	private float attackPow = 0.0f;
	private Vector3 impactPow;

	void Awake(){
		this.tag = "Player";
		speed = NORM_SPEED;
	}

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		player.bb = GetComponentInChildren<BillBoard>();
		baseScale = this.transform.localScale;
		charge = 0;

		GetPower(0);
		setSE ();
	}
	void setSE(){
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("power26","Charge"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("attack00","PreAttack"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1092","Damege0"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1093","Damege1"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1094","Damege2"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1095","Damege3"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1091","Damege4"));
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
				player.bb.SetRun(true);
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
		case UCState.AfterDamage:
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
				SoundPlayer.Instance.playSE("Damege" + Random.Range(0,5));
				this.Damage(uc.player.Power/20 + 1);
				this.OnCollImpact(-c.normal * uc.player.Power / 10f);
				return;
			}else
			if(this.ucState == UCState.Attack
			   && (uc.ucState == UCState.Idle||uc.ucState == UCState.Charge)){
				SoundPlayer.Instance.playSE("Damege" + Random.Range(0,5));
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
		if(ucState == UCState.Attack){
			player.bb.smoke.SetActive(false);
		}
		ucState = st;
		switch(st){
		case UCState.Idle:
			player.bb.setIdle();
			break;
		case UCState.Charge:
			player.bb.smoke.SetActive(true);
			SoundPlayer.Instance.playSE("Charge");
			break;
		case UCState.Attack:
			SoundPlayer.Instance.playSE("PreAttack");
			break;
		case UCState.OnDamage:
			player.bb.setDamage();
			Invoke ("SetAfterDamage",param);
			break;
		case UCState.AfterDamage:
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
	void SetAfterDamage(){
		setState(UCState.AfterDamage,1.5f);
	}
	public GameObject uni8Coin;
	private void spawnUni8(int num){
		for(int i = 0; i < num; i++){
			int x = Random.Range(-1,3) * 4;
			int y = 5;
			int z = Random.Range(-1,3) * 4;
			Instantiate(uni8Coin,this.transform.position + new Vector3(x,y,z),Quaternion.identity);
		}
	}
	
	public void OnCollDamage(int damage){
		if(ucState == UCState.Idle || ucState == UCState.Charge){
			Debug.Log ("CollD");
			Damage(damage);
			setState(UCState.OnDamage);
		}
	}
	public void OnCollPararise(float sec){
		Debug.Log ("CollP");
		if(ucState == UCState.Idle || ucState == UCState.Charge){
			setState(UCState.Pararaise);
		}
	}

	public void OnCollImpact(Vector3 pow){
		Debug.Log ("CollI");
		impactPow = pow;
			if(ucState == UCState.Idle || ucState == UCState.Charge){
				setState(UCState.Impacted);
			}
	}

}
