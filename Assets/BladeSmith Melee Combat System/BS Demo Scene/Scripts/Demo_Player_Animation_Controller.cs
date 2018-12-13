using UnityEngine;
using System.Collections;

public class Demo_Player_Animation_Controller : MonoBehaviour {


	public Animator PlayerAnimator;
	public GameObject SoulSwords;
	float t;
	bool freeze;
	// Update is called once per frame


	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	void Update () {
		if (freeze) {
			t+= Time.deltaTime;
			if(t >0.85f)
			{
				freeze = false;
				t = 0;
			}
		}

		if (Input.GetKeyDown ("p")) {
			Application.LoadLevel(Application.loadedLevel);
		}

		///////////////
	
		if (Input.GetMouseButton (0) && freeze == false) {
			PlayerAnimator.SetInteger("Combo", 1);
			freeze = true;
		}
		if (!Input.GetMouseButton (0) && freeze == false) {
			PlayerAnimator.SetInteger("Combo", 0);
		}
	

		if (Input.GetMouseButton (1) && freeze == false) {
			PlayerAnimator.SetInteger("Combo", 2);
			freeze = true;
		}
		if (!Input.GetMouseButton (1) && freeze == false) {
			PlayerAnimator.SetInteger("Combo", 0);
		}

		if (Input.GetKeyDown ("x")) {
			SoulSwords.SetActive (true);
		}
		if (Input.GetKeyUp ("x")) {
			SoulSwords.SetActive (false);
		}
	}
}
