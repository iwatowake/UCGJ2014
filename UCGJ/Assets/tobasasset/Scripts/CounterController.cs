using UnityEngine;
using System.Collections;

public class CounterController : MonoBehaviour
{

//	public UnityChanControll uc;
    //	public SpriteRenderer spriteRenderer;
    public SpriteRenderer objOnes, objTens;  // 一の位、十の位のオブジェクト
//    public int onesPlace, tensPlace;        // 一の位、十の位の値
	//public TestPlayerParam playerParamObj;
    //public Transform parentTransform;       // 親の座標
    public int parentMinity;           // 親の情報を持ったスクリプト
    public int value;
    public Sprite[] numbers;

    // Use this for initialization
    void Start()
    {
        //       parentTransform = GetComponentInParent<Transform>();
        //parentTransform = this.transform.parent;
        //parentParamScript = GetComponentsInParent<TestPlayerParam>();
        //parentParamScript = this.transform.Find("Cube").GetComponent(typeof(TestPlayerParam)).


        objOnes.transform.Translate(1.0f * objOnes.transform.lossyScale.x, 0.0f, 0.0f);     // 一ケタ目をずらす
    }

    // Update is called once per frame
    void Update()
    {
        //parentMinity = this.GetComponentInParent<TestPlayerParam>().minity;
        //parentMinity = this.transform.Find("Cube").GetComponent<TestPlayerParam>().minity;
		//playerParamObj = GameObject.Find ("Cube").GetComponent<TestPlayerParam> ();
//		playerParamObj = TestPlayerParam.Instance;
//		parentMinity = playerParamObj.minity;

        //value = parentTransform.position.x;
//        value = parentMinity;

//		value = uc.player.Power;
//		if (value <= 0)
//		{
//			value = 0;
//		}
//      int onesPlace = value % 10;
//    int tensPlace = value / 10;

//s        SetSpriteRender(onesPlace,tensPlace);  // 子のスプライトを描画
    }

	public void setPoints(int minity){
		if (minity <= 0)
		{
			minity = 0;
		}
		int onesPlace = minity % 10;
		int tensPlace = minity / 10;
		
		SetSpriteRender(onesPlace,tensPlace);  // 子のスプライトを描画
	}

    void SetSpriteRender(int onesPlace, int tensPlace)
    {
        objOnes.sprite = numbers[onesPlace];
//        Debug.Log("time1:" + onesPlace);

		objTens.sprite = numbers [tensPlace];
		Vector3 pos = objTens.transform.position;
		objOnes.transform.position = new Vector3(1.0f * objOnes.transform.lossyScale.x + pos.x, pos.y, pos.z);

//		Debug.Log("time10:" + tensPlace);

    }
}
