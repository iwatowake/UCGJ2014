using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {

	public GameObject GetParticle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay(Collision c){
		if(c.transform.tag =="Player"){
			UnityChanControll uc = c.transform.GetComponent<UnityChanControll>();
			uc.GetPower(1);
			GameObject obj = (GameObject)Instantiate(GetParticle,this.transform.position,this.transform.rotation);
			Destroy(obj,1.0f);
			Destroy (this.gameObject);
		}
	}
}
