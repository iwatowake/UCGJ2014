using UnityEngine;
using System.Collections;

//! Gimmick1 Parrarises
public class Gimmick1 : MonoBehaviour {

	public	float	effectSec=0;
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player") {
			UnityChanControll uc = collision.transform.GetComponent<UnityChanControll>();
			uc.OnCollPararise(effectSec);
		}			
	}
}
