using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    void OnGUI()
    {
        if (Event.current.type == EventType.KeyDown)
        {
            Application.LoadLevel("TitleScene");
        }
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }
}
