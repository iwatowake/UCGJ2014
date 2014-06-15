using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultInfo : SingletonMonoBehaviour<ResultInfo> {

	public class ResultData{
		public int playerNum;
		public Sprite sp;
		public int score;

		public ResultData(int playerNum, Sprite sp, int score){
			this.playerNum = playerNum;
			this.sp = sp;
			this.score = Mathf.Clamp (score * 5,0,1000);
		}
	}

	public List<ResultData> data;

	void Start(){
		data = new List<ResultData>();
	}
}
