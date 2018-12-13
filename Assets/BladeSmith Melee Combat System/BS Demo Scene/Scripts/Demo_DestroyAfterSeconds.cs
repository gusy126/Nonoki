using UnityEngine;
using System.Collections;

public class Demo_DestroyAfterSeconds : MonoBehaviour {

	public float DestructionDelay = 7;
	// Use this for initialization
	void Start () {
		Destroy (gameObject,  DestructionDelay);
	}
	

}
