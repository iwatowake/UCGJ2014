using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	private Transform target01;
	private Transform target02;
	private Transform target03;
	private Transform target04;
	private Transform target;
	float x0;
	float z0;
	float length1;
	float length2;
	float length3;
	float length4;
	float length;

	// Use this for initialization
	void Start () {
		target01 = GameObject.Find("01").transform;
		target02 = GameObject.Find ("02").transform;
		target03 = GameObject.Find("03").transform;
		target04 = GameObject.Find("04").transform;
		target   = GameObject.Find("Target").transform;
	}
	
	// Update is called once per frame
	void Update () {
		x0 = (target01.transform.position.x + target02.transform.position.x + target03.transform.position.x + target04.transform.position.x);
		z0 = (target01.transform.position.z + target02.transform.position.z + target03.transform.position.z + target04.transform.position.z);

		length1 = (((target01.transform.position.x - target02.transform.position.x)*(target01.transform.position.x - target02.transform.position.x))
		           +((target01.transform.position.z - target02.transform.position.z)*(target01.transform.position.z - target02.transform.position.z)));
		length2 = (((target02.transform.position.x - target03.transform.position.x)*(target02.transform.position.x - target03.transform.position.x))
		           +((target02.transform.position.z - target03.transform.position.z)*(target02.transform.position.z - target03.transform.position.z)));
		length3 = (((target03.transform.position.x - target03.transform.position.x)*(target03.transform.position.x - target04.transform.position.x))
		           +((target03.transform.position.z - target03.transform.position.z)*(target03.transform.position.z - target04.transform.position.z)));
		length4 = (((target04.transform.position.x - target01.transform.position.x)*(target04.transform.position.x - target01.transform.position.x))
		           +((target04.transform.position.z - target01.transform.position.z)*(target04.transform.position.z - target01.transform.position.z)));
		length  = 30.0f * 0.001f * (length1+length2+length3+length4);
		if(length < 20.0f)
		{
			length = 20.0f;
		}
		else if(length > 100.0f)
		{
			length = 100.0f;
		}
		Vector3 tempPos = new Vector3(x0/4,length,z0/4);
		Camera.main.transform.position =tempPos;
		Debug.Log (length);
	}
}
