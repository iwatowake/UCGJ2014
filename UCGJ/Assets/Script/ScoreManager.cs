using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var resultInfo = ResultInfo.Instance;


        // dummy data
        for (int i = 0; i < 4; i++)
        {
            var resultData = new ResultInfo.ResultData();
            resultData.playerNum = i;
            resultData.score = i * 100;
            resultInfo.data.Add(resultData);
        }


        var resultDataList = resultInfo.data.ToArray();
        PlayerResult[] objs = GameObject.FindObjectsOfType<PlayerResult>();
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].scoreGUIText.text = "" + resultDataList[i].score;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
