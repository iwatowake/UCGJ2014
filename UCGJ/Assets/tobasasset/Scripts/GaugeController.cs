using UnityEngine;
using System.Collections;

public class GaugeController : MonoBehaviour {
	private const int minityMax = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		float range;
//		float minity = (float)uc.player.Power;
//		range = minity / minityMax;
//		if (range < 0) range = 0;
//		this.transform.localScale = new Vector3(range, this.transform.localScale.y, this.transform.localScale.z);
	}

	public void setGauge(float minity){
		float range;
		range = minity / minityMax;
		if (range < 0) range = 0;
		this.transform.localScale = new Vector3(range, this.transform.localScale.y, this.transform.localScale.z);
	}
}
