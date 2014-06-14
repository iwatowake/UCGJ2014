using UnityEngine;
using System.Collections;

//! Gimmick2 ImpactBlock
public class Gimmick2 : MonoBehaviour {

/*	public	enum DIR
	{
		UP=0,
		DOWN,
		RIGHT,
		LEFT
	}
*/

//	public	DIR		dir=DIR.UP;
	public	float	pow=0;
	
	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Player") {

			Vector3 point = collision.contacts[0].point;

			Vector3	vec = Vector3.zero;

			if(point.x <= transform.position.x - transform.lossyScale.x/2)
			{
				vec = Vector3.left * pow;
			}else if(point.x >= transform.position.x + transform.lossyScale.x/2){
				vec = Vector3.right * pow;
			}else if(point.y <= transform.position.y - transform.lossyScale.y/2){
				vec = Vector3.down * pow;
			}else if(point.y >= transform.position.y + transform.lossyScale.y/2){
				vec = Vector3.up * pow;
			}
/*
			switch(dir)
			{
			case DIR.UP:
				vec = Vector3.forward * pow;
				break;
			case DIR.DOWN:
				vec = Vector3.back * pow;
				break;
			case DIR.RIGHT:
				vec = Vector3.right * pow;
				break;
			case DIR.LEFT:
				vec = Vector3.left * pow;
				break;
			}
*/
			UnityChanControll uc = collision.transform.GetComponent<UnityChanControll>();

			uc.OnCollImpact(vec);
		}
	}

}