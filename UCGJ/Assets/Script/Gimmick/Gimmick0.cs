using UnityEngine;
using System.Collections;

//! Gimmick0 ExplosionBlock
public class Gimmick0 : MonoBehaviour {
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player") {
			Destroy(gameObject);
			Instantiate(Resources.Load("Gimmick/Gimmick01"), transform.position, new Quaternion());
		}
	}
}