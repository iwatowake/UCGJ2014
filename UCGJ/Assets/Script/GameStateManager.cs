﻿using UnityEngine;
using System.Collections;

public class GameStateManager : SingletonMonoBehaviour<GameStateManager> {
	public int PlayerNum;
	public Sprite death;
	public Sprite little;
	public Sprite norm;
	public Sprite big;

	public GameObject[] stages;
	public const string STAGE_KEY  = "Stage_Key";
	public const string PLAYER_NUM_KEY = "Player_Num_Key";

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);

		GameObject.Instantiate(stages[PlayerPrefs.GetInt(STAGE_KEY)],Vector3.zero,Quaternion.identity);
		PlayerNum = GameObject.FindObjectsOfType<UnityChanControll>().Length;
	}

	public void setDeathList(int id, int score){
		if(score > 50){
			ResultInfo.Instance.data.Add(new ResultInfo.ResultData(id,big,score));
		}else if(score > 15){
			ResultInfo.Instance.data.Add(new ResultInfo.ResultData(id,norm,score));
		}else if(score > 0){
			ResultInfo.Instance.data.Add(new ResultInfo.ResultData(id,little,score));
		}else{
			ResultInfo.Instance.data.Add(new ResultInfo.ResultData(id,death,score));
		}
		PlayerNum--;
		if(PlayerNum <= 1){
			GameOver();
		}
	}

	public void GameOver(){
		UnityChanControll[] ucs = GameObject.FindObjectsOfType<UnityChanControll>();

		foreach(UnityChanControll uc in ucs){
			setDeathList(uc.player.id,uc.player.Power);
		}

		Application.LoadLevel("Result");
	}
}
