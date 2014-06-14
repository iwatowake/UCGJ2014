using UnityEngine;
using System.Collections;

//! Gimmick0 Damage
public class Gimmick0 : MonoBehaviour {

	public	int	damage=0;

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player") {
			UnityChanControll uc = collision.transform.GetComponent<UnityChanControll>();
			uc.OnCollDamage(damage);
		}			
	}
}