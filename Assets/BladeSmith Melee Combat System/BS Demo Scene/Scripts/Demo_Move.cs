using UnityEngine;
using System.Collections;

public class Demo_Move : MonoBehaviour {


	public CharacterController c;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		c.Move (transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * 7);
		c.Move (transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * 7);
		c.Move (-transform.up   * Time.deltaTime * 1);
	}
}
