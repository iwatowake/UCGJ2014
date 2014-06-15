﻿using UnityEngine;
using System.Collections;

//! Gimmick1 TeslaCoil
public class Gimmick1 : MonoBehaviour {

	enum STATE{
		start_init=0,
		start_wait,

		off_init,
		off_wait,

		aleart_init,
		aleart_wait,

		on_init,
		on_wait,
	}

	public	float	START_TIME_OFFSET 	= 0.0f;
	const	float	TIME_OFF			= 15.0f;
	const	float	TIME_ON				= 3.0f;
			float	timer				= 0.0f;

			STATE	state				= STATE.start_init;

			bool	isHitEnable			= false;

	const	float	EFFECTSEC			= 3.0f;

	public	ParticleSystem	spark		= null;
	public	ParticleSystem	aleart		= null;

	void Update()	
	{
		switch (state) 
		{
		case STATE.start_init:
			state_start_init();
			break;
		case STATE.start_wait:
			state_start_wait();
			break;

		case STATE.off_init:
			state_off_init();
			break;
		case STATE.off_wait:
			state_off_wait();
			break;

		case STATE.aleart_init:
			state_aleart_init();
			break;
		case STATE.aleart_wait:
			state_aleart_wait();
			break;

		case STATE.on_init:
			state_on_init();
			break;
		case STATE.on_wait:
			state_on_wait();
			break;
		}
	}

	//--------------------------------
	// start
	void state_start_init()
	{
		Color col = renderer.material.color;
		col.a = 0.25f;
		renderer.material.color = col;
		isHitEnable = false;
		timer = 0;
		state++;
	}
	void state_start_wait()
	{
		if (timer > START_TIME_OFFSET) {
			state = STATE.off_init;
		} else {
			timer += Time.deltaTime;
		}
	}

	//--------------------------------
	// off
	void state_off_init()
	{
		Color col = renderer.material.color;
		col.a = 0.25f;
		renderer.material.color = col;
		isHitEnable = false;
		spark.Stop ();
		timer = 0;
		state++;
	}	
	void state_off_wait()
	{
		if (timer > TIME_OFF) {
			state = STATE.aleart_init;
		} else {
			timer += Time.deltaTime;
		}
	}

	//---------------------------------
	// aleart
	void state_aleart_init()
	{
		aleart.Play ();
		state++;
	}
	void state_aleart_wait()
	{
		if (aleart.isStopped) 
		{
			state = STATE.on_init;
		}
	}

	//---------------------------------
	// on
	void state_on_init()
	{
		Color col = renderer.material.color;
		col.a = 0.8f;
		renderer.material.color = col;
        isHitEnable = true;
		spark.particleSystem.Play ();
		timer = 0;
		state++;
	}
	void state_on_wait()
	{
		if (timer > TIME_ON) {
			state = STATE.off_init;
		} else {
			timer += Time.deltaTime;
		}
	}

	//----------------------------------
	// CollisionCallback
	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player" && isHitEnable) {
			UnityChanControll uc = collider.transform.GetComponent<UnityChanControll>();
			uc.OnCollPararise(EFFECTSEC);
		}
	}
}
