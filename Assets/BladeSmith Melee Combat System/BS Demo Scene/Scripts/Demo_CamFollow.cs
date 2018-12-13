using UnityEngine;
using System.Collections;

public class Demo_CamFollow : MonoBehaviour {

	public Transform lookat;
	public Transform follow;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (lookat);
		transform.position = Vector3.Lerp (transform.position, follow.position, Time.deltaTime * 5);

	}
}
