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

	public void setGauge(int minity){
		float range;
		range = (float)minity / (float)minityMax;
        Debug.Log("range:" + range);
		if (range < 0) range = 0;
		this.transform.localScale = new Vector3(range, this.transform.localScale.y, this.transform.localScale.z);
	}
}
