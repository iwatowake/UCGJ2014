using UnityEngine;
using System.Collections;

public class SetProgress : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public float pr;
	// Update is called once per frame
	void Update () {
		renderer.material.SetFloat("Progress",pr);

	}
}
