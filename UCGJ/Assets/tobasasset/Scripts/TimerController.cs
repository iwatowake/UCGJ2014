using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {

//	public SpriteRenderer spriteRenderer;
    public SpriteRenderer objOnes,objTens;  // 一の位、十の位のオブジェクト
    public int onesPlace, tensPlace;        // 一の位、十の位の値
    public int beforeOnesPlace;            // 過去の一の位

	public float MaxTime = 40.0f;
	public float timer;
	public Sprite[] numbers;

	// Use this for initialization
	void Start () {
        objOnes.transform.Translate(1.0f*objOnes.transform.lossyScale.x, 0.0f, 0.0f);     // 一ケタ目をずらす
        setSE();
        beforeOnesPlace = -1;
    }

    void setSE()
    {
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1136", "Zero"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1137", "One"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1138", "Two"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1139", "Three"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1140", "Four"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1141", "Five"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1142", "Six"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1143", "Seven"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1144", "Eight"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1145", "Nine"));
        SoundPlayer.Instance.addSe(new SoundPlayer.AudioClipInfo("univ1146", "Ten"));
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

        if(beforeOnesPlace != onesPlace)
        {
            if (tensPlace <= 0)
            {
                switch (onesPlace)
                {
                    case 0:
                        SoundPlayer.Instance.playSE("Zero");
					GameStateManager.Instance.GameOver();
                        break;
                    case 1:
                        SoundPlayer.Instance.playSE("One");
                        break;
                    case 2:
                        SoundPlayer.Instance.playSE("Two");
                        break;
                    case 3:
                        SoundPlayer.Instance.playSE("Three");
                        break;
                    case 4:
                        SoundPlayer.Instance.playSE("Four");
                        break;
                    case 5:
                        SoundPlayer.Instance.playSE("Five");
                        break;
                    case 6:
                        SoundPlayer.Instance.playSE("Six");
                        break;
                    case 7:
                        SoundPlayer.Instance.playSE("Seven");
                        break;
                    case 8:
                        SoundPlayer.Instance.playSE("Eight");
                        break;
                    case 9:
                        SoundPlayer.Instance.playSE("Nine");
                        break;
                    default:
                        break;
                }
            }
            if (tensPlace == 1 && onesPlace == 0) SoundPlayer.Instance.playSE("Ten");
        }
        beforeOnesPlace = onesPlace;

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
