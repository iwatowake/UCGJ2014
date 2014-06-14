using UnityEngine;
using System.Collections;

//! Gimmick0 ExplosionBlock
public class Gimmick0 : MonoBehaviour {
	public GameObject obj;
	void OnTriggerEnter(Collider collision){
		Debug.Log ("Col:" + collision.gameObject.tag);
		if (collision.gameObject.tag == "Player") {
//			Destroy(gameObject);
//			Instantiate(Resources.Load("Gimmick/Gimmick01"), transform.position, new Quaternion());
			Instantiate(obj, transform.position, new Quaternion());
			UnityChanControll uc = collision.GetComponent<UnityChanControll>();
			uc.OnCollImpact((collision.transform.position - this.transform.position) * 5);
		}
	}
}