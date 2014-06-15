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

	enum STATE{
		state_0_init=0,
		state_0_wait,

		state_1_init,
		state_1_wait,

		state_2_init,
		state_2_wait,

		state_enumend
	}

//	public	DIR		dir=DIR.UP;
	public	float	pow=0;

	private	float	timer = 0;

	private	const 	float 	SCALESPEED = 50.0f;

	private	STATE	state = STATE.state_enumend;
	private	Vector3	baseScale = new Vector3 ();
	
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {

			Vector3 point = collider.transform.position;

			Vector3	vec = Vector3.zero;

			if(point.x <= transform.position.x - transform.lossyScale.x/4)
			{
				vec = Vector3.left * pow;
			}

			if(point.x >= transform.position.x + transform.lossyScale.x/4){
				vec = Vector3.right * pow;
			}

			if(point.z <= transform.position.z - transform.lossyScale.z/4){
				vec = Vector3.back * pow;
			}

			if(point.z >= transform.position.z + transform.lossyScale.z/4){
				vec = Vector3.forward * pow;
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
			UnityChanControll uc = collider.transform.GetComponent<UnityChanControll>();

			uc.OnCollImpact(vec);

			state = STATE.state_0_init;
		}
	}

	void Start()
	{
		baseScale = transform.localScale;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.D))
						state = STATE.state_0_init;

		switch (state) 
		{
		case STATE.state_0_init:
			timer = 0;
			state++;
			break;
		case STATE.state_0_wait:
			timer += Time.deltaTime*SCALESPEED;
			SetScaleRatio( Mathf.Lerp(1, 0.8f, timer) );

			if(timer > 1)
			{
				state++;
			}
			break;

		case STATE.state_1_init:
			timer = 0;
			state++;
			break;
		case STATE.state_1_wait:
			timer += Time.deltaTime*SCALESPEED;
			SetScaleRatio( Mathf.Lerp(0.8f, 1.2f, timer) );
			
			if(timer > 1)
			{
				state++;
			}
			break;

		case STATE.state_2_init:
			timer = 0;
			state++;
			break;
		case STATE.state_2_wait:
			timer += Time.deltaTime*SCALESPEED;
			SetScaleRatio( Mathf.Lerp(1.2f, 1.0f, timer) );
			
			if(timer > 1)
			{
				state++;
			}
			break;
		}
	}

	void SetScaleRatio(float ratio)
	{
		Vector3 scale = transform.localScale;
		scale = baseScale * ratio;
		transform.localScale = scale;
	}

	void SetScale(Vector3 scale)
	{
		transform.localScale = scale;
	}
}