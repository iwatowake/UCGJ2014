using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class TitleUnityChanControll : MonoBehaviour{
	private const float gravity = -9.8f;
	[System.Serializable]
	public class Player{
		public int id;
		[SerializeField]
		private int power = 100;
		public BillBoard bb;
		public int Power{
			get{
				return power;
			}
			set{
				power = value;
			}
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

	private Vector3 attackDir = Vector3.zero;
	private float attackPow = 0.0f;
	private Vector3 impactPow;

	void Awake(){
		this.tag = "Player";
		speed = 10;
	}

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
		player.bb = GetComponentInChildren<BillBoard>();
		player.bb.setPower(player.Power);
		charge = 0;

		setSE ();
	}
	void setSE(){
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("power26","Charge"));
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("attack00","PreAttack"));
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
		if(ucState == UCState.Attack){
			if(c.transform.name == "TwoPlayer"){
				Destroy(c.gameObject);
				Debug.Log ("2");
				PlayerPrefs.SetInt(GameStateManager.PLAYER_NUM_KEY, 2);
				Application.LoadLevel("StageSelect");
			}
			if(c.transform.name == "ThreePlayer"){
				Destroy(c.gameObject);
				Debug.Log ("3");
				PlayerPrefs.SetInt(GameStateManager.PLAYER_NUM_KEY, 3);
				Application.LoadLevel("StageSelect");
			}
			if(c.transform.name == "FourPlayer"){
				Destroy(c.gameObject);
				Debug.Log ("4");
				PlayerPrefs.SetInt(GameStateManager.PLAYER_NUM_KEY, 4);
				Application.LoadLevel("StageSelect");
			}
		}
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
	void ReturnIdle(){
		CancelInvoke("ReturnIdle");
		setState(UCState.Idle);
	}
	void SetAfterDamage(){
		setState(UCState.AfterDamage,1.5f);
	}
}
