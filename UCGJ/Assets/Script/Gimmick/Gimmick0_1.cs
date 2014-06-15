using UnityEngine;
using System.Collections;

//! Gimmick01 Explosion
public class Gimmick0_1 : MonoBehaviour {

	public	int		damage			= 1;

	const 	float	LIFETIME 		= 1.0f;
			float	timer 			= 0;

			bool	isDamageEnable	= true;

	void Start(){
		SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("bomb", "bomb"));
        SoundPlayer.Instance.playSE("bomb");
	}

	void Update () {
		timer += Time.deltaTime;
		if (timer > LIFETIME) 
		{
			isDamageEnable = false;
		}
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player" && isDamageEnable) {
			UnityChanControll uc = collider.transform.GetComponent<UnityChanControll>();
			uc.OnCollDamage(damage);
		}			
	}
}
