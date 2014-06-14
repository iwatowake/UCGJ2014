using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    // Use this for initialization
    void Start ()
    {
        PlayerResult[] objs = GameObject.FindObjectsOfType<PlayerResult> ();
        for (int i = 0; i < objs.Length; i++) {
            objs [i].scoreGUIText.text = "" + i;
        }
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
