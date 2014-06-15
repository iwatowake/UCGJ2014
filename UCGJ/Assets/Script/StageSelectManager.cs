using UnityEngine;
using System.Collections;

public class StageSelectManager : MonoBehaviour {

	enum BLACESTATE{
		WIDE=0,
		NALLOW
	}

	public	GameObject[]	stagePrefabs;
	public	GUIText			blace;
	public	GUIText			stageName;

	private	int				nowSelect=0;

//	private	float			timer=0;

	private	BLACESTATE		state;

	private	float			oldAxis = 0;

	private string[] 	STAGE_NAMES = new string[]
	{
		"The Flat",
		"Classic",
		"BBQ",
		"Bump&Fire",
		"TeslaCoils",
		"a Series Circuit",
		"Interruption",
		"BombRoom",
		"Red-Hot",
		"Mad Lab."
	};


	void Start () {
		for (int i=0; i<stagePrefabs.Length; i++) 
		{
			stagePrefabs[i].transform.position = new Vector3(0,5000,0);
		}

		stagePrefabs [0].transform.position = new Vector3 (0, 0, 0);
	}	

	void Update () {

		if (Input.GetButtonDown ("ButtonA1")) {
			PlayerPrefs.SetInt (GameStateManager.STAGE_KEY, 0 + nowSelect);

			int plNum = PlayerPrefs.GetInt(GameStateManager.PLAYER_NUM_KEY);

			switch(plNum)
			{
			case 2:
				break;
			case 3:
				break;
			case 4:
				break;
			}

		} else {

			float axis = Input.GetAxisRaw ("Horizontal0");
			if (axis == oldAxis) {
					return;
			}

			int i = (int)axis;

			switch (i) {
			case -1:
					stagePrefabs [nowSelect].transform.position = new Vector3 (0, 5000, 0);
					if (nowSelect == 0) {
							nowSelect = stagePrefabs.Length - 1;
					} else {
							nowSelect--;
					}
					stagePrefabs [nowSelect].transform.position = new Vector3 (0, 0, 0);
					break;
			case 1:
					stagePrefabs [nowSelect].transform.position = new Vector3 (0, 5000, 0);
					if (nowSelect == stagePrefabs.Length - 1) {
							nowSelect = 0;
					} else {
							nowSelect++;
					}
					stagePrefabs [nowSelect].transform.position = new Vector3 (0, 0, 0);
					break;
			default:
					break;
			}

			stageName.text = STAGE_NAMES [nowSelect];

			oldAxis = axis;
		}
	}
}
