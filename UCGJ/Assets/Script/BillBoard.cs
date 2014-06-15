using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {
	private bool isReverse;
	private Animator animator;

	public GameObject smoke;
	public GameObject star;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		lookRight();
	}
	private bool isRight = false;
	public void lookRight(){
		isRight = true;
		this.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0,180,0);
	}
	public void lookLeft(){
		isRight = false;
		this.transform.rotation = Camera.main.transform.rotation;
	}
	public bool IsRight(){
		return isRight;
	}

	public void SetRun(bool isRun){
		animator.SetBool("IsRun",isRun);
	}
	public void setDamage(){
		animator.ResetTrigger("IdlingTrigger");
		animator.SetTrigger("DamageTrigger");
	}
	public void setIdle(){
		animator.ResetTrigger("DamageTrigger");
		animator.SetTrigger("IdlingTrigger");
	}
	public void setPower(int pow){
		animator.SetInteger("Power",pow);
	}
	// Update is called once per frame
	void Update () {
	}
}
