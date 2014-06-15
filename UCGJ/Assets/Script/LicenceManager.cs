using UnityEngine;
using System.Collections;

public class LicenceManager : MonoBehaviour {
	IEnumerator Start () {
		yield return new WaitForSeconds(3.0f);
		Application.LoadLevel("Title");
	}
}
