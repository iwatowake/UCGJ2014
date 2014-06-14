using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {
	private bool isReverse;
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		lookRight();
	}
	public void lookRight(){
		this.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler(0,180,0);
	}
	public void lookLeft(){
		this.transform.rotation = Camera.main.transform.rotation;
	}

	public void SetRun(bool isRun){
		animator.SetBool("IsRun",isRun);
	}
	public void setDamage(){
		animator.SetTrigger("DamageTrigger");
	}
	public void setIdle(){
		animator.SetTrigger("IdlingTrigger");
	}

	// Update is called once per frame
	void Update () {
	}
}
