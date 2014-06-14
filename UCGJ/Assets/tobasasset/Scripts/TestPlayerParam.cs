using UnityEngine;
using System.Collections;

public class TestPlayerParam : SingletonMonoBehaviour<TestPlayerParam> {

    public int minity;  // 仮ミニティ50
	public int minityMax;	// 仮ミニティ最大数
	// Use this for initialization
	void Start () {
        minity = 25;
		minityMax = 100;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Z))
        {
            minity++;
        }
		if(Input.GetKeyDown(KeyCode.X))
		{
			minity--;
		}
		if (minity > minityMax) minity = minityMax;
		if (minity < 0) minity = 0;
	}
}
