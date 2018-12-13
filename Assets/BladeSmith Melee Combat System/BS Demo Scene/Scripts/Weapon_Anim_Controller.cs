using UnityEngine;
using System.Collections;

public class Weapon_Anim_Controller : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("f")) {
			//Animation.Play("Combo1State");
		}
		if (Input.GetKeyDown ("q")) {
			//Animation.Play("Combo2State");
		}
		if (Input.GetKeyDown ("e")) {
			//Animation.Play("Combo3State");
		}
		if (Input.GetKeyDown ("x")) {
			//Animation.Play("Combo4State");
		}

	}
}
