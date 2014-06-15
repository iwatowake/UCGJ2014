using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var resultInfo = ResultInfo.Instance;


        // dummy data
//        for (int i = 0; i < 4; i++)
//        {
//			resultInfo.data.Add(new ResultInfo.ResultData(i, null, i * 100));
//       }


        var resultDataList = resultInfo.data.ToArray();
        PlayerResult[] objs = GameObject.FindObjectsOfType<PlayerResult>();
        for (int i = 0; i < resultDataList.Length; i++)
        {
            objs[i].scoreGUIText.text = "" + resultDataList[i].score;
            objs[i].faceGUITexture.texture = resultDataList[i].sp.texture;
        }

    }

    // Update is called once per frame
    void Update()
    {
		if(Input.GetKeyDown (KeyCode.Space)){
			resetGame();
		}

    }

	void resetGame(){
		Destroy(GameStateManager.Instance.gameObject);
		Application.LoadLevel(0);
	}
}
