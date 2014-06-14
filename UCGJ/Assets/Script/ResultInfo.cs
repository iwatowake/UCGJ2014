using UnityEngine;
using System.Collections;

public class ResultInfo : SingletonMonoBehaviour<ResultInfo> {

	public class ResultDate{
		public int playerNum;
		public Sprite sp;
		public int score;
	}

	public ResultData[] data;
}
