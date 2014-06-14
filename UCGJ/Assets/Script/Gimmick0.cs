using UnityEngine;
using System.Collections;

public class Gimmick0 : MonoBehaviour {

	public	int	damage;

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player") {
			UnityChanControll uc = collision.transform.GetComponent<UnityChanControll>();
			uc.OnCollDamage(damage);
		}			
	}
}