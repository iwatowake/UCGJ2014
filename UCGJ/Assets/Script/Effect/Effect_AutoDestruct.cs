using UnityEngine;
using System.Collections;

public class Effect_AutoDestruct : MonoBehaviour {

	void Update () {
		if (!particleSystem.isPlaying) 
		{
			Destroy(gameObject);
		}
	}
}
