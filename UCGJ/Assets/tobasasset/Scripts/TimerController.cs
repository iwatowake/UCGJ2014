using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {

//	public SpriteRenderer spriteRenderer;
    public SpriteRenderer objOnes,objTens;  // 一の位、十の位のオブジェクト
    public int onesPlace, tensPlace;        // 一の位、十の位の値

	public float MaxTime = 40.0f;
	public float timer;
	public Sprite[] numbers;

	// Use this for initialization
	void Start () {
        objOnes.transform.Translate(1.0f*objOnes.transform.lossyScale.x, 0.0f, 0.0f);     // 一ケタ目をずらす
	}
	
	// Update is called once per frame
	void Update () {
        int time = (int)Time.timeSinceLevelLoad;
		int onesPlace;
		int tensPlace;

		if (time >= MaxTime)
		{
			time = (int)MaxTime-1;
		}

		onesPlace = 9 - time % 10;
		tensPlace = (int)(((MaxTime-0.01f)/10) - time / 10);

		SetSpriteRender(onesPlace, tensPlace);  // 子のスプライトを描画
	}

	void SetSpriteRender(int onesPlace, int tensPlace)
    {
        objOnes.sprite = numbers[onesPlace];
 //       Debug.Log("time1:" + onesPlace);
		if (tensPlace != 0) {
			objTens.sprite = numbers [tensPlace];
			Vector3 pos = objTens.transform.position;
			objOnes.transform.position = new Vector3(1.0f * objOnes.transform.lossyScale.x + pos.x, pos.y, pos.z);
		} else {
			objTens.sprite = null;
			Vector3 pos = objTens.transform.position;
			objOnes.transform.position = new Vector3(0.5f * objOnes.transform.lossyScale.x + pos.x, pos.y, pos.z);
		}
  //      Debug.Log("time10:" + tensPlace);

    }
}
