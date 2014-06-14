using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultInfo : SingletonMonoBehaviour<ResultInfo> {

	public class ResultData{
		public int playerNum;
		public Sprite sp;
		public int score;
	}

	public List<ResultData> data;

	void Start(){
		data = new List<ResultData>();
	}
}
